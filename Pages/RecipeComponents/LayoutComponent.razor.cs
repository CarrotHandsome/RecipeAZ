using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using RecipeAZ.Models;
using Microsoft.AspNetCore.Identity;
using System.Net.NetworkInformation;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Components.Forms;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace RecipeAZ.Pages.RecipeComponents {
    public partial class LayoutComponent {
        //DataContext? DataContext { get; set; }
        UserManager<AppUser>? UserManager { get; set; }
        private int imageSizeMaxBytes = 1024000;
        protected override async Task OnInitializedAsync() {
            AuthenticationState authState = await authenticationStateProvider.GetAuthenticationStateAsync();            
            var user = authState.User;
            if (user != null && user!.Identity != null && user!.Identity!.IsAuthenticated) {
                var userIdClaim = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim != null && userIdClaim.Value != null) {
                    User = await dataContext.Users
                        .Include(u => u.RecipesILike)
                        .FirstOrDefaultAsync(u => u.Id == userIdClaim.Value);
                    UserId = User?.Id;
                }
            }

            if (Id == null) {
                Recipe = new Recipe {
                    UserId = UserId,
                    User = User,
                    Name = "New Recipe",
                    Description = "description",
                    Notes = "notes",
                    RecipeIngredients = new List<RecipeIngredient>(),
                    RecipeSteps = new List<RecipeStep>(),
                    RecipeTags = new List<RecipeTag>()
                };
                CanEdit = true;
                Editing = true;
                
            } else {
                Recipe = await dataContext.Recipes                  
                    .Include(r => r.RecipeIngredients)
                    .Include(r => r.RecipeSteps)
                    .Include(r => r.User)
                    .Include(r => r.UsersWhoLikeMe)
                    .Include(r => r.Comments!)
                        .ThenInclude(c => c.User)
                    .Include(r => r.RecipeTags)
                        .ThenInclude(rt => rt.Tag)
                    .FirstOrDefaultAsync(r => r.RecipeId == Id);
                Console.WriteLine("comments is null: " + (Recipe.Comments == null).ToString());

                    if (Recipe == null) {
                        Console.WriteLine("null recipe from Id");
                        Recipe = new Recipe {
                            UserId = UserId,
                            Name = "New Recipe",
                            Description = "description",
                            Notes = "notes",
                            RecipeIngredients = new List<RecipeIngredient>(),
                            RecipeSteps = new List<RecipeStep>(),
                            Comments = new List<Comment>()
                        };
                    }
            }
            if (UserId != null && UserId == Recipe.UserId) {
                CanEdit = true;
                
            }
            Liked = await dataContext.Users
                .Where(u => u.Id == UserId)
                .AnyAsync(u => u.RecipesILike.Any(rl => rl.RecipeId == Recipe.RecipeId));
        }

        private async Task SaveRecipe(bool fromCreator=true) {

            if (Id != null) {
                dataContext.Recipes.Update(Recipe!);
            } else {
                await dataContext.Recipes.AddAsync(Recipe!);
                Console.WriteLine("added recipe..");
                //dataContext.Recipes.Update(EditRecipe!);
            }
            Console.WriteLine("Saving changes...");
            await dataContext.SaveChangesAsync();
            Console.WriteLine("Saved");
            if (fromCreator) {
                snackBar.Configuration.PositionClass = MudBlazor.Defaults.Classes.Position.BottomCenter;
                snackBar.Configuration.VisibleStateDuration = 500;
                snackBar.Add("Saved Recipe", MudBlazor.Severity.Success);                
            }
            navigationManager.NavigateTo($"/recipe/{Recipe!.RecipeId}");
        }

        private async Task OnImageUpload(IBrowserFile file) {

            try {
                IBrowserFile uploadedImage = file;
                if (uploadedImage.Size <= imageSizeMaxBytes) {
                    string imageFolder = "images";
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(uploadedImage.Name)}";
                    string folderPath = Path.Combine(env.WebRootPath, imageFolder);
                    string fullPath = Path.Combine(folderPath, fileName);

                    using var memoryStream = new MemoryStream();
                    await uploadedImage.OpenReadStream().CopyToAsync(memoryStream);

                    memoryStream.Position = 0; // Reset the memory stream position
                    using var image = Image.Load(memoryStream);
                    
                    if (image.Width > image.Height * 2) {
                        snackBar.Add("Image width must not be more than double image height.");
                        return;
                    }

                    if (!Directory.Exists(folderPath)) {
                        Directory.CreateDirectory(folderPath);
                    }

                    using (var stream = new FileStream(fullPath, FileMode.Create)) {
                        await uploadedImage.OpenReadStream(imageSizeMaxBytes).CopyToAsync(stream);
                        await stream.FlushAsync();
                    }

                    if (!string.IsNullOrEmpty(Recipe.ImagePath)) {
                        string oldImagePath = Path.Combine(env.WebRootPath, Recipe.ImagePath);

                        if (System.IO.File.Exists(oldImagePath) && oldImagePath != "images/recipe_default.png") {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    Recipe.ImagePath = $"{imageFolder}/{fileName}";
                    dataContext.Update(Recipe);
                    await dataContext.SaveChangesAsync();
                }
                else {
                    snackBar.Configuration.PositionClass = MudBlazor.Defaults.Classes.Position.TopCenter;
                    snackBar.Configuration.VisibleStateDuration = 500;
                    snackBar.Add($"Image file too large. Maximum size is {imageSizeMaxBytes} bytes", MudBlazor.Severity.Warning);
                }

                
            } catch (Exception ex) {

                Logger.LogError(ex, "An error occurred while uploading the image.");
            }

        }

        public async Task OnToggleSave() {
            await SaveRecipe();
        }

        private void UpdateEditState(bool editState) {
            Editing = editState;
            StateHasChanged();
        }
        
        private async Task HandleChipClose(RecipeTag recipeTag) {
            Recipe.RecipeTags.Remove(recipeTag);
            await dataContext.SaveChangesAsync();   
        }
        private async Task AddTag() {
            if (!string.IsNullOrEmpty(NewTagName)) {
                Tag existingTag = await dataContext.Tags.FirstOrDefaultAsync(t => t.Name == ToTitleCase(NewTagName));
                Tag tagToAdd;
                if (existingTag == null) {
                    tagToAdd = new Tag { Name = ToTitleCase(NewTagName) };
                    dataContext.Tags.Add(tagToAdd);
                    await dataContext.SaveChangesAsync();
                } else {
                    tagToAdd = existingTag;
                }

                RecipeTag recipeTag = new RecipeTag { RecipeId = Recipe.RecipeId, TagId = tagToAdd.TagId };
                dataContext.RecipeTags.Add(recipeTag);
                await dataContext.SaveChangesAsync();
                Recipe.RecipeTags.Add(recipeTag);
                NewTagName = string.Empty;
                NewTagOpen = false;
            }
        }

        private void NavigateToProfile(string userId) {
            navigationManager.NavigateTo("/profile/" + userId);
        }

        public string ToTitleCase(string input) {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(input.ToLower());
        }
    }
}

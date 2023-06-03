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
using static MudBlazor.Colors;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace RecipeAZ.Pages.RecipeComponents {
    public partial class LayoutComponent {
        //_recipeService.DataContext? _recipeService.DataContext { get; set; }
        UserManager<AppUser>? UserManager { get; set; }
        private int imageSizeMaxBytes = 1024000;

        protected override async Task OnInitializedAsync() {
            await base.OnInitializedAsync();
        }
        protected override async Task OnParametersSetAsync() {
            await base.OnParametersSetAsync();
            try {
                await LoadData();
            } catch (Exception e) {
                Console.WriteLine("EXCEPTION: " + e);
            }
        }
        private async Task LoadData() {
            Console.WriteLine("Id: " + Id);
            if (Id == null && User != null) {
                Recipe = _recipeService.NewRecipe(User);
                CanEdit = true;
                Editing = true;
            } else if (Id == null && User == null) {
                navigationManager.NavigateTo("/");
            } else {
                Recipe = await _recipeService.LoadRecipeAsync(Id, User);
            }
            if (User != null && User.Id == Recipe.UserId) {
                CanEdit = true;
            }
            if (User != null) {
                Liked = User.RecipesILike
                .Any(rl => rl.RecipeId == Id);
            }
            Console.WriteLine($"Recipe is null in LoadData: {Recipe == null}");
            _relatedRecipesWeighted = await _recipeService.GetRelatedRecipesWeightedAsync(Recipe ?? new());
        }
        private async Task SaveRecipe(bool fromCreator=true) {
            Recipe = await _recipeService.SaveRecipeAsync(Id);
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
                    await _recipeService.SaveRecipeAsync(Id);
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
        private void UpdateEditState(bool editState) {
            Editing = editState;
            StateHasChanged();
        }
        private async Task HandleChipClose(Tag tag) {
            RecipeTag rt = Recipe.RecipeTags.Where(t => t.Tag == tag).FirstOrDefault();
            if (rt != null) {
                Recipe.RecipeTags.Remove(rt);
                await _recipeService.DataContext.SaveChangesAsync();
            }            
        }
        private async Task AddTag() {
            if (!string.IsNullOrEmpty(NewTagName)) {
                await _recipeService.AddTag(NewTagName);
                StateHasChanged();
                NewTagName = string.Empty;
                NewTagOpen = false;
            }
        }

        private void NavigateToProfile(string userId) {
            navigationManager.NavigateTo("/profile/" + userId);
        }

        
        private async Task<IEnumerable<string>> TagSearch(string value) {
            if (string.IsNullOrEmpty(value)) {
                return new List<string>();
            }
            
            List<string> tags = await _recipeService.DataContext.Tags.Select(t => t.Name).ToListAsync();
            //foreach(RecipeTag rt in Recipe.RecipeTags) {
            //    tags.Remove(_recipeService.DataContext.Tags.FirstOrDefault(t => t.TagId == rt.TagId).Name);                
            //}
                          
            return tags.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }

        private void ToggleLike(bool toggled) {
            Console.WriteLine(toggled);
            RecipeLike rl = new RecipeLike {
                AppUserId = User.Id!,
                RecipeId = Recipe.RecipeId
            };
            if (toggled) {
                _recipeService.AddLike(rl);
            } else {
                _recipeService.RemoveLike(User);
            }
            Liked = toggled;
            if (User?.Id != Recipe.UserId) {
                _recipeService.SaveRecipe(Recipe.RecipeId);
            }
            //StateHasChanged();
        }
    }
}

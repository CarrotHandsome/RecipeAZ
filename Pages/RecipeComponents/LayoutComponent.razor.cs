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
using System.Drawing.Imaging;


namespace RecipeAZ.Pages.RecipeComponents {
    public partial class LayoutComponent {
        UserManager<AppUser>? UserManager { get; set; }
        private int imageSizeMaxBytes = 1024000;     
        protected override async Task OnParametersSetAsync() {
            
            try {
                await LoadData();
            } catch (Exception e) {
                Console.WriteLine("EXCEPTION: " + e);
            }
        }
        private async Task LoadData() {
            //Console.WriteLine("Id: " + Id);
            if (Id == null && User != null) {
                _recipe = _recipeService.NewRecipe(User);
                _recipe.ImagePath = "recipeaz-images/recipe_default.png";
                CanEdit = true;
                Editing = true;
            } else if (Id == null && User == null) {
                navigationManager.NavigateTo("/");
            } else {
                _recipe = await _recipeService.LoadRecipeAsync(Id, User);
            }
            if (User != null && User.Id == _recipe.UserId) {
                CanEdit = true;
            }
            if (User != null) {
                Liked = User.RecipesILike
                .Any(rl => rl.RecipeId == Id);
            }
            //Console.WriteLine($"Recipe is null in LoadData: {_recipe == null}");
            _relatedRecipesWeighted = await _recipeService.GetRelatedRecipesWeightedAsync(_recipe ?? new());
        }
        private async Task SaveRecipe(bool fromCreator=true) {
            _recipe = await _recipeService.SaveRecipeAsync(Id);
            if (fromCreator) {
                snackBar.Configuration.PositionClass = MudBlazor.Defaults.Classes.Position.BottomCenter;
                snackBar.Configuration.VisibleStateDuration = 500;
                snackBar.Add("Saved Recipe", MudBlazor.Severity.Success);                
            }
            navigationManager.NavigateTo($"/recipe/{_recipe!.RecipeId}");
        }

        private async Task OnImageUpload(IBrowserFile file) {
            //Console.WriteLine("Uploading image...");
            try {
                Console.WriteLine("Starting upload...");
                IBrowserFile uploadedImage = file;


                using var memoryStream = new MemoryStream();
                await uploadedImage.OpenReadStream(imageSizeMaxBytes * 15).CopyToAsync(memoryStream);
                Console.WriteLine("Opened read stream");
                memoryStream.Position = 0; // Reset the memory stream position
                using var image = Image.Load(memoryStream);
                Console.WriteLine("Loaded image");

                string imageFolder = "recipeaz-images";
                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(uploadedImage.Name)}";
                string folderPath = Path.Combine(env.WebRootPath, imageFolder);
                string fullPath = Path.Combine(folderPath, fileName);
                    
                if (!_imageService.AdjustImageToMaxFileSize(image, imageSizeMaxBytes, Path.GetExtension(uploadedImage.Name))) {
                    Console.WriteLine("Couldn't adjust size");
                    snackBar.Add("Image could not be resized");
                    return;
                }
                else if (image.Width > image.Height * 2) {
                    //Console.WriteLine("Image too wide");
                    snackBar.Add("Image width must not be more than double image height.");
                    return;
                } else {

                    Console.WriteLine("Image adjusted");

                    if (!Directory.Exists(folderPath)) {
                        Directory.CreateDirectory(folderPath);
                    }

                    using (var stream = new FileStream(fullPath, FileMode.Create)) {
                        await uploadedImage.OpenReadStream(imageSizeMaxBytes * 15).CopyToAsync(stream);
                        await stream.FlushAsync();
                    }

                    if (!string.IsNullOrEmpty(_recipe.ImagePath)) {
                        string oldImagePath = Path.Combine(env.WebRootPath, _recipe.ImagePath);
                        List<Recipe> recipesUsingImage =
                            await _recipeService.GetRecipesAsync(r => r.ImagePath == _recipe.ImagePath, "", -1);
                        if (System.IO.File.Exists(oldImagePath)

                            && oldImagePath != Path.Combine(env.WebRootPath, "recipeaz-images/recipe_default.png")

                            && recipesUsingImage.Count == 0) {
                            //Console.WriteLine("deleted old image");
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    _recipe.ImagePath = $"{imageFolder}/{fileName}";
                    await _recipeService.SaveRecipeAsync(Id);
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
            RecipeTag rt = _recipe.RecipeTags.Where(t => t.Tag == tag).FirstOrDefault();
            if (rt != null) {
                _recipe.RecipeTags.Remove(rt);
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
            //Console.WriteLine(toggled);
            RecipeLike rl = new RecipeLike {
                AppUserId = User.Id!,
                RecipeId = _recipe.RecipeId
            };
            if (toggled) {
                _recipeService.AddLike(rl);
            } else {
                _recipeService.RemoveLike(User);
            }
            Liked = toggled;
            if (User?.Id != _recipe.UserId) {
                _recipeService.SaveRecipe(_recipe.RecipeId);
            }
        }
    }
}

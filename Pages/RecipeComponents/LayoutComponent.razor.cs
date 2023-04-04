using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using RecipeAZ.Models;
using Microsoft.AspNetCore.Identity;
using System.Net.NetworkInformation;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace RecipeAZ.Pages.RecipeComponents {
    public partial class LayoutComponent {
        //DataContext? DataContext { get; set; }
        UserManager<AppUser>? UserManager { get; set; }        


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
                    RecipeSteps = new List<RecipeStep>()
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

        public async Task OnToggleSave() {
            await SaveRecipe();
        }

        private void UpdateEditState(bool editState) {
            Editing = editState;
            StateHasChanged();
        }
        
        
    }
}

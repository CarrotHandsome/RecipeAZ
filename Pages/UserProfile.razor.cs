using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace RecipeAZ.Pages {
    public partial class UserProfile {
        protected override async Task OnInitializedAsync() {
            AuthenticationState authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            if (user != null && user!.Identity != null && user!.Identity!.IsAuthenticated) {
                var userIdClaim = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim != null && userIdClaim.Value != null) {
                    Console.WriteLine("user authorized: ");
                    
                    Console.WriteLine("welcome " + User.UserName);
                                        
                }
            }
            User = await dataContext.Users
                .Include(u => u.Recipes)
                .FirstOrDefaultAsync(u => u.Id == Id);
            Recipes = await dataContext.Recipes
                .Where(r => r.UserId == Id)
                .Include(r => r.UsersWhoLikeMe)
                .Include(r => r.User)
                .Include(r => r.RecipeTags)
                    .ThenInclude(rt => rt.Tag)
                .ToListAsync();
        }

        private void NavigateToRecipe(string recipeId) {
            navigationManager.NavigateTo("/recipe/" + recipeId);
        }

        private void Sort(string sortBy, bool descending) {
            if (sortBy == "like") {
                Recipes.Sort((recipe1, recipe2) => recipe1.UsersWhoLikeMe.Count.CompareTo(recipe2.UsersWhoLikeMe.Count));
            } else if (sortBy == "date") {
                Recipes.Sort((recipe1, recipe2) => recipe1.CreatedAt.CompareTo(recipe2.CreatedAt));
            }
            if (descending) {
                Recipes.Reverse();
            }
            StateHasChanged();
        }
        
    }
}

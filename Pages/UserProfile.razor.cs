using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using RecipeAZ.Models;
using System.Security.Claims;

namespace RecipeAZ.Pages {
    public partial class UserProfile {
        protected override async Task OnInitializedAsync() {
            await base.OnInitializedAsync();
            using (var context = _contextFactory.CreateDbContext()) {
                Recipes = await context.Recipes
                    .Where(r => r.UserId == Id)
                    .Include(r => r.UsersWhoLikeMe)
                    .Include(r => r.User)
                    .Include(r => r.RecipeTags)
                        .ThenInclude(rt => rt.Tag)
                    .ToListAsync();
                ProfileUser = context.Users.Where(u => u.Id == Id).FirstOrDefault();
            }           
                
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

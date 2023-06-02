using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using RecipeAZ.Models;
using System.Diagnostics;
using System.Text.Json;

namespace RecipeAZ.Pages.RecipeComponents {
    public partial class IngredientsComponent {
        protected override async Task OnInitializedAsync() {
            await base.OnInitializedAsync();
            //DataContext = await _contextFactory.CreateDbContextAsync();
            if (ItemRecipe != null && ItemRecipe.RecipeIngredients != null) {
                foreach (RecipeIngredient ri in ItemRecipe!.RecipeIngredients!) {
                    detailsOpen[ri] = false;
                }
            }

            AllIngredients = await _recipeService.GetAllIngredientNames();
        }        
        private void AddRecipeIngredient(RecipeIngredient ri) {
            _recipeService.AddRecipeIngredient(ri);
            detailsOpen[ri] = ri.Details != string.Empty;            
            ShowNewIngredientInput = false;
            LastItem = new();
            StateHasChanged();
        }
        
        private Ingredient NewIngredient(string name, RecipeIngredient ri) {
            Ingredient ingredient = new Ingredient {
                Name = name,
                RecipeIngredients = new List<RecipeIngredient> { ri }
            };
            
            //DataContext.Ingredients.Add(ingredient);
            return ingredient;
        }

        private async Task<IEnumerable<string>> FindIngredientMatches(string matchTo) {
            if (string.IsNullOrEmpty(matchTo)) {
                return new List<string>();
            }
            using var DataContext = await _contextFactory.CreateDbContextAsync();
            List<string> candidates = await DataContext.Ingredients.Select(i => i.Name).ToListAsync();
           
            return candidates.Where(x => (x.Contains(matchTo, StringComparison.InvariantCultureIgnoreCase) || matchTo.Contains(x, StringComparison.InvariantCultureIgnoreCase)));
        }

        private void ItemDropUpdateOrder(MudItemDropInfo<RecipeIngredient> dropItem) {
            Console.WriteLine("starting reorder update");
            int originalOrder = dropItem.Item.Order;
            Console.WriteLine($"Original Order: {originalOrder}");
            
            Console.WriteLine($"Parsed Order: {dropItem.Item.Order}");
            var targetOrder = int.Parse(dropItem.DropzoneIdentifier);
            var targetItem = RecipeIngredientsList.FirstOrDefault(ri => ri.Order == targetOrder);

            if (targetItem != null) {
                dropItem.Item.Order = targetOrder;
                targetItem.Order = originalOrder;
            }

        }

        
    }
}

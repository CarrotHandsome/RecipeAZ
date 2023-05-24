using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
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
        //public async Task UpdateAddedIngredient() {
        //    if (ItemRecipe != null && LastItem.Name != null) { 
        //        try {
        //            Console.WriteLine("adding new ingredient");
        //            if (await _recipeService.DoesIngredientExist(LastItem.Name)) {//!await DataContext.Ingredients.AnyAsync(i => i.Name == LastItem.Ingredient.Name)) {
        //                Console.WriteLine("adding new Ingredient");
        //                await DataContext.Ingredients.AddAsync(LastItem.Ingredient);
        //                Console.WriteLine("added new ingredient");
        //            } 
                    
        //            RecipeIngredient newRecipeIngredient = new RecipeIngredient {
        //                Name = LastItem.Name,
        //                Recipe = ItemRecipe,
        //                RecipeId = ItemRecipe.RecipeId,
        //                Ingredient = LastItem.Ingredient,
        //                IngredientId = LastItem.Ingredient.IngredientId,
        //                Before = LastItem.Before,
        //                BeforeId = LastItem.BeforeId ?? "1",
        //                After = LastItem.After,
        //                AfterId = LastItem.AfterId ?? "1"
        //            };
        //        } catch (Exception ex) {
        //            Console.WriteLine($"EXCEPTION: {ex.Message}");
        //        }                
        //            LastItem = new();
        //        //_newRI = new();
        //        //StateHasChanged();
        //        //Console.WriteLine(_newRI.Ingredient.Name);
        //    }
        //}

        private void AddRecipeIngredient(RecipeIngredient ri) {
            _recipeService.AddRecipeIngredient(ri);
            detailsOpen[ri] = ri.Notes != string.Empty;            
            ShowNewIngredientInput = false;                       
            StateHasChanged();
        }
        
        private Ingredient NewIngredient(string name, RecipeIngredient ri) {
            Ingredient ingredient = new Ingredient {
                Name = name,
                RecipeIngredients = new List<RecipeIngredient> { ri }
            };
            _newIngredientToRemove = ingredient;
            //DataContext.Ingredients.Add(ingredient);
            return ingredient;
        }

        private async Task<IEnumerable<string>> FindIngredientMatches(string matchTo) {
            Console.WriteLine("finidng");
            if (string.IsNullOrEmpty(matchTo)) {
                return new List<string>();
            }
            using var DataContext = await _contextFactory.CreateDbContextAsync();
            List<string> candidates = await DataContext.Ingredients.Select(i => i.Name).ToListAsync();
           
            return candidates.Where(x => (x.Contains(matchTo, StringComparison.InvariantCultureIgnoreCase) || matchTo.Contains(x, StringComparison.InvariantCultureIgnoreCase)));
        } 
    }
}

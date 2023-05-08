using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using RecipeAZ.Models;
using System.Diagnostics;
using System.Text.Json;

namespace RecipeAZ.Pages.RecipeComponents {
    public partial class IngredientsComponent {
        protected override void OnInitialized() {
            base.OnInitialized();
            if (ItemRecipe != null && ItemRecipe.RecipeIngredients != null) {
                foreach (RecipeIngredient ri in ItemRecipe!.RecipeIngredients!) {
                    detailsOpen[ri] = false;
                }
            }

            AllIngredients = dataContext.Ingredients.Select(i => i.Name).ToList();
        }
        public async Task UpdateAddedIngredient() {
            if (ItemRecipe != null && LastItem.Name != null) {               
                
                try {
                    Console.WriteLine("adding new ingredient");
                    if (!dataContext.Ingredients.Any(i => i.Name == LastItem.Ingredient.Name)) {
                        Console.WriteLine("adding new Ingredient");
                        await dataContext.Ingredients.AddAsync(LastItem.Ingredient);
                        Console.WriteLine("added new ingredient");
                    } 
                    if (LastItem.Before != null && !dataContext.Modifiers.Any(m => m.Name == LastItem.Before.Name)) {
                        await dataContext.Modifiers.AddAsync(LastItem.Before);
                    }
                    if (LastItem.After != null && !dataContext.Modifiers.Any(m => m.Name == LastItem.After.Name)) {
                        await dataContext.Modifiers.AddAsync(LastItem.After);
                    }
                    RecipeIngredient newRecipeIngredient = new RecipeIngredient {
                        Name = LastItem.Name,
                        Recipe = ItemRecipe,
                        RecipeId = ItemRecipe.RecipeId,
                        Ingredient = LastItem.Ingredient,
                        IngredientId = LastItem.Ingredient.IngredientId,
                        Before = LastItem.Before,
                        BeforeId = LastItem.BeforeId ?? "1",
                        After = LastItem.After,
                        AfterId = LastItem.AfterId ?? "1"
                    };
                   
                    //await dataContext.RecipeIngredients.AddAsync(newRecipeIngredient);
                    Console.WriteLine($"Ingredient Id: {newRecipeIngredient.RecipeIngredientId}");
                    
                } catch (Exception ex) {
                    Console.WriteLine($"EXCEPTION: {ex.Message}");
                }
                
                    LastItem = new();
                //_newRI = new();
                //StateHasChanged();
                //Console.WriteLine(_newRI.Ingredient.Name);
            }
        }

        private void AddRecipeIngredient(RecipeIngredient ri) {
            bool adding = ri.RecipeIngredientId == null;
            Console.WriteLine("recipe ingredient: " + ri.Name);
            Ingredient existingIngredient = dataContext.Ingredients.FirstOrDefault(i => i.Name == ri.Name);
            Console.WriteLine("existing ingredient" + (existingIngredient?.Name ?? "no existing ingredient"));
            if (existingIngredient == null) {
                Console.WriteLine("adding new ingredient");
                ri.Ingredient = new Ingredient {
                    Name = ri.Name                    
                };
                
            } else {
                ri.Ingredient = existingIngredient;
            }            
            if (adding) {
                ri.Order = ItemRecipe.RecipeIngredients.Count() + 1;
                ItemRecipe?.RecipeIngredients?.Add(ri);
            } else {

            }            
            
            detailsOpen[ri] = ri.Notes != string.Empty;
            
            ShowNewIngredientInput = false;           
            
            StateHasChanged();
        }
        private async Task EditIngredient(RecipeIngredient originalRI, RecipeIngredient newRI) {
            
            
        }

        private List<(IngredientModifier?, Ingredient?, IngredientModifier?)> ComposeName(string fullName="Unnamed", bool partials = false) {
            /*
             * Returns an Ingredient and two modifiers whose names compose to equal fullName
             */
            Dictionary<string, Ingredient> ingredientsDict = dataContext.Ingredients.ToDictionary(i => i.Name);
            Dictionary<string, IngredientModifier> beforesDict = dataContext.Modifiers.ToDictionary(im => im.Name);
            Dictionary<string, IngredientModifier> aftersDict = dataContext.Modifiers.ToDictionary(im => im.Name);
            List<(IngredientModifier?, Ingredient?, IngredientModifier?)> returnList = new();
            Console.WriteLine($"Full Name: {fullName}");
            IngredientModifier emptyMod = dataContext.Modifiers.FirstOrDefault(m => m.IngredientModifierId == "1");
            
            for (int i = 0; i < fullName.Length; i++) {
                for (int j = i + 1; j <= fullName.Length; j++) {
                    var beforeName = fullName.Substring(0, i);
                    var ingredientName = fullName.Substring(i, j - i);
                    var afterName = fullName.Substring(j);
                    IngredientModifier? before = emptyMod;
                    Ingredient? ingredient = null;
                    IngredientModifier? after = emptyMod;
                    
                    if ((beforesDict.TryGetValue(beforeName, out before) && beforeName != string.Empty) ||
                        (aftersDict.TryGetValue(afterName, out after) && afterName != string.Empty) ||
                        (ingredientsDict.TryGetValue(ingredientName, out ingredient) && ingredientName != string.Empty)) {
                        // Found a match
                        if (before == emptyMod && beforeName != string.Empty) {
                            before = new IngredientModifier {
                                Name = beforeName
                            };
                        }
                        if (ingredient == null && ingredientName != string.Empty) {
                            ingredient = new Ingredient {
                                Name = ingredientName
                            };
                        }
                        if (after == emptyMod && afterName != string.Empty) {
                            after = new IngredientModifier {
                                Name = afterName
                            };
                        }
                        Console.WriteLine($"{ingredientName}");
                        returnList.Add((before, ingredient, after));
                    }
                }
            }
            
            return returnList;
        }
        private Ingredient NewIngredient(string name, RecipeIngredient ri) {
            Ingredient ingredient = new Ingredient {
                Name = name,
                RecipeIngredients = new List<RecipeIngredient> { ri }
            };
            _newIngredientToRemove = ingredient;
            //dataContext.Ingredients.Add(ingredient);
            return ingredient;
        }

        private async Task<IEnumerable<string>> FindIngredientMatches(string matchTo) {
            Console.WriteLine("finidng");
            if (string.IsNullOrEmpty(matchTo)) {
                return new List<string>();
            }
            List<string> candidates = await dataContext.Ingredients.Select(i => i.Name).ToListAsync();
           
            return candidates.Where(x => (x.Contains(matchTo, StringComparison.InvariantCultureIgnoreCase) || matchTo.Contains(x, StringComparison.InvariantCultureIgnoreCase)));
        } 
    }
}

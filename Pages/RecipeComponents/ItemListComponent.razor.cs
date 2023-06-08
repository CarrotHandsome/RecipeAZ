using MudBlazor;
using RecipeAZ.Interfaces;
using RecipeAZ.Models;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;

namespace RecipeAZ.Pages.RecipeComponents {
    public partial class ItemListComponent<ItemType> where ItemType : IEditableListItem<ItemType>, new() {

        protected override async Task OnInitializedAsync() { 
            await base.OnInitializedAsync();           
            
            _typeName = typeof(ItemType).Name;
            if (ItemRecipe != null) {
                foreach (ItemType ri in ItemRecipe!.GetItems<ItemType>()!) {
                    _detailsOpen[ri] = false;
                }
            }
            _allItems = _typeName == "RecipeIngredient" ? await _recipeService.GetAllIngredientNames() : new();
            _typeNameSimple = _typeName == "RecipeIngredient" ? "ingredient" : "step";            
        }
        private async Task AddItem(IEditableListItem<ItemType> ri) {
            await _recipeService.AddItem(ri);
            if (_detailsOpen.ContainsKey(ri)) {
                _detailsOpen[ri] = ri.Details != string.Empty;
            } else {
                _detailsOpen.Add(ri, ri.Details != string.Empty);
            }
            
            ShowNewItemInput = false;
            LastItem = new ItemType();
            
        }
        public void ItemDropUpdateOrder<T>(MudItemDropInfo<IEditableListItem<T>> dropItem) where T : new() {
            
            //Console.WriteLine("starting reorder update");
            int originalOrder = dropItem.Item.Order;
            //Console.WriteLine($"Original Order: {originalOrder}");

            //Console.WriteLine($"Parsed Order: {dropItem.Item.Order}");
            var targetOrder = int.Parse(dropItem.DropzoneIdentifier);
            var targetItem = ItemsList.FirstOrDefault(ri => ri.Order == targetOrder);

            if (targetItem != null) {
                dropItem.Item.Order = targetOrder;
                targetItem.Order = originalOrder;
            }

        }
        
        public async Task<IEnumerable<string>> FindItemMatchesSearchFunc(string input) {
            if (_typeName == "RecipeIngredient") {
                using var context = await _contextFactory.CreateDbContextAsync();
                if (string.IsNullOrEmpty(input)) {
                    return new List<string>();
                }
                List<string> candidates = await context.Ingredients.Select(i => i.Name).ToListAsync();

                return candidates.Where(x => (x.Contains(input, StringComparison.InvariantCultureIgnoreCase) || input.Contains(x, StringComparison.InvariantCultureIgnoreCase)));
            } else {
                return new List<string>();
            }
        }        
    }
}

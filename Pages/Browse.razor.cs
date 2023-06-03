using Microsoft.AspNetCore.WebUtilities;
using RecipeAZ.Pages.RecipeComponents;
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

namespace RecipeAZ.Pages {
    public partial class Browse {
       



        protected override async Task OnInitializedAsync() {
            await base.OnInitializedAsync();

            _searchText = Terms ?? string.Empty;
            _recipes = await SearchResults();
            var uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);
            string tag = string.Empty;
            string ing = string.Empty;

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("tag", out var tagValue)) {
                tag = tagValue.First() == null ? "" : tagValue.First();
            }
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("ing", out var ingValue)) {
                ing = ingValue.First() == null ? "" : ingValue.First();
            }

            using (var context = _contextFactory.CreateDbContext()) {
                var tagToAdd = context.Tags.Where(t => t.Name == tag).FirstOrDefault();
                if (tagToAdd != null) {
                    _tagsFilter.Add(tagToAdd);
                }                
                var ingredientToAdd = context.Ingredients.Where(i => i.Name == ing).FirstOrDefault();
                if (ingredientToAdd != null) {
                    _ingredientsFilter.Add(ingredientToAdd);
                }                
            }
            await UpdateRecipes();
            
        }

        private async Task UpdateRecipes() {
            _recipes = await SearchResults();
            foreach(Tag r in _tagsFilter) {
                Console.WriteLine($"FILTER: {r.Name}");
            }
            foreach (Recipe r in _recipes) {
                Console.WriteLine(r.Name);
                foreach (RecipeTag rt in r.RecipeTags) {
                    Console.WriteLine(rt.Tag.Name);
                }
                Console.WriteLine($"{r.RecipeTags.Any(rt => _tagsFilter.Contains(rt.Tag))}");
            }
            _recipes = _recipes.Where(r =>
                (_tagsFilter.Any() ? _tagsFilter.All(tag => r.RecipeTags.Any(rt => rt.Tag.TagId == tag.TagId)) : true) &&
                (_ingredientsFilter.Any() ? _ingredientsFilter.All(ingredient => r.RecipeIngredients.Any(ri => ri.Ingredient.IngredientId == ingredient.IngredientId)) : true)
            ).ToList();


            foreach (Recipe r in _recipes) {
                Console.WriteLine($"AFTER FILTER: {r.Name}");
            }
            StateHasChanged();

        }
        private async Task<List<Recipe>> SearchResults() {
            {
                List<Recipe> recipes = await _recipeService.GetRecipesAsync(
                    
                    (r => r.Name.ToLower().Contains(_searchText.ToLower())
                || r.RecipeIngredients.Any(ri => ri.Ingredient.Name.ToLower().Contains(_searchText.ToLower()))
                || r.RecipeTags.Any(rt => rt.Tag.Name.ToLower().Contains(_searchText)) || string.IsNullOrEmpty(_searchText.ToLower())),
                _searchText, -1);

                switch (orderIconIndex % _orderIconOptions.Length) {
                    case 0:
                        if (ResultsAscending) {
                            recipes = recipes.OrderBy(r => r.Name).ToList();
                        } else {
                            recipes = recipes.OrderByDescending(r => r.Name).ToList();
                        }
                        break;
                    case 1:
                        if (ResultsAscending) {
                            recipes = recipes.OrderBy(r => r.CreatedAt).ToList();
                        } else {
                            recipes = recipes.OrderByDescending(r => r.CreatedAt).ToList();
                        }
                        break;
                    case 2:
                        if (ResultsAscending) {
                            recipes = recipes.OrderBy(r => r.UsersWhoLikeMe?.Count ?? 0).ToList();
                        } else {
                            recipes = recipes.OrderByDescending(r => r.UsersWhoLikeMe?.Count ?? 0).ToList();
                        }
                        break;
                    default:
                        recipes = recipes.OrderBy(r => r.Name).ToList();
                        break;
                }
                return recipes;
            }
        }

        private async Task<IEnumerable<string>> TagAutoCompleteSearch(string value) {
            if (string.IsNullOrEmpty(value)) {
                return new List<string>();
            }
            List<string> filteredTags;
            try {
                using (var context = await _contextFactory.CreateDbContextAsync()) {
                    filteredTags = await context.Tags.Where(t => t.Name.ToLower().Contains(value.ToLower()))
                    .Select(t => t.Name)
                    .ToListAsync();
                }

            } catch (Exception e) {
                Console.WriteLine(e);
                filteredTags = new();
            }
            return filteredTags;
        }
        private async Task<IEnumerable<string>> IngredientAutoCompleteSearch(string value) {
            if (string.IsNullOrEmpty(value)) {
                return new List<string>();
            }
            List<string> filteredIngredients;
            try {
                using (var context = await _contextFactory.CreateDbContextAsync()) {
                    filteredIngredients = await context.Ingredients.Where(i => i.Name.ToLower().Contains(value.ToLower()))
                    .Select(t => t.Name)
                    .ToListAsync();
                }

            } catch (Exception e) {
                Console.WriteLine(e);
                filteredIngredients = new();
            }
            return filteredIngredients;
        }
        private void UpdateTagSearchText(string value) {
            TagFilterSearchText = value;
        }
        private void UpdateIngredientSearchText(string value) {
            IngredientFilterSearchText = value;
        }
        private async Task AddTagFilter() {
            Tag tag = null;
            using (var context = await _contextFactory.CreateDbContextAsync()) {
                tag = context.Tags
                .Where(t => t.Name.ToLower() == TagFilterSearchText.ToLower()).FirstOrDefault();

                if (tag != null) {
                    _tagsFilter.Add(tag);
                    await UpdateRecipes();
                    
                    UpdateTagSearchText(string.Empty);
                }
            }
        }
        private async Task AddIngredientFilter() {
            Ingredient ingredient = null;
            using (var context = await _contextFactory.CreateDbContextAsync()) {
                ingredient = context.Ingredients
                    .Where(ingredient => ingredient.Name.ToLower() == IngredientFilterSearchText.ToLower()).FirstOrDefault();
            }

            if (ingredient != null) {
                _ingredientsFilter.Add(ingredient);
                await UpdateRecipes();
            }
        }
        
        private async Task HandleTagFilterClose(Tag tag) {
            _tagsFilter.Remove(tag);
            await UpdateRecipes();
            StateHasChanged();
        }
        private async Task HandleIngredientFilterClose(Ingredient ingredient) {
            _ingredientsFilter.Remove(ingredient);
            await UpdateRecipes();
            StateHasChanged();
        }

        private void NavigateToRecipe(string id) {
            _navigationManager.NavigateTo($"/recipe/{id}");
        }
        private void NavigateToUser(string id) {
            _navigationManager.NavigateTo($"/profile/{id}");
        }

        private async Task ChangeOrderIcon() {
            orderIconIndex++;
            _recipes = await SearchResults();
            StateHasChanged();
        }

        
    }
}

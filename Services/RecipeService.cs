﻿using Microsoft.EntityFrameworkCore;
using RecipeAZ.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace RecipeAZ.Services {
    public class RecipeService {
        public IDbContextFactory<DataContext> _contextFactory { get; set; }
        public DataContext DataContext { get; set; }
        public Recipe Recipe { get; set; }
        public RecipeService(DataContext context, IDbContextFactory<DataContext> factory) {
            _contextFactory = factory;
            DataContext = context;
        }
        public async Task<Recipe> GetRecipeAsync(string id) {
            
            return await DataContext.Recipes
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)                
                .Include(r => r.RecipeSteps)
                .Include(r => r.User)
                    .ThenInclude(u => u.RecipesILike)
                .Include(r => r.UsersWhoLikeMe)
                .Include(r => r.Comments)
                    .ThenInclude(c => c.User)
                .Include(r => r.RecipeTags)
                    .ThenInclude(rt => rt.Tag)
                .Include(r => r.ParentRecipe)
                .FirstOrDefaultAsync(r => r.RecipeId == id);
        }
        public async Task<Recipe> LoadRecipeAsync(string id, AppUser user) {
            Recipe = await GetRecipeAsync(id);
            if (Recipe == null) {
                Recipe = NewRecipe(user);
            }
            return Recipe;

        }

        public Recipe NewRecipe(AppUser user) {
            Recipe = new Recipe {
                Name = "New Recipe",
                Description = "description",
                Notes = "notes",
                RecipeIngredients = new List<RecipeIngredient>(),
                RecipeSteps = new List<RecipeStep>(),
                RecipeTags = new List<RecipeTag>(),
                UserId = user?.Id
            };
            return Recipe;
        }
        
        public async Task<Recipe> SaveRecipeAsync(string id) {            
            try {
                if (id != null) {
                    Console.WriteLine("updating recipe");
                    DataContext.Recipes.Update(Recipe);                    
                } else {
                    await DataContext.Recipes.AddAsync(Recipe);                    
                }
                await DataContext.SaveChangesAsync();
            } catch (Exception ex) {
                Console.WriteLine($"EXCEPTION: {ex}");                
            }
            return Recipe;
        }
        public Recipe SaveRecipe(string id) {
            try {
                if (id != null) {
                    Console.WriteLine("updating recipe");
                    DataContext.Recipes.Update(Recipe);
                } else {
                    DataContext.Recipes.Add(Recipe);
                }
                DataContext.SaveChanges();
            } catch (Exception ex) {
                Console.WriteLine($"EXCEPTION: {ex}");
            }
            return Recipe;
        }
        public async Task RemoveRecipe(Recipe recipe) {
            //using var context = await _contextFactory.CreateDbContextAsync();
            DataContext.Recipes.Remove(recipe);
            await DataContext.SaveChangesAsync();
        }
        public async Task<List<string>> GetAllIngredientNames() {
            //using var context = await _contextFactory.CreateDbContextAsync();
            return DataContext.Ingredients.Select(i => i.Name).ToList();
        }
        public async Task<bool> DoesIngredientExist(string name) {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Ingredients.AnyAsync(i => i.Name == name);
        }
        public async Task<IEnumerable<string>> FindIngredientMatches(string matchTo) {
            using var context = await _contextFactory.CreateDbContextAsync();
            Console.WriteLine("finidng");
            if (string.IsNullOrEmpty(matchTo)) {
                return new List<string>();
            }
            List<string> candidates = await context.Ingredients.Select(i => i.Name).ToListAsync();

            return candidates.Where(x => (x.Contains(matchTo, StringComparison.InvariantCultureIgnoreCase) || matchTo.Contains(x, StringComparison.InvariantCultureIgnoreCase)));
        }

        public async Task AddRecipeIngredient(RecipeIngredient ri) {
            bool adding = ri.RecipeIngredientId == null;
            Ingredient existingIngredient = DataContext.Ingredients.FirstOrDefault(i => i.Name == ri.Name);
            if (existingIngredient == null) {
                ri.Ingredient = new Ingredient {
                    Name = ri.Name
                };
            } else {
                ri.Ingredient = existingIngredient;
            }
            if (adding) {
                ri.Order = Recipe.RecipeIngredients.Count() + 1;
                Recipe.RecipeIngredients.Add(ri);
            }
        }
        public async Task RemoveRecipeIngredient(RecipeIngredient ri) {
            //List<RecipeIngredient> befores = DataContext.RecipeIngredients.Where(before => before.RecipeId == ri.RecipeId && ri.Order > before.Order).ToList();
            List<RecipeIngredient> afters = DataContext.RecipeIngredients.Where(after => after.RecipeId == ri.RecipeId && ri.Order < after.Order).ToList();
            foreach (RecipeIngredient after in afters) {
                after.Order = after.Order - 1;
            }
            DataContext.RecipeIngredients.Attach(ri);
            DataContext.RecipeIngredients.Remove(ri);
            Recipe.RecipeIngredients.Remove(ri);
            DataContext.Recipes.Update(Recipe);
        }
        public async Task RemoveRecipeStep(Recipe recipe, RecipeStep rs) {
            List<RecipeStep> afters = DataContext.RecipeSteps.Where(after => after.RecipeId == rs.RecipeId && rs.Order < after.Order).ToList();
            foreach (RecipeStep after in afters) {
                after.Order = after.Order - 1;
            }
            DataContext.RecipeSteps.Attach(rs);
            DataContext.RecipeSteps.Remove(rs);
            recipe.RecipeSteps.Remove(rs);
            DataContext.Recipes.Update(Recipe);
        }
        public void AddLike(RecipeLike rl) {
            Recipe.UsersWhoLikeMe.Add(rl);
        }
        public void RemoveLike(AppUser user) {
            var existingRl = DataContext.Find<RecipeLike>(user.Id, Recipe.RecipeId);
            if (existingRl != null) {
                Recipe.UsersWhoLikeMe.Remove(existingRl);
            }
        }

        public async Task AddTag(string newTagName) {
            if (!string.IsNullOrEmpty(newTagName)) {
                Console.WriteLine("adding tag");

                Tag tagToAdd;
                Tag existingTag;                
                Console.WriteLine("finding existing tag");
                existingTag = await DataContext.Tags.FirstOrDefaultAsync(t => t.Name == TextProcessing.ToTitleCase(newTagName));
                if (existingTag == null) {
                    Console.WriteLine("no existing tag");
                    tagToAdd = new Tag { Name = TextProcessing.ToTitleCase(newTagName) };
                    DataContext.Tags.Add(tagToAdd);
                    //await context.SaveChangesAsync();
                } else {
                    Console.WriteLine("found existing tag");
                    tagToAdd = existingTag;
                }
                
                Console.WriteLine("Added tag or found existing one");
                if (!DataContext.RecipeTags.Any(rt => rt.RecipeId == Recipe.RecipeId && rt.TagId == tagToAdd.TagId)) {
                    RecipeTag recipeTag = new RecipeTag { RecipeId = Recipe.RecipeId, Tag = tagToAdd, TagId = tagToAdd.TagId };
                    //_recipeService.DataContext.RecipeTags.Add(recipeTag);
                    Recipe.RecipeTags.Add(recipeTag);
                    //await _recipeService.DataContext.SaveChangesAsync();
                }
            }
        }
        public async Task AddComment(Comment comment) {
            try {
                DataContext.Comments.Add(comment);
                await SaveRecipeAsync(Recipe.RecipeId);
            } catch (Exception e) {
                Console.WriteLine("EXCEPTION:" + e);
            }
            
        }
        public async Task<List<Recipe>> GetRecipesAsync(Expression<Func<Recipe, bool>> filter, string searchText, int minLength=2) {
            //using var DataContext = await _contextFactory.CreateDbContextAsync();
            if (searchText.Length > minLength || minLength == -1) {
                searchText = searchText.ToLower();
                using (var context = await _contextFactory.CreateDbContextAsync()) {
                    return await context.Recipes
                    .Include(r => r.RecipeIngredients)
                    .Include(r => r.User)
                    .Include(r => r.RecipeTags)
                        .ThenInclude(rt => rt.Tag)
                    .Include(r => r.User)
                    .Include(r => r.RecipeIngredients)
                        .ThenInclude(ri => ri.Ingredient)
                    .Include(r => r.RecipeTags)
                        .ThenInclude(rt => rt.Tag)
                    .Include(r => r.UsersWhoLikeMe)
                    .Where(filter).Select(r => r).ToListAsync() ?? new List<Recipe>();
                }                
            }
            
            return new List<Recipe>();
            
            
            //return await DataContext.Recipes
            //    .Include(r => r.RecipeIngredients)
            //    .Include(r => r.User)
            //    .Include(r => r.RecipeTags)
            //        .ThenInclude(rt => rt.Tag)
            //    .Where(r => r.Name.ToLower().Contains(searchText)
            //    || r.RecipeIngredients.Any(ri => ri.Name.ToLower().Contains(searchText))
            //    || r.RecipeTags.Any(rt => rt.Tag.Name.ToLower().Contains(searchText)))
            //    .Select(r => r).ToListAsync();

        }

        public async Task<IEnumerable<Recipe>> GetRelatedRecipesByIngredientAsync(Recipe originalRecipe) {
            var originalIngredientIds = originalRecipe?.RecipeIngredients?.Select(ri => ri.IngredientId).ToList();
            using (var context = await _contextFactory.CreateDbContextAsync()) {
                return await context.Recipes
                .Include(r => r.RecipeIngredients)
                .Include(r => r.User)
                .Where(r => r.RecipeIngredients != null && r.RecipeIngredients.Any(ri => originalIngredientIds.Contains(ri.IngredientId)
                    && r.RecipeId != originalRecipe.RecipeId)).ToListAsync();
            }
            
        }
        public async Task<IEnumerable<Recipe>> GetRelatedRecipesByTagAsync(Recipe originalRecipe) {
            Console.WriteLine("trying to get related by tag");
            Console.WriteLine($"originalRecipe recipetags is null: {originalRecipe.RecipeTags == null}");
            var originalTagIds = originalRecipe?.RecipeTags?.Select(rt => rt.TagId).ToList();
            Console.WriteLine("made list of tags");
            using (var context = await _contextFactory.CreateDbContextAsync()) {
                Console.WriteLine("using disposable context");
                try {
                    return await context.Recipes
                    .Include(r => r.RecipeTags)
                    .Include(r => r.User)
                    .Where(r => r.RecipeTags != null && r.RecipeTags.Any(rt => originalTagIds.Contains(rt.TagId)
                        && r.RecipeId != originalRecipe.RecipeId)).ToListAsync() ?? new List<Recipe>();
                } catch(Exception e) {
                    Console.WriteLine("EXCEPTION: " + e);
                    return new List<Recipe>();
                }
                
            }                
        }

        public async Task<Dictionary<Recipe, int>> GetRelatedRecipesByIngredientWeightedAsync(Recipe originalRecipe) {
            Dictionary<Recipe, int> weightedList= new Dictionary<Recipe, int>();
            var originalIngredientIds = originalRecipe?.RecipeIngredients?.Select(ri => ri.IngredientId).ToList();

            foreach (Recipe r in await GetRelatedRecipesByIngredientAsync(originalRecipe) ?? new List<Recipe>()) {
                weightedList[r] = 0;
                foreach (RecipeIngredient ri in r?.RecipeIngredients ?? new List<RecipeIngredient>()) {
                    if (originalIngredientIds.Contains(ri.IngredientId)) {
                        weightedList[r]++;
                    }
                }                
            }
            return weightedList;
        }

        public async Task<Dictionary<Recipe, int>> GetRelatedRecipesByTagWeightedAsync(Recipe originalRecipe) {
            Dictionary<Recipe, int> weightedList = new Dictionary<Recipe, int>();
            Console.WriteLine($"Original REcipe is null: {originalRecipe == null}");
            var originalTagIds = originalRecipe?.RecipeTags?.Select(rt => rt.TagId).ToList();
            Console.WriteLine($"Original tags is null: {originalTagIds == null}");
            foreach (Recipe r in await GetRelatedRecipesByTagAsync(originalRecipe) ?? new List<Recipe>()) {
                weightedList[r] = 0;
                foreach (RecipeTag rt in r?.RecipeTags ?? new List<RecipeTag>()) {
                    if (originalTagIds.Contains(rt.TagId)) {
                        weightedList[r]++;
                    }
                }
            }
            return weightedList;
        }
        public async Task<Dictionary<Recipe, int>> GetRelatedRecipesWeightedAsync(Recipe originalRecipe,
            int tagWeight=2, int ingredientWeight=1) {
            
            Dictionary<Recipe, int> tagWeighted = await GetRelatedRecipesByTagWeightedAsync(originalRecipe);
            Dictionary<Recipe, int> ingredientWeighted = await GetRelatedRecipesByIngredientWeightedAsync(originalRecipe);
            Dictionary<Recipe, int> weightedList = new();

            foreach (KeyValuePair<Recipe, int> kvp in tagWeighted) {
                if (!weightedList.ContainsKey(kvp.Key)) {
                    weightedList[kvp.Key] = 0;
                }
                weightedList[kvp.Key] += kvp.Value * tagWeight;
            }
            foreach (KeyValuePair<Recipe, int> kvp in ingredientWeighted) {
                if (!weightedList.ContainsKey(kvp.Key)) {
                    weightedList[kvp.Key] = 0;
                }
                weightedList[kvp.Key] += kvp.Value * ingredientWeight;
            }
            Console.WriteLine($"LIST COUNT: {weightedList.Count}");
            return weightedList;
        }
    }
}

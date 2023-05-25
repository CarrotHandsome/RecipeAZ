using Microsoft.EntityFrameworkCore;
using RecipeAZ.Models;

namespace RecipeAZ.Services {
    public class RecipeService {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private DataContext DataContext { get; set; }
        public Recipe Recipe { get; set; }
        public RecipeService(DataContext context) {
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
            DataContext.RecipeIngredients.Attach(ri);
            DataContext.RecipeIngredients.Remove(ri);
            Recipe.RecipeIngredients.Remove(ri);
            DataContext.Recipes.Update(Recipe);
        }
        public async Task RemoveRecipeStep(Recipe recipe, RecipeStep rs) {
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
        public async Task AddComment(Comment comment) {
            try {
                DataContext.Comments.Add(comment);
                await SaveRecipeAsync(Recipe.RecipeId);
            } catch (Exception e) {
                Console.WriteLine("EXCEPTION:" + e);
            }
            
        }
        public async Task<IEnumerable<Recipe>> SearchRecipes(string searchText) {
            //using var DataContext = await _contextFactory.CreateDbContextAsync();
            if (searchText.Length < 2) {
                return null;
            }
            searchText = searchText.ToLower();
            return await DataContext.Recipes
                .Include(r => r.RecipeIngredients)
                .Include(r => r.User)
                .Include(r => r.RecipeTags)
                    .ThenInclude(rt => rt.Tag)
                .Where(r => r.Name.ToLower().Contains(searchText)
                || r.RecipeIngredients.Any(ri => ri.Name.ToLower().Contains(searchText))
                || r.RecipeTags.Any(rt => rt.Tag.Name.ToLower().Contains(searchText)))
                .Select(r => r).ToListAsync();

        }
    }
}

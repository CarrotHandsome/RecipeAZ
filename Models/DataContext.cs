using Microsoft.EntityFrameworkCore;


namespace RecipeAZ.Models {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> opts)
            : base(opts) { }

        public DbSet<Recipe> Recipes => Set<Recipe>();
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
        public DbSet<RecipeStep> RecipeSteps => Set<RecipeStep>();     
        public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();
        public DbSet<Comment> Comments => Set<Comment>();
    }
}

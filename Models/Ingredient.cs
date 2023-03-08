namespace RecipeAZ.Models {
    public class Ingredient {
        public long IngredientId { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Recipe>? Recipes { get; set; }
    }
}

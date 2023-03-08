namespace RecipeAZ.Models {
    public class RecipeIngredient {
        public long RecipeIngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }
        public Recipe? Recipe { get; set; }
        public string Amount { get; set; } = "";
        public string ModifierPre { get; set; } = "";
        public string ModifierPost { get; set; } = "";

    }
}
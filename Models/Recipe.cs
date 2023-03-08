namespace RecipeAZ.Models {
    public class Recipe {
        public long RecipeId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        public List<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
        public List<RecipeStep>? Steps { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}

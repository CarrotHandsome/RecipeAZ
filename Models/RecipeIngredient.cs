using RecipeAZ.Interfaces;

namespace RecipeAZ.Models {
    public class RecipeIngredient : IEditableListItem {
        public string RecipeIngredientId { get; set; }
        public string RecipeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public Recipe? Recipe { get; set; }
        public string Amount { get; set; } = string.Empty;
        


    }
}
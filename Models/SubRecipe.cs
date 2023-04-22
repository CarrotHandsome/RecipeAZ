using RecipeAZ.Interfaces;

namespace RecipeAZ.Models {
    public class SubRecipe : IEditableListItem {
        public string SubRecipeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Recipe? Recipe { get; set; }
        public string RecipeId { get; set; }
        public ICollection<RecipeStep>? RecipeSteps { get; set; }
        public int Order { get; set; }
    }
}

using RecipeAZ.Interfaces;

namespace RecipeAZ.Models {
    public class RecipeStep : IEditableListItem {
        public string RecipeStepId { get; set; }
        public string Name { get; set; } = string.Empty;        
        public string Description { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public string RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
        public int Order { get; set; }
    }
}

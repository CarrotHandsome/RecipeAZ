using System.ComponentModel.DataAnnotations;

namespace RecipeAZ.Models {
    public class RecipeBindingTarget {
        [Required]
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Notes { get; set; } = "";
        public List<RecipeIngredient>? Ingredients { get; set; }
        public List<RecipeStep>? Steps { get; set; }

        public Recipe ToRecipe() => new Recipe() {
            Name = this.Name,
            Description = this.Description,
            Notes = this.Notes,
            Ingredients = this.Ingredients ?? new List<RecipeIngredient>(),
            Steps = this.Steps
        };
    }
}

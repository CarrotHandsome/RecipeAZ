using System.ComponentModel.DataAnnotations;

namespace RecipeAZ.Models {
    public class RecipeBindingTarget {
        [Required]
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Notes { get; set; } = "";
        public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
        public ICollection<RecipeStep>? RecipeSteps { get; set; }

        public Recipe ToRecipe() => new Recipe() {
            Name = this.Name,
            Description = this.Description,
            Notes = this.Notes,
            RecipeIngredients = this.RecipeIngredients ?? new List<RecipeIngredient>(),
            RecipeSteps = this.RecipeSteps
        };
    }
}

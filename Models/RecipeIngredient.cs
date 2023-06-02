using RecipeAZ.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace RecipeAZ.Models {


    public class RecipeIngredient : IEditableListItem<RecipeIngredient> {
        public string RecipeIngredientId { get; set; }
        public Recipe? Recipe { get; set; }
        public string RecipeId { get; set; }
        public Ingredient? Ingredient { get; set; }
        public string IngredientId { get; set; }
        //public IngredientModifier Before { get; set; }
        //public string? BeforeId { get; set; } = "1";
        //public IngredientModifier After { get; set; }
        //public string? AfterId { get; set; } = "1";
        public string Name { get; set; } = string.Empty;        
        public string Description { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;        
        public int Order { get; set; }

        //public string FullName {
        //    get {
        //        string before = Before?.Name ?? string.Empty;
        //        string after = After?.Name ?? string.Empty;
        //        return $"{before} {Ingredient?.Name ?? string.Empty} {after}";
        //    }
        //}
    }
}
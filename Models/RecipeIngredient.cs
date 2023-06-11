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
        public string Name { get; set; } = string.Empty;      
        public string Description { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;        
        public int Order { get; set; }
        public bool Optional { get; set; }

        
    }
}
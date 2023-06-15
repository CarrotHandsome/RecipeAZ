using Microsoft.EntityFrameworkCore;
using RecipeAZ.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeAZ.Models
{
    public class Recipe {
        public string RecipeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; //aka Overview
        public string Details { get; set; } = string.Empty;
        public string? UserId { get; set; } = string.Empty;
        public string? ImagePath { get; set; } = "recipeaz-images/dd870e50-f702-43c9-8282-b3ed92dd4033.png";
        public AppUser? User { get; set; }
        public string? ParentRecipeId { get; set; }
        public Recipe? ParentRecipe { get; set; }
        public ICollection<Recipe>? ChildRecipes { get;set; }
        public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
        public ICollection<RecipeStep>? RecipeSteps { get; set; } 
        public ICollection<Comment>? Comments { get; set; }
        public HashSet<RecipeLike>? UsersWhoLikeMe { get; set; }
        public ICollection<RecipeTag> RecipeTags { get; set; } = new List<RecipeTag>();

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
        
        public List<IEditableListItem<T>> GetItems<T>() where T : new() {
            string type = typeof(T).Name;
            if (type == "RecipeIngredient") {
                return RecipeIngredients?.Cast<IEditableListItem<T>>().ToList() ?? new List<IEditableListItem<T>>();
            } else {
                return RecipeSteps?.Cast<IEditableListItem<T>>().ToList() ?? new List<IEditableListItem<T>>();
            }
        }
    }
}

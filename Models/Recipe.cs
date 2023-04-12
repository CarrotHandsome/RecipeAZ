using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeAZ.Models
{
    public class Recipe {
        public string RecipeId { get; set; }
            
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string? UserId { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public AppUser? User { get; set; }
        public ICollection<RecipeIngredient>? RecipeIngredients { get; set; } 
        public ICollection<RecipeStep>? RecipeSteps { get; set; } 
        public ICollection<Comment>? Comments { get; set; }
        public HashSet<RecipeLike>? UsersWhoLikeMe { get; set; }
        public ICollection<RecipeTag> RecipeTags { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
        
    }
}

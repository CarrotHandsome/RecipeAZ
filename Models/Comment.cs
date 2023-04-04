using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeAZ.Models
{

    public class Comment {       
        public string CommentId { get; set; }
        public string UserId { get; set; }
        public AppUser? User { get; set; }
        public string RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public string Text { get; set; } = "";       
        public DateTime CreatedAt { get; set; }
    }
}
namespace RecipeAZ.Models {
    
    public class Comment {
        public long CommentId { get; set; }        
        public Recipe? Recipe { get; set; }
        public string Text { get; set; } = "";
    }
}
namespace RecipeAZ.Models {
    public class RecipeLike {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public string RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

namespace RecipeAZ.Models {
    public class Tag {
        public string TagId { get; set; }
        public string Name { get; set; }
        public ICollection<RecipeTag> RecipeTags { get; set; }
    }
}

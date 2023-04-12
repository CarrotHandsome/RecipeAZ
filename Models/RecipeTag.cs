namespace RecipeAZ.Models {
    public class RecipeTag {
        public string RecipeId { get; set; }
        public string TagId { get; set; }
        public Recipe Recipe { get; set; }  
        public Tag Tag { get; set; }
    }
}

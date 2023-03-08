namespace RecipeAZ.Models {
    public class RecipeStep {
        public long RecipeStepId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;        
        public long RecipeId { get; set; }
    }
}

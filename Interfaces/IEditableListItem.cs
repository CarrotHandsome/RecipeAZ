using RecipeAZ.Models;

namespace RecipeAZ.Interfaces {
    public interface IEditableListItem<T> where T : new() {
        public string Name { get; set; }    
        public string Description { get; set; }
        public string Details { get; set; }
        public int Order { get; set; }
        public Recipe Recipe { get; set; }        
    }
}

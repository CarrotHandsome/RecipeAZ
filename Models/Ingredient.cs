
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RecipeAZ.Models {
    public class Ingredient {
        [Key]
        public string IngredientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
        //public ICollection<IngredientModifier> Befores { 
        //    get {
        //        if (RecipeIngredients == null) {
        //            return new List<IngredientModifier>();
        //        }
        //        return RecipeIngredients.Select(ri => ri.Before).Where(b => b != null && b.IngredientModifierId != "1").ToList() ?? new List<IngredientModifier>();
        //    } 
        //}
        //public ICollection<IngredientModifier> Afters {
        //    get {
        //        if (RecipeIngredients == null) {
        //            return new List<IngredientModifier>();
        //        }
        //        return RecipeIngredients.Select(ri => ri.Before).Where(a => a != null && a.IngredientModifierId != "1").ToList() ?? new List<IngredientModifier>();
        //        }
        //}
    }
    //public class IngredientModifier {
    //    public string IngredientModifierId { get; set; }
    //    public string Name { get; set; }
    //    public bool IsBefore { get; set; } = false;
    //    public ICollection<RecipeIngredient>? BeforeRecipeIngredients { get; set; } = new List<RecipeIngredient>();
    //    public ICollection<RecipeIngredient>? AfterRecipeIngredients { get; set; } = new List<RecipeIngredient>();

    //}
}

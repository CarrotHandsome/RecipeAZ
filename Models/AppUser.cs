using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RecipeAZ.Models;

// Add profile data for application users by adding properties to the AppUser class
public class AppUser : IdentityUser
{
    public ICollection<Recipe>? Recipes { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public HashSet<RecipeLike>? RecipesILike { get; set; }
}


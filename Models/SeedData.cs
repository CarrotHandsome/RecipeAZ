using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipeAZ.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Runtime.CompilerServices;

public static class SeedData {
    public static async Task InitializeAsync(UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager, DataContext dataContext) {

        //await dataContext.Database.MigrateAsync();
        dataContext.Database.EnsureCreated();

        //Comment c = new Comment {
        //    CommentId = "1",            
        //    Text = "nice recipe"
        //};

        //AppUser? user = await GetUser(roleManager, userManager);
        //user!.Comments = new List<Comment> { c };
        //await userManager.UpdateAsync(user);
        
        //await userManager.UpdateAsync(user!);
        //dataContext.ChangeTracker.Clear();  
        
       
        //if (dataContext.Recipes.Count() == 0) {
        //    RecipeIngredient ri1 = GetRecipeIngredient(dataContext, "1", "Lentils", "4 cups", "1");
        //    RecipeIngredient ri2 = GetRecipeIngredient(dataContext, "2", "Water", "to cover", "1");
        //    RecipeStep rs1 = GetRecipeStep(dataContext, "1", "Step 1", "Rinse Lentils", "1");
        //    RecipeStep rs2 = GetRecipeStep(dataContext, "2", "Step 2", "Add lentils to pot and add water", "1");
        //    RecipeStep rs3 = GetRecipeStep(dataContext, "3", "Step 3", "Bring to boil till cooked", "1");
            

        //    Console.WriteLine("adding steps and ingredients to dataContext...");
        //    //dataContext.RecipeIngredients.Add(ri1);
        //    //dataContext.RecipeIngredients.Add(ri2);
        //    //dataContext.RecipeSteps.Add(rs1);
        //    //dataContext.RecipeSteps.Add(rs2);
        //    //dataContext.Comments.Add(c);

        //    Console.WriteLine("added.");

        //    Recipe recipe = new Recipe {
        //        RecipeId = "1",
        //        Name = "Tarka Dal",
        //        Description = "Nulla turpis risus, mollis sed mi non, congue posuere enim. Fusce vehicula ligula nec nibh vehicula tempor. Maecenas accumsan quis mauris a imperdiet. Phasellus venenatis, dolor vitae venenatis aliquet, mi nisi consequat ligula, vel facilisis arcu eros id justo. Vestibulum id porta tellus, nec porttitor diam. Praesent hendrerit nulla sed eros finibus, eu sodales mauris semper. Praesent eros velit, sollicitudin a ex id, sagittis eleifend turpis. Pellentesque facilisis sem eu varius elementum. Sed ac justo dolor. Donec malesuada justo eu urna luctus, et cursus magna laoreet. Vivamus a accumsan risus. Nullam rutrum porta elementum. Curabitur lectus odio, euismod et dolor sit amet, efficitur elementum felis. Mauris quam sapien, commodo sed libero facilisis, cursus feugiat ligula.",
        //        Details = "Donec quis ullamcorper erat, id tempor ante. Integer ut lorem viverra, laoreet orci nec, aliquam nisl. Fusce placerat nisl ac mauris condimentum, sed efficitur urna faucibus. Suspendisse laoreet laoreet malesuada. Quisque congue ut leo ac rhoncus. Aenean commodo dui ut urna ullamcorper tristique. Proin ac feugiat enim, vitae tempus magna. Nam suscipit luctus maximus. Sed laoreet dolor nibh, quis vestibulum leo facilisis a. Integer at mi convallis, porttitor leo non, eleifend enim. Mauris et dolor diam. Morbi metus ligula, pretium ac lectus in, tempus ullamcorper leo.",                
                
        //    };

            //recipe.User = user;
            //recipe.RecipeIngredients = new List<RecipeIngredient> { ri1, ri2 };
            //recipe.RecipeSteps = new List<RecipeStep> { rs1, rs2, rs3 };
            //recipe.Comments = new List<Comment> { c };  

            //dataContext.Recipes.Add(recipe);
            dataContext.SaveChanges();            
        //}
        
        //await userManager.UpdateAsync(user);       
    }

    
    //private static RecipeIngredient GetRecipeIngredient(DataContext dataContext, 
    //    string id, string name, string Description, string recipeId) {
    //    RecipeIngredient ri = new RecipeIngredient {
    //        RecipeIngredientId = id,
    //        IngredientName = name,
    //        Description = Description,
    //        RecipeId = recipeId
    //    };
    //    dataContext.RecipeIngredients.Add(ri);

    //    return ri;
    //}
    //private static RecipeStep GetRecipeStep(DataContext dataContext,
    //    string id, string name, string description, string recipeId) {
    //    RecipeStep rs = new RecipeStep {
    //        RecipeStepId = id,
    //        Name = name,
    //        Description = description,
    //        RecipeId = recipeId
    //    };
    //    dataContext.RecipeSteps.Add(rs);

    //    return rs;
    //}
    //private static async Task<AppUser?> GetUser(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager) {
        
    //    string adminEmail = "admin@example.com";
    //    string adminPassword = "Pa$$w0rd";
    //    AppUser? user = await userManager.FindByNameAsync(adminEmail);

    //    if (user == null) {
    //        string[] roleNames = { "Admin", "User" };
    //        foreach (string roleName in roleNames) {
    //            if (await roleManager.FindByNameAsync(roleName) == null) {
    //                await roleManager.CreateAsync(new IdentityRole(roleName));
    //            }
    //        }

    //        user = new AppUser { UserName = adminEmail, Email = adminEmail };
    //        Console.WriteLine((user == null).ToString());            
    //        var result = await userManager.CreateAsync(user, adminPassword);
    //        if (result.Succeeded) {
    //            await userManager.AddToRoleAsync(user, "Admin");
    //        }
    //    }

    //    return user;
    //}
}



using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace RecipeAZ.Models {
    public class SeedData {
        //private readonly UserManager<ApplicationUser> userManager;
        //public SeedData(UserManager<ApplicationUser> userMgr) {
        //    userManager = userMgr;
        //}

        //public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager) {
        //    var user1 = new ApplicationUser {
        //        UserName = "youser",
        //        Email = "youser@gmail.com"                
        //    };
        //    var user2 = new ApplicationUser {
        //        UserName = "pooser",
        //        Email = "pooser@gmail.com"                
        //    };

        //    var password = "Password123!";
        //    Console.WriteLine("attempting to seed users");
        //    await userManager.CreateAsync(user1, password);
        //    await userManager.CreateAsync(user2, password);
        //    Console.WriteLine("seeded users");
        //}
        public static void SeedDatabase(DataContext context) {/*(), UserManager<ApplicationUser> userManager)*/
            context.Database.Migrate();

            if (context.Recipes.Count() == 0
                || context.Ingredients.Count() == 0 || context.RecipeSteps.Count() == 0) {
                
                //SeedUsersAsync(userManager).Wait();

                //var user1 = userManager.FindByNameAsync("youser").Result;
                //var user2 = userManager.FindByNameAsync("pooser").Result;

                RecipeStep rs1 = new RecipeStep { 
                    Name = "Step 1", 
                    Description = "Rinse Lentils" 
                };
                RecipeStep rs2 = new RecipeStep { 
                    Name = "Step 2", 
                    Description = "Add lentils to pot and add water" 
                };
                RecipeStep rs3 = new RecipeStep { 
                    Name = "Step 3", 
                    Description = "Bring to boil, adding water when necessary, keep on medium boil until lentils disintegrate." 
                };
                
                Recipe r1 = new Recipe { 
                    Name = "Tarka Dal", 
                    Ingredients = new List<RecipeIngredient> { 
                        new RecipeIngredient {
                            Ingredient = new Ingredient {
                                Name = "Lentils"
                            },                            
                            Amount = "4 cups",
                            ModifierPost = "soaked"                            
                        },
                        new RecipeIngredient {
                            Ingredient = new Ingredient {
                                Name = "Water"
                            },
                            Amount = "to cover"                                                        
                        }
                    
                     }, 
                    Steps = new List<RecipeStep> { rs1, rs2, rs3 },
                    Description = "Nulla turpis risus, mollis sed mi non, congue posuere enim. Fusce vehicula ligula nec nibh vehicula tempor. Maecenas accumsan quis mauris a imperdiet. Phasellus venenatis, dolor vitae venenatis aliquet, mi nisi consequat ligula, vel facilisis arcu eros id justo. Vestibulum id porta tellus, nec porttitor diam. Praesent hendrerit nulla sed eros finibus, eu sodales mauris semper. Praesent eros velit, sollicitudin a ex id, sagittis eleifend turpis. Pellentesque facilisis sem eu varius elementum. Sed ac justo dolor. Donec malesuada justo eu urna luctus, et cursus magna laoreet. Vivamus a accumsan risus. Nullam rutrum porta elementum. Curabitur lectus odio, euismod et dolor sit amet, efficitur elementum felis. Mauris quam sapien, commodo sed libero facilisis, cursus feugiat ligula.",
                    Notes = "Donec quis ullamcorper erat, id tempor ante. Integer ut lorem viverra, laoreet orci nec, aliquam nisl. Fusce placerat nisl ac mauris condimentum, sed efficitur urna faucibus. Suspendisse laoreet laoreet malesuada. Quisque congue ut leo ac rhoncus. Aenean commodo dui ut urna ullamcorper tristique. Proin ac feugiat enim, vitae tempus magna. Nam suscipit luctus maximus. Sed laoreet dolor nibh, quis vestibulum leo facilisis a. Integer at mi convallis, porttitor leo non, eleifend enim. Mauris et dolor diam. Morbi metus ligula, pretium ac lectus in, tempus ullamcorper leo."
                };

                //Recipe recipe1 = r1.ToRecipe();
                foreach(RecipeIngredient ri in r1.Ingredients ?? Enumerable.Empty<RecipeIngredient>()) {
                    ri.Recipe = r1;
                }
                
                //user1.Recipes = new List<Recipe> { r1 };        
                
                RecipeStep rs4 = new RecipeStep { 
                    Name = "Step 1", 
                    Description = "Boil Macaroni" 
                };
                RecipeStep rs5 = new RecipeStep { 
                    Name = "Step 2", 
                    Description = "Add Cheese" 
                };
                Recipe r2 = new Recipe {
                    Name = "Mac & Cheese",
                    Ingredients = new List<RecipeIngredient> {
                        new RecipeIngredient {
                            Ingredient = new Ingredient {
                                Name = "Macaroni"
                            },
                            Amount = "1 bag",
                            ModifierPost = "boiled"
                        },
                        new RecipeIngredient {
                            Ingredient = new Ingredient {
                                Name = "cheese"                                
                            },
                            Amount = "2 cups",
                            ModifierPre = "grated"
                        }
                    },
                    Steps = new List<RecipeStep> { rs4, rs5 },
                    Description = "Nulla turpis risus, mollis sed mi non, congue posuere enim. Fusce vehicula ligula nec nibh vehicula tempor. Maecenas accumsan quis mauris a imperdiet. Phasellus venenatis, dolor vitae venenatis aliquet, mi nisi consequat ligula, vel facilisis arcu eros id justo. Vestibulum id porta tellus, nec porttitor diam. Praesent hendrerit nulla sed eros finibus, eu sodales mauris semper. Praesent eros velit, sollicitudin a ex id, sagittis eleifend turpis. Pellentesque facilisis sem eu varius elementum. Sed ac justo dolor. Donec malesuada justo eu urna luctus, et cursus magna laoreet. Vivamus a accumsan risus. Nullam rutrum porta elementum. Curabitur lectus odio, euismod et dolor sit amet, efficitur elementum felis. Mauris quam sapien, commodo sed libero facilisis, cursus feugiat ligula.",
                    Notes = "Donec quis ullamcorper erat, id tempor ante. Integer ut lorem viverra, laoreet orci nec, aliquam nisl. Fusce placerat nisl ac mauris condimentum, sed efficitur urna faucibus. Suspendisse laoreet laoreet malesuada. Quisque congue ut leo ac rhoncus. Aenean commodo dui ut urna ullamcorper tristique. Proin ac feugiat enim, vitae tempus magna. Nam suscipit luctus maximus. Sed laoreet dolor nibh, quis vestibulum leo facilisis a. Integer at mi convallis, porttitor leo non, eleifend enim. Mauris et dolor diam. Morbi metus ligula, pretium ac lectus in, tempus ullamcorper leo."
                };
                //Recipe recipe2 = r2.ToRecipe();
                foreach(RecipeIngredient ri in r2.Ingredients ?? Enumerable.Empty<RecipeIngredient>()) {
                    ri.Recipe = r2;
                }
                //r2.User = u2;
                //user2.Recipes = new List<Recipe> { r2 };

                Comment c1 = new Comment {
                    //User = user2,
                    Recipe = r1,
                    Text = "Nice recipe."
                };
                Comment c2 = new Comment {
                    //User = user1,
                    Recipe = r2,
                    Text = "This sucks."
                };

                r1.Comments = new List<Comment> { c1 };
                r2.Comments = new List<Comment> { c2 };

                //user1.Comments = new List<Comment> { c2 };
                //user2.Comments = new List<Comment> { c1 };

                context.Recipes.Add(r1);
                
                context.Recipes.Add(r2);
                
                Console.WriteLine("atempting to update users");
                // userManager.UpdateAsync(user1);
                // userManager.UpdateAsync(user2);
                //context.Users.Update(user1);
                //context.Users.Update(user2);
                Console.WriteLine("updated users");
                context.SaveChanges();
                
            }
        }
    }
}

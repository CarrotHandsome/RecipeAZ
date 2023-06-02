using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Newtonsoft.Json;
using RecipeAZ.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace RecipeAZ.Services {
    public class JsonDbService {
        public IDbContextFactory<DataContext> _contextFactory { get; set; }
        public IWebHostEnvironment _env;
        public JsonDbService(IDbContextFactory<DataContext> contextFactory, IWebHostEnvironment environment) {
            _contextFactory = contextFactory;
            _env = environment;
        }
        public async Task ExportToJson() {
            using (var context = await _contextFactory.CreateDbContextAsync()) {
                await ExportDbSet<Recipe, RecipeDto>(context.Recipes, "recipes");
                await ExportDbSet<Ingredient, IngredientDto>(context.Ingredients, "ingredients");
                await ExportDbSet<RecipeStep, RecipeStepDto>(context.RecipeSteps, "recipesteps");
                await ExportDbSet<RecipeIngredient, RecipeIngredientDto>(context.RecipeIngredients, "recipeingredients");
                await ExportDbSet<Comment, CommentDto>(context.Comments, "comments");
                await ExportDbSet<Tag, TagDto>(context.Tags, "tags");
                await ExportDbSet<RecipeTag, RecipeTagDto>(context.RecipeTags, "recipetags");
                await ExportDbSet<RecipeLike, RecipeLikeDto>(context.RecipeLikes, "reipelikes");
                await ExportDbSet<AppUser, UserDto>(context.Users, "users", u => u.Id != "02174cf0–9412–4cfe - afbf - 59f706d72cf6");
                await ExportDbSet<IdentityRole, IdentityRoleDto>(context.IdentityRoles, "identityroles", ird => ird.Id != "341743f0 - asd2–42de - afbf - 59kmkkmk72cf6");
                await ExportDbSet<IdentityUserRole<string>, IdentityUserRoleDto>(context.IdentityUserRoles, "identityuserroles", iurd => iurd.RoleId != "341743f0 - asd2–42de - afbf - 59kmkkmk72cf6"
                    && iurd.UserId != "02174cf0–9412–4cfe - afbf - 59f706d72cf6");
            }
        }

        public async Task ImportFromJson() {
            using (var context = await _contextFactory.CreateDbContextAsync()) {
                await ImportDbSet<Recipe, RecipeDto>(context.Recipes, "recipes");
                await ImportDbSet<Ingredient, IngredientDto>(context.Ingredients, "ingredients");
                await ImportDbSet<RecipeStep, RecipeStepDto>(context.RecipeSteps, "recipesteps");
                await ImportDbSet<RecipeIngredient, RecipeIngredientDto>(context.RecipeIngredients, "recipeingredients");
                await ImportDbSet<Comment, CommentDto>(context.Comments, "comments");
                await ImportDbSet<Tag, TagDto>(context.Tags, "tags");
                await ImportDbSet<RecipeTag, RecipeTagDto>(context.RecipeTags, "recipetags");
                await ImportDbSet<RecipeLike, RecipeLikeDto>(context.RecipeLikes, "reipelikes");
                await ImportDbSet<AppUser, UserDto>(context.Users, "users");

                await context.SaveChangesAsync();
            }
        }

        public async Task ExportDbSet<T, D>(DbSet<T> dbSet, string filename, Func<T, bool> filter = null) 
            where T : class
            where D : class, new() {
            var entities = await dbSet.ToListAsync();
            if (filter != null) {
                entities = entities.Where(filter).ToList();
            }
            var dtos = entities.Select(e => MapToDto<T, D>(e)).ToList();
            
            File.WriteAllText(Path.Combine(_env.WebRootPath, "exportedjson", $"{filename}.json"), JsonConvert.SerializeObject(dtos, Formatting.Indented));
        }

        public async Task ImportDbSet<T, D>(DbSet<T> dbSet, string filename)
            where T : class, new()
            where D : class {
            var path = Path.Combine(_env.WebRootPath, "exportedjson", $"{filename}.json");

            if (File.Exists(path)) {
                var json = await File.ReadAllTextAsync(path);
                var dtos = JsonConvert.DeserializeObject<List<D>>(json);

                var entities = dtos.Select(d => MapToEntity<T, D>(d)).ToList();
                foreach (var entity in entities) {
                    await dbSet.AddAsync(entity);
                }
            }
        }

        private D MapToDto<T, D>(T entity) where D : new() {
            var dto = new D();

            var entityProperties = typeof(T).GetProperties();
            var dtoProperties = typeof(D).GetProperties();

            foreach (var dtoProperty in dtoProperties) {
                if (dtoProperty.CanWrite) {
                    var entityProperty = entityProperties.FirstOrDefault(p => p.Name == dtoProperty.Name);
                    if (entityProperty != null) {
                        var value = entityProperty.GetValue(entity);
                        dtoProperty.SetValue(dto, value);
                    }
                }
            }
            return dto;
        }

        private T MapToEntity<T, D>(D dto) where T : new() {
            var entity = new T();

            var dtoProperties = typeof(D).GetProperties();
            var entityProperties = typeof(T).GetProperties();

            foreach (var dtoProperty in dtoProperties) {
                if (dtoProperty.CanRead) {
                    var entityProperty = entityProperties.FirstOrDefault(p => p.Name == dtoProperty.Name);
                    if (entityProperty != null && entityProperty.CanWrite) {
                        var value = dtoProperty.GetValue(dto);
                        entityProperty.SetValue(entity, value);
                    }
                }
            }
            return entity;
        }
    }


    public class RecipeDto {
        public string RecipeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; //aka Overview
        public string Details { get; set; } = string.Empty;
        public string? UserId { get; set; } = string.Empty;
        public string? ImagePath { get; set; } = "images/recipe_default.png";        
        public string? ParentRecipeId { get; set; }          
        public DateTime CreatedAt { get; set; }
    }
    public class IngredientDto {
        public string IngredientId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    public class RecipeStepDto {
        public string RecipeStepId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public string RecipeId { get; set; }
        public int Order { get; set; }
    }
    public class RecipeIngredientDto {
        public string RecipeIngredientId { get; set; }
        public string RecipeId { get; set; }
        public string IngredientId { get; set; }
        public string? BeforeId { get; set; } = "1";
        public string? AfterId { get; set; } = "1";
        public string Name { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Order { get; set; }
    }
    public class CommentDto {
        public string CommentId { get; set; }
        public string UserId { get; set; }
        public string RecipeId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class TagDto {
        public string TagId { get; set; }
        public string Name { get; set; }
    }
    public class RecipeTagDto {
        public string RecipeId { get; set; }
        public string TagId { get; set; }
    }
    public class RecipeLikeDto {
        public string AppUserId { get; set; }
        public string RecipeId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class UserDto {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        
    }

    public class IdentityRoleDto {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
    }

    public class IdentityUserRoleDto {
        public string RoleId { get; set; }
        public string UserId { get; set; }
    }
}

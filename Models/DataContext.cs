using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace RecipeAZ.Models {
    public class DataContext : IdentityDbContext<AppUser> {
        private readonly ILogger<DataContext> _logger;
        public DataContext(DbContextOptions<DataContext> options, ILoggerFactory loggerFactory)
        : base(options) {
            _logger = loggerFactory.CreateLogger<DataContext>();
            
        }

        public DbSet<Recipe> Recipes => Set<Recipe>();
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
        public DbSet<RecipeStep> RecipeSteps => Set<RecipeStep>();     
        public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<RecipeTag> RecipeTags => Set<RecipeTag>();
        public DbSet<IngredientModifier> Modifiers => Set<IngredientModifier>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            string ADMIN_ID = "02174cf0–9412–4cfe - afbf - 59f706d72cf6";
            string ROLE_ID = "341743f0 - asd2–42de - afbf - 59kmkkmk72cf6";

            //seed admin role
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });
            
            AppUser user = new AppUser {
                Id = ADMIN_ID,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };

            PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "P@ssw0rd!");
            modelBuilder.Entity<AppUser>().HasData(user);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });

            modelBuilder.Entity<Ingredient>()
                .Property(i => i.IngredientId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Ingredient>()
                .HasKey(i => i.IngredientId);
            modelBuilder.Entity<IngredientModifier>()
                .Property(im => im.IngredientModifierId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<IngredientModifier>()
                .HasMany(im => im.BeforeRecipeIngredients)
                .WithOne(ri => ri.Before)
                .HasForeignKey(ri => ri.BeforeId);
            modelBuilder.Entity<IngredientModifier>()
                .HasMany(im => im.AfterRecipeIngredients)
                .WithOne(ri => ri.After)
                .HasForeignKey(ri => ri.AfterId);
            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientId);
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient {
                    IngredientId = "1",
                    Name = "water"
                });
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient {
                    IngredientId = "2",
                    Name = "red lentils"
                });
            modelBuilder.Entity<IngredientModifier>().HasData(
                new IngredientModifier {
                    IngredientModifierId = "1",
                    Name = string.Empty
                });

            modelBuilder.Entity<Recipe>().HasData(
                new Recipe { 
                    RecipeId = "1", 
                    UserId = ADMIN_ID,
                    Name = "Tarka Dal",
                    Description = "Nulla turpis risus, mollis sed mi non, congue posuere enim. Fusce vehicula ligula nec nibh vehicula tempor. Maecenas accumsan quis mauris a imperdiet. Phasellus venenatis, dolor vitae venenatis aliquet, mi nisi consequat ligula, vel facilisis arcu eros id justo. Vestibulum id porta tellus, nec porttitor diam. Praesent hendrerit nulla sed eros finibus, eu sodales mauris semper. Praesent eros velit, sollicitudin a ex id, sagittis eleifend turpis. Pellentesque facilisis sem eu varius elementum. Sed ac justo dolor. Donec malesuada justo eu urna luctus, et cursus magna laoreet. Vivamus a accumsan risus. Nullam rutrum porta elementum. Curabitur lectus odio, euismod et dolor sit amet, efficitur elementum felis. Mauris quam sapien, commodo sed libero facilisis, cursus feugiat ligula.",
                    Notes = "Donec quis ullamcorper erat, id tempor ante. Integer ut lorem viverra, laoreet orci nec, aliquam nisl. Fusce placerat nisl ac mauris condimentum, sed efficitur urna faucibus. Suspendisse laoreet laoreet malesuada. Quisque congue ut leo ac rhoncus. Aenean commodo dui ut urna ullamcorper tristique. Proin ac feugiat enim, vitae tempus magna. Nam suscipit luctus maximus. Sed laoreet dolor nibh, quis vestibulum leo facilisis a. Integer at mi convallis, porttitor leo non, eleifend enim. Mauris et dolor diam. Morbi metus ligula, pretium ac lectus in, tempus ullamcorper leo.",  
                    CreatedAt = DateTime.Now
                }
            );
            modelBuilder.Entity<Recipe>()
                .Property(r => r.RecipeId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Recipe>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<IngredientModifier>().HasData(
                new IngredientModifier {
                    IngredientModifierId = "2",
                    Name = "red",
                    IsBefore = true
                });
            modelBuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient { 
                    RecipeIngredientId = "1", 
                    RecipeId = "1", 
                    IngredientId = "2",
                    BeforeId = "2",
                    AfterId = "1",
                    Amount = "4 cups",
                    Order = 1
                },
                new RecipeIngredient {
                    RecipeIngredientId = "2",
                    RecipeId = "1",
                    IngredientId = "1",  
                    BeforeId = "1",
                    AfterId = "1",
                    Amount = "to cover",
                    Order = 2
                }
            );
            modelBuilder.Entity<RecipeIngredient>()
                .Property(r => r.RecipeIngredientId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<RecipeStep>().HasData(
                new RecipeStep {
                    RecipeStepId = "1",
                    RecipeId = "1",
                    Name = "Step 1",
                    Description = "Rinse Lentils",
                    Order = 1
                },
                new RecipeStep {
                    RecipeStepId = "2",
                    RecipeId = "1",
                    Name = "Step 2",
                    Description = "Add lentils and water to pot",
                    Order = 2
                }, new RecipeStep {
                    RecipeStepId = "3",
                    RecipeId = "1",
                    Name = "Step 3",
                    Description = "Boil till cooked",
                    Order = 3
                }
            );
            modelBuilder.Entity<RecipeStep>()
                .Property(r => r.RecipeStepId)
                .ValueGeneratedOnAdd();
            

            modelBuilder.Entity<Comment>().HasData(
                new Comment {
                    CommentId = "1",
                    UserId = ADMIN_ID,
                    RecipeId = "1",
                    Text = "nice recipe",
                    CreatedAt = DateTime.Now,
                }
            );
            modelBuilder.Entity<Tag>()
                .Property(t => t.TagId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Comment>()
                .Property(c => c.CommentId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Comment>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("getdate()");
            
            modelBuilder.Entity<RecipeLike>()
                .HasKey(l => new { l.AppUserId, l.RecipeId });
            modelBuilder.Entity<RecipeLike>()
                .HasOne(l => l.AppUser)
                .WithMany(u => u.RecipesILike)
                .HasForeignKey(l => l.AppUserId);
            modelBuilder.Entity<RecipeLike>()
                .HasOne(l => l.Recipe)
                .WithMany(r => r.UsersWhoLikeMe)
                .HasForeignKey(l => l.RecipeId);
            modelBuilder.Entity<RecipeLike>()
                .Property(rl => rl.CreatedAt)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<RecipeLike>()
                .HasIndex(l => l.AppUserId);
            modelBuilder.Entity<RecipeLike>()
                .HasIndex(l => l.RecipeId);

            modelBuilder.Entity<RecipeTag>()
                .HasKey(t => new { t.RecipeId, t.TagId });
            modelBuilder.Entity<RecipeTag>()
                .HasOne(rt => rt.Recipe)
                .WithMany(r => r.RecipeTags)
                .HasForeignKey(rt => rt.RecipeId);
            modelBuilder.Entity<RecipeTag>()
                .HasOne(rt => rt.Tag)
                .WithMany(t => t.RecipeTags)
                .HasForeignKey(rt => rt.TagId);
        }

        public override void Dispose() {
            base.Dispose();
            _logger.LogInformation("DataContext disposed");
        }

        public override ValueTask DisposeAsync() {
            _logger.LogInformation("DataContext disposed asynchronously");
            return base.DisposeAsync();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess) {
            _logger.LogDebug("Saving changes to the database");
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecipeAZ.Models;

#nullable disable

namespace RecipeAZ.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230504224410_AttemptToFixKeyError")]
    partial class AttemptToFixKeyError
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "341743f0 - asd2–42de - afbf - 59kmkkmk72cf6",
                            ConcurrencyStamp = "341743f0 - asd2–42de - afbf - 59kmkkmk72cf6",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                            RoleId = "341743f0 - asd2–42de - afbf - 59kmkkmk72cf6"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("RecipeAZ.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "98a7afba-bc4f-43af-81cf-e9eb2ac3f9a5",
                            Email = "admin@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAIAAYagAAAAEGOil990IhHoWRxfXrMMwGZtE4pSlLgfeRj6UEB2XdoXpBfctpBVZ4eogD8Cw9r9FA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "352626ad-1436-47c5-97b8-13bd9ec2f6c7",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("RecipeAZ.Models.Comment", b =>
                {
                    b.Property<string>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("RecipeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CommentId");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            CommentId = "1",
                            CreatedAt = new DateTime(2023, 5, 4, 15, 44, 10, 321, DateTimeKind.Local).AddTicks(189),
                            RecipeId = "1",
                            Text = "nice recipe",
                            UserId = "02174cf0–9412–4cfe - afbf - 59f706d72cf6"
                        });
                });

            modelBuilder.Entity("RecipeAZ.Models.Ingredient", b =>
                {
                    b.Property<string>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IngredientId");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            IngredientId = "1",
                            Name = "water"
                        },
                        new
                        {
                            IngredientId = "2",
                            Name = "lentils"
                        });
                });

            modelBuilder.Entity("RecipeAZ.Models.IngredientModifier", b =>
                {
                    b.Property<string>("IngredientModifierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IngredientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IngredientId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsBefore")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IngredientModifierId");

                    b.HasIndex("IngredientId");

                    b.HasIndex("IngredientId1");

                    b.ToTable("Modifiers");

                    b.HasData(
                        new
                        {
                            IngredientModifierId = "1",
                            IsBefore = false,
                            Name = ""
                        },
                        new
                        {
                            IngredientModifierId = "2",
                            IsBefore = true,
                            Name = "red"
                        });
                });

            modelBuilder.Entity("RecipeAZ.Models.Recipe", b =>
                {
                    b.Property<string>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RecipeId");

                    b.HasIndex("UserId");

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            RecipeId = "1",
                            CreatedAt = new DateTime(2023, 5, 4, 15, 44, 10, 320, DateTimeKind.Local).AddTicks(9092),
                            Description = "Nulla turpis risus, mollis sed mi non, congue posuere enim. Fusce vehicula ligula nec nibh vehicula tempor. Maecenas accumsan quis mauris a imperdiet. Phasellus venenatis, dolor vitae venenatis aliquet, mi nisi consequat ligula, vel facilisis arcu eros id justo. Vestibulum id porta tellus, nec porttitor diam. Praesent hendrerit nulla sed eros finibus, eu sodales mauris semper. Praesent eros velit, sollicitudin a ex id, sagittis eleifend turpis. Pellentesque facilisis sem eu varius elementum. Sed ac justo dolor. Donec malesuada justo eu urna luctus, et cursus magna laoreet. Vivamus a accumsan risus. Nullam rutrum porta elementum. Curabitur lectus odio, euismod et dolor sit amet, efficitur elementum felis. Mauris quam sapien, commodo sed libero facilisis, cursus feugiat ligula.",
                            ImagePath = "images/recipe_default.png",
                            Name = "Tarka Dal",
                            Notes = "Donec quis ullamcorper erat, id tempor ante. Integer ut lorem viverra, laoreet orci nec, aliquam nisl. Fusce placerat nisl ac mauris condimentum, sed efficitur urna faucibus. Suspendisse laoreet laoreet malesuada. Quisque congue ut leo ac rhoncus. Aenean commodo dui ut urna ullamcorper tristique. Proin ac feugiat enim, vitae tempus magna. Nam suscipit luctus maximus. Sed laoreet dolor nibh, quis vestibulum leo facilisis a. Integer at mi convallis, porttitor leo non, eleifend enim. Mauris et dolor diam. Morbi metus ligula, pretium ac lectus in, tempus ullamcorper leo.",
                            UserId = "02174cf0–9412–4cfe - afbf - 59f706d72cf6"
                        });
                });

            modelBuilder.Entity("RecipeAZ.Models.RecipeIngredient", b =>
                {
                    b.Property<string>("RecipeIngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AfterId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Amount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BeforeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IngredientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("RecipeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RecipeIngredientId");

                    b.HasIndex("AfterId");

                    b.HasIndex("BeforeId");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeIngredients");

                    b.HasData(
                        new
                        {
                            RecipeIngredientId = "1",
                            AfterId = "1",
                            Amount = "4 cups",
                            BeforeId = "2",
                            IngredientId = "2",
                            Name = "",
                            Notes = "",
                            Order = 1,
                            RecipeId = "1"
                        },
                        new
                        {
                            RecipeIngredientId = "2",
                            AfterId = "1",
                            Amount = "to cover",
                            BeforeId = "1",
                            IngredientId = "1",
                            Name = "",
                            Notes = "",
                            Order = 2,
                            RecipeId = "1"
                        });
                });

            modelBuilder.Entity("RecipeAZ.Models.RecipeLike", b =>
                {
                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RecipeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("AppUserId", "RecipeId");

                    b.HasIndex("AppUserId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeLike");
                });

            modelBuilder.Entity("RecipeAZ.Models.RecipeStep", b =>
                {
                    b.Property<string>("RecipeStepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("RecipeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RecipeStepId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeSteps");

                    b.HasData(
                        new
                        {
                            RecipeStepId = "1",
                            Description = "Rinse Lentils",
                            Details = "",
                            Name = "Step 1",
                            Order = 1,
                            RecipeId = "1"
                        },
                        new
                        {
                            RecipeStepId = "2",
                            Description = "Add lentils and water to pot",
                            Details = "",
                            Name = "Step 2",
                            Order = 2,
                            RecipeId = "1"
                        },
                        new
                        {
                            RecipeStepId = "3",
                            Description = "Boil till cooked",
                            Details = "",
                            Name = "Step 3",
                            Order = 3,
                            RecipeId = "1"
                        });
                });

            modelBuilder.Entity("RecipeAZ.Models.RecipeTag", b =>
                {
                    b.Property<string>("RecipeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TagId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RecipeId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("RecipeTags");
                });

            modelBuilder.Entity("RecipeAZ.Models.Tag", b =>
                {
                    b.Property<string>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("RecipeAZ.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("RecipeAZ.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeAZ.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("RecipeAZ.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecipeAZ.Models.Comment", b =>
                {
                    b.HasOne("RecipeAZ.Models.Recipe", "Recipe")
                        .WithMany("Comments")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeAZ.Models.AppUser", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipeAZ.Models.IngredientModifier", b =>
                {
                    b.HasOne("RecipeAZ.Models.Ingredient", null)
                        .WithMany("Afters")
                        .HasForeignKey("IngredientId");

                    b.HasOne("RecipeAZ.Models.Ingredient", null)
                        .WithMany("Befores")
                        .HasForeignKey("IngredientId1");
                });

            modelBuilder.Entity("RecipeAZ.Models.Recipe", b =>
                {
                    b.HasOne("RecipeAZ.Models.AppUser", "User")
                        .WithMany("Recipes")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipeAZ.Models.RecipeIngredient", b =>
                {
                    b.HasOne("RecipeAZ.Models.IngredientModifier", "After")
                        .WithMany("AfterRecipeIngredients")
                        .HasForeignKey("AfterId");

                    b.HasOne("RecipeAZ.Models.IngredientModifier", "Before")
                        .WithMany("BeforeRecipeIngredients")
                        .HasForeignKey("BeforeId");

                    b.HasOne("RecipeAZ.Models.Ingredient", "Ingredient")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeAZ.Models.Recipe", "Recipe")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("After");

                    b.Navigation("Before");

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeAZ.Models.RecipeLike", b =>
                {
                    b.HasOne("RecipeAZ.Models.AppUser", "AppUser")
                        .WithMany("RecipesILike")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeAZ.Models.Recipe", "Recipe")
                        .WithMany("UsersWhoLikeMe")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeAZ.Models.RecipeStep", b =>
                {
                    b.HasOne("RecipeAZ.Models.Recipe", "Recipe")
                        .WithMany("RecipeSteps")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeAZ.Models.RecipeTag", b =>
                {
                    b.HasOne("RecipeAZ.Models.Recipe", "Recipe")
                        .WithMany("RecipeTags")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeAZ.Models.Tag", "Tag")
                        .WithMany("RecipeTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("RecipeAZ.Models.AppUser", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Recipes");

                    b.Navigation("RecipesILike");
                });

            modelBuilder.Entity("RecipeAZ.Models.Ingredient", b =>
                {
                    b.Navigation("Afters");

                    b.Navigation("Befores");

                    b.Navigation("RecipeIngredients");
                });

            modelBuilder.Entity("RecipeAZ.Models.IngredientModifier", b =>
                {
                    b.Navigation("AfterRecipeIngredients");

                    b.Navigation("BeforeRecipeIngredients");
                });

            modelBuilder.Entity("RecipeAZ.Models.Recipe", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("RecipeIngredients");

                    b.Navigation("RecipeSteps");

                    b.Navigation("RecipeTags");

                    b.Navigation("UsersWhoLikeMe");
                });

            modelBuilder.Entity("RecipeAZ.Models.Tag", b =>
                {
                    b.Navigation("RecipeTags");
                });
#pragma warning restore 612, 618
        }
    }
}
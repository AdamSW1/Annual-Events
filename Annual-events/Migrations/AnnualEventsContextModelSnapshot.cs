﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace annualevents.Migrations
{
    [DbContext(typeof(AnnualEventsContext))]
    partial class AnnualEventsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Annual_Events_UserRecipe", b =>
                {
                    b.Property<int>("FavRecipesRecipeID")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("FavouritedByAnnual_Events_UserId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("FavRecipesRecipeID", "FavouritedByAnnual_Events_UserId");

                    b.HasIndex("FavouritedByAnnual_Events_UserId");

                    b.ToTable("Annual_Events_UserRecipe");
                });

            modelBuilder.Entity("BusinessLayer.Annual_Events_User", b =>
                {
                    b.Property<int>("Annual_Events_UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Annual_Events_UserId"));

                    b.Property<int>("Age")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("Annual_Events_UserId");

                    b.ToTable("Annual_Events_User");
                });

            modelBuilder.Entity("BusinessLayer.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"));

                    b.Property<int?>("RecipeID")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("ReviewerUsername")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("Score")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("ReviewId");

                    b.HasIndex("RecipeID");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("PreparationRecipe", b =>
                {
                    b.Property<int>("PreparationID")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("RecipesRecipeID")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("PreparationID", "RecipesRecipeID");

                    b.HasIndex("RecipesRecipeID");

                    b.ToTable("PreparationRecipe");
                });

            modelBuilder.Entity("RecipeInfo.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<double>("Price")
                        .HasColumnType("BINARY_DOUBLE");

                    b.HasKey("IngredientId");

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("RecipeInfo.Preparation", b =>
                {
                    b.Property<int>("PreparationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PreparationID"));

                    b.Property<string>("Step")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("StepNumber")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("PreparationID");

                    b.ToTable("Preparation");
                });

            modelBuilder.Entity("RecipeInfo.Recipe", b =>
                {
                    b.Property<int>("RecipeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeID"));

                    b.Property<double>("AverageScore")
                        .HasColumnType("BINARY_DOUBLE");

                    b.Property<double>("CookingTime")
                        .HasColumnType("BINARY_DOUBLE");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("Favourite")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("OwnerAnnual_Events_UserId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("Servings")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("RecipeID");

                    b.HasIndex("OwnerAnnual_Events_UserId");

                    b.ToTable("Recipe");
                });

            modelBuilder.Entity("RecipeInfo.RecipeIngredient", b =>
                {
                    b.Property<int>("RecipeIngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeIngredientId"));

                    b.Property<int>("IngredientId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Quantity")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("RecipeID")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("RecipeIngredientId");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RecipeID");

                    b.ToTable("RecipeIngredients");
                });

            modelBuilder.Entity("RecipeInfo.RecipeTag", b =>
                {
                    b.Property<int>("RecipeTagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeTagId"));

                    b.Property<string>("Tag")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("RecipeTagId");

                    b.ToTable("RecipeTag");
                });

            modelBuilder.Entity("RecipeRecipeTag", b =>
                {
                    b.Property<int>("RecipeWithTagsRecipeID")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("TagsRecipeTagId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("RecipeWithTagsRecipeID", "TagsRecipeTagId");

                    b.HasIndex("TagsRecipeTagId");

                    b.ToTable("RecipeRecipeTag");
                });

            modelBuilder.Entity("Annual_Events_UserRecipe", b =>
                {
                    b.HasOne("RecipeInfo.Recipe", null)
                        .WithMany()
                        .HasForeignKey("FavRecipesRecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessLayer.Annual_Events_User", null)
                        .WithMany()
                        .HasForeignKey("FavouritedByAnnual_Events_UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessLayer.Review", b =>
                {
                    b.HasOne("RecipeInfo.Recipe", null)
                        .WithMany("Reviews")
                        .HasForeignKey("RecipeID");
                });

            modelBuilder.Entity("PreparationRecipe", b =>
                {
                    b.HasOne("RecipeInfo.Preparation", null)
                        .WithMany()
                        .HasForeignKey("PreparationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeInfo.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesRecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecipeInfo.Recipe", b =>
                {
                    b.HasOne("BusinessLayer.Annual_Events_User", "Owner")
                        .WithMany("Recipes")
                        .HasForeignKey("OwnerAnnual_Events_UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("RecipeInfo.RecipeIngredient", b =>
                {
                    b.HasOne("RecipeInfo.Ingredient", "Ingredient")
                        .WithMany("Recipes")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeInfo.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeRecipeTag", b =>
                {
                    b.HasOne("RecipeInfo.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipeWithTagsRecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeInfo.RecipeTag", null)
                        .WithMany()
                        .HasForeignKey("TagsRecipeTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessLayer.Annual_Events_User", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("RecipeInfo.Ingredient", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("RecipeInfo.Recipe", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}

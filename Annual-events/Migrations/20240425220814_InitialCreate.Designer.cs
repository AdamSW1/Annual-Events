﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace annualevents.Migrations
{
    [DbContext(typeof(AnnualEventsContext))]
    [Migration("20240425220814_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("FavouritedByAnnual_Events_UserID")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("FavRecipesRecipeID", "FavouritedByAnnual_Events_UserID");

                    b.HasIndex("FavouritedByAnnual_Events_UserID");

                    b.ToTable("Annual_Events_UserRecipe");
                });

            modelBuilder.Entity("BusinessLayer.Annual_Events_User", b =>
                {
                    b.Property<int>("Annual_Events_UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Annual_Events_UserID"));

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

                    b.HasKey("Annual_Events_UserID");

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

                    b.ToTable("Annual_Events_Review");
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

                    b.Property<string>("Quantity")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int?>("RecipeID")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("IngredientId");

                    b.HasIndex("RecipeID");

                    b.ToTable("Annual_Events_Ingredient");
                });

            modelBuilder.Entity("RecipeInfo.Preparation", b =>
                {
                    b.Property<int>("PreparationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PreparationID"));

                    b.Property<int?>("RecipeID")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Step")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("StepNumber")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("PreparationID");

                    b.HasIndex("RecipeID");

                    b.ToTable("Annual_Events_Preparation");
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

                    b.Property<int?>("FavouritedByID")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int?>("OwnerID")
                        .IsRequired()
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("Servings")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("RecipeID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Annual_Events_Recipe");
                });

            modelBuilder.Entity("RecipeInfo.RecipeTag", b =>
                {
                    b.Property<int>("RecipeTagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeTagId"));

                    b.Property<int?>("RecipeID")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Tag")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("RecipeTagId");

                    b.HasIndex("RecipeID");

                    b.ToTable("Annual_Events_RecipeTag");
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
                        .HasForeignKey("FavouritedByAnnual_Events_UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessLayer.Review", b =>
                {
                    b.HasOne("RecipeInfo.Recipe", null)
                        .WithMany("Reviews")
                        .HasForeignKey("RecipeID");
                });

            modelBuilder.Entity("RecipeInfo.Ingredient", b =>
                {
                    b.HasOne("RecipeInfo.Recipe", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeID");
                });

            modelBuilder.Entity("RecipeInfo.Preparation", b =>
                {
                    b.HasOne("RecipeInfo.Recipe", null)
                        .WithMany("Preparation")
                        .HasForeignKey("RecipeID");
                });

            modelBuilder.Entity("RecipeInfo.Recipe", b =>
                {
                    b.HasOne("BusinessLayer.Annual_Events_User", "Owner")
                        .WithMany("Recipes")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("RecipeInfo.RecipeTag", b =>
                {
                    b.HasOne("RecipeInfo.Recipe", null)
                        .WithMany("Tags")
                        .HasForeignKey("RecipeID");
                });

            modelBuilder.Entity("BusinessLayer.Annual_Events_User", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("RecipeInfo.Recipe", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("Preparation");

                    b.Navigation("Reviews");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}

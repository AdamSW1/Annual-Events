﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace annualevents.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Annual_Events_User",
                columns: table => new
                {
                    AnnualEventsUserId = table.Column<int>(name: "Annual_Events_UserId", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Annual_Events_User", x => x.AnnualEventsUserId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.IngredientId);
                });

            migrationBuilder.CreateTable(
                name: "Preparation",
                columns: table => new
                {
                    PreparationID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StepNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Step = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preparation", x => x.PreparationID);
                });

            migrationBuilder.CreateTable(
                name: "RecipeTag",
                columns: table => new
                {
                    RecipeTagId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tag = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTag", x => x.RecipeTagId);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    RecipeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OwnerAnnualEventsUserId = table.Column<int>(name: "OwnerAnnual_Events_UserId", type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CookingTime = table.Column<double>(type: "REAL", nullable: false),
                    Servings = table.Column<int>(type: "INTEGER", nullable: false),
                    AverageScore = table.Column<double>(type: "REAL", nullable: false),
                    Favourite = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.RecipeID);
                    table.ForeignKey(
                        name: "FK_Recipe_Annual_Events_User_OwnerAnnual_Events_UserId",
                        column: x => x.OwnerAnnualEventsUserId,
                        principalTable: "Annual_Events_User",
                        principalColumn: "Annual_Events_UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Annual_Events_UserRecipe",
                columns: table => new
                {
                    FavRecipesRecipeID = table.Column<int>(type: "INTEGER", nullable: false),
                    FavouritedByAnnualEventsUserId = table.Column<int>(name: "FavouritedByAnnual_Events_UserId", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Annual_Events_UserRecipe", x => new { x.FavRecipesRecipeID, x.FavouritedByAnnualEventsUserId });
                    table.ForeignKey(
                        name: "FK_Annual_Events_UserRecipe_Annual_Events_User_FavouritedByAnnual_Events_UserId",
                        column: x => x.FavouritedByAnnualEventsUserId,
                        principalTable: "Annual_Events_User",
                        principalColumn: "Annual_Events_UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Annual_Events_UserRecipe_Recipe_FavRecipesRecipeID",
                        column: x => x.FavRecipesRecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreparationRecipe",
                columns: table => new
                {
                    PreparationID = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipesRecipeID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreparationRecipe", x => new { x.PreparationID, x.RecipesRecipeID });
                    table.ForeignKey(
                        name: "FK_PreparationRecipe_Preparation_PreparationID",
                        column: x => x.PreparationID,
                        principalTable: "Preparation",
                        principalColumn: "PreparationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreparationRecipe_Recipe_RecipesRecipeID",
                        column: x => x.RecipesRecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    RecipeIngredientId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeID = table.Column<int>(type: "INTEGER", nullable: true),
                    IngredientId = table.Column<int>(type: "INTEGER", nullable: true),
                    Quantity = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => x.RecipeIngredientId);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredient",
                        principalColumn: "IngredientId");
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Recipe_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID");
                });

            migrationBuilder.CreateTable(
                name: "RecipeRecipeTag",
                columns: table => new
                {
                    RecipeWithTagsRecipeID = table.Column<int>(type: "INTEGER", nullable: false),
                    TagsRecipeTagId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeRecipeTag", x => new { x.RecipeWithTagsRecipeID, x.TagsRecipeTagId });
                    table.ForeignKey(
                        name: "FK_RecipeRecipeTag_RecipeTag_TagsRecipeTagId",
                        column: x => x.TagsRecipeTagId,
                        principalTable: "RecipeTag",
                        principalColumn: "RecipeTagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeRecipeTag_Recipe_RecipeWithTagsRecipeID",
                        column: x => x.RecipeWithTagsRecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeID = table.Column<int>(type: "INTEGER", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false),
                    ReviewerUsername = table.Column<string>(type: "TEXT", nullable: false),
                    ReviewText = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Review_Recipe_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Annual_Events_UserRecipe_FavouritedByAnnual_Events_UserId",
                table: "Annual_Events_UserRecipe",
                column: "FavouritedByAnnual_Events_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PreparationRecipe_RecipesRecipeID",
                table: "PreparationRecipe",
                column: "RecipesRecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_OwnerAnnual_Events_UserId",
                table: "Recipe",
                column: "OwnerAnnual_Events_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IngredientId",
                table: "RecipeIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_RecipeID",
                table: "RecipeIngredients",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeRecipeTag_TagsRecipeTagId",
                table: "RecipeRecipeTag",
                column: "TagsRecipeTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_RecipeID",
                table: "Review",
                column: "RecipeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Annual_Events_UserRecipe");

            migrationBuilder.DropTable(
                name: "PreparationRecipe");

            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "RecipeRecipeTag");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Preparation");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "RecipeTag");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "Annual_Events_User");
        }
    }
}
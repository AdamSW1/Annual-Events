﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace annual_events.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Annual_Events_User",
                columns: table => new
                {
                    Annual_Events_UserId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Username = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Password = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Age = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ProfilePicture = table.Column<byte[]>(type: "BLOB", maxLength: 3000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Annual_Events_User", x => x.Annual_Events_UserId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Price = table.Column<double>(type: "BINARY_DOUBLE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                });

            migrationBuilder.CreateTable(
                name: "Preparation",
                columns: table => new
                {
                    PreparationID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    StepNumber = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Step = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preparation", x => x.PreparationID);
                });

            migrationBuilder.CreateTable(
                name: "RecipeTags",
                columns: table => new
                {
                    RecipeTagId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Tag = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTags", x => x.RecipeTagId);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    RecipeID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    OwnerAnnual_Events_UserId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CookingTime = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Servings = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AverageScore = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Favourite = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.RecipeID);
                    table.ForeignKey(
                        name: "FK_Recipe_Annual_Events_User_OwnerAnnual_Events_UserId",
                        column: x => x.OwnerAnnual_Events_UserId,
                        principalTable: "Annual_Events_User",
                        principalColumn: "Annual_Events_UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Annual_Events_UserRecipe",
                columns: table => new
                {
                    FavRecipesRecipeID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    FavouritedByAnnual_Events_UserId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Annual_Events_UserRecipe", x => new { x.FavRecipesRecipeID, x.FavouritedByAnnual_Events_UserId });
                    table.ForeignKey(
                        name: "FK_Annual_Events_UserRecipe_Annual_Events_User_FavouritedByAnnual_Events_UserId",
                        column: x => x.FavouritedByAnnual_Events_UserId,
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
                    PreparationID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RecipesRecipeID = table.Column<int>(type: "NUMBER(10)", nullable: false)
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
                    RecipeIngredientId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    RecipeID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    IngredientId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    Quantity = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => x.RecipeIngredientId);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
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
                    RecipeWithTagsRecipeID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TagsRecipeTagId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeRecipeTag", x => new { x.RecipeWithTagsRecipeID, x.TagsRecipeTagId });
                    table.ForeignKey(
                        name: "FK_RecipeRecipeTag_RecipeTags_TagsRecipeTagId",
                        column: x => x.TagsRecipeTagId,
                        principalTable: "RecipeTags",
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
                    ReviewId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    RecipeID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Score = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ReviewerUsername = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ReviewText = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
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
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "RecipeTags");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "Annual_Events_User");
        }
    }
}

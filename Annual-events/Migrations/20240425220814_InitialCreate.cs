using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace annualevents.Migrations
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
                    AnnualEventsUserID = table.Column<int>(name: "Annual_Events_UserID", type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Username = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Age = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Annual_Events_User", x => x.AnnualEventsUserID);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    RecipeID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    OwnerID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    FavouritedByID = table.Column<int>(type: "NUMBER(10)", nullable: true),
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
                        name: "FK_Recipe_Annual_Events_User_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Annual_Events_User",
                        principalColumn: "Annual_Events_UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Annual_Events_UserRecipe",
                columns: table => new
                {
                    FavRecipesRecipeID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    FavouritedByAnnualEventsUserID = table.Column<int>(name: "FavouritedByAnnual_Events_UserID", type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Annual_Events_UserRecipe", x => new { x.FavRecipesRecipeID, x.FavouritedByAnnualEventsUserID });
                    table.ForeignKey(
                        name: "FK_Annual_Events_UserRecipe_Annual_Events_User_FavouritedByAnnual_Events_UserID",
                        column: x => x.FavouritedByAnnualEventsUserID,
                        principalTable: "Annual_Events_User",
                        principalColumn: "Annual_Events_UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Annual_Events_UserRecipe_Recipe_FavRecipesRecipeID",
                        column: x => x.FavRecipesRecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Quantity = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Price = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    RecipeID = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.IngredientId);
                    table.ForeignKey(
                        name: "FK_Ingredient_Recipe_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID");
                });

            migrationBuilder.CreateTable(
                name: "Preparation",
                columns: table => new
                {
                    PreparationID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    StepNumber = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Step = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    RecipeID = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preparation", x => x.PreparationID);
                    table.ForeignKey(
                        name: "FK_Preparation_Recipe_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID");
                });

            migrationBuilder.CreateTable(
                name: "RecipeTag",
                columns: table => new
                {
                    RecipeTagId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Tag = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    RecipeID = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTag", x => x.RecipeTagId);
                    table.ForeignKey(
                        name: "FK_RecipeTag_Recipe_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Score = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ReviewerUsername = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ReviewText = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    RecipeID = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Review_Recipe_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Annual_Events_UserRecipe_FavouritedByAnnual_Events_UserID",
                table: "Annual_Events_UserRecipe",
                column: "FavouritedByAnnual_Events_UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_RecipeID",
                table: "Ingredient",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_Preparation_RecipeID",
                table: "Preparation",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_OwnerID",
                table: "Recipe",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTag_RecipeID",
                table: "RecipeTag",
                column: "RecipeID");

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
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Preparation");

            migrationBuilder.DropTable(
                name: "RecipeTag");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "Annual_Events_User");
        }
    }
}

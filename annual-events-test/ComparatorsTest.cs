using Comparators;
using RecipeInfo;
using BusinessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace annual_events_test;

[TestClass]
public class ComparatorsTest
{

    // Tests CompareByIngredients
    [TestMethod]
    public void TestIngredientsComparator()
    {
        // Arrange
        IComparer<Recipe> ingredientComparator = new CompareByIngredients();
        

        // Act
        (Recipe recipe1, Recipe recipe2) = AddExampleRecipes();

        // Assert
        Assert.AreEqual(ingredientComparator.Compare(recipe1, recipe2), 0);
        Assert.AreEqual(ingredientComparator.Compare(recipe2, recipe1), 0);
    }

    // Tests CompareByRating
    [TestMethod]
    public void TestRatingComparator()
    {
        // Arrange
        IComparer<Recipe> ratingComparator = new CompareByRating();

        // Act
        (Recipe recipe1, Recipe recipe2) = AddExampleRecipes();

        // Assert
        Assert.AreEqual(ratingComparator.Compare(recipe1, recipe2), 1);
        Assert.AreEqual(ratingComparator.Compare(recipe2, recipe1), -1);

    }

    // Tests CompareByServings
    [TestMethod]
    public void TestServingsComparator()
    {
        // Arrange
        IComparer<Recipe> servingsComparator = new CompareByServings();

        // Act
        (Recipe recipe1, Recipe recipe2) = AddExampleRecipes();

        // Assert
        Assert.AreEqual(servingsComparator.Compare(recipe1, recipe2), 1);
        Assert.AreEqual(servingsComparator.Compare(recipe2, recipe1), -1);
    }

    // Tests CompareByTime
    [TestMethod]
    public void TestTimeComparator()
    {
        // Arrange
        IComparer<Recipe> timeComparator = new CompareByTime();

        // Act
        (Recipe recipe1, Recipe recipe2) = AddExampleRecipes();

        // Assert
        Assert.AreEqual(timeComparator.Compare(recipe1, recipe2), 1);
        Assert.AreEqual(timeComparator.Compare(recipe2, recipe1), -1);
    }

    private static (Recipe, Recipe) AddExampleRecipes()
    {
        Ingredient flour = new("flour", 7);
        Ingredient egg = new("egg", 3);
        List<Ingredient> ingredients = new() { flour, egg };
        List<RecipeIngredient> recipeIngredients= ingredients.Select(ingredient => new RecipeIngredient{Ingredient = ingredient,Quantity ="4"}).ToList();
        List<RecipeTag> tags = new List<RecipeTag>() { new("cake"), new("chocolate") };
        Recipe exampleRecipe = new("Chocolate cake",
                                            "A simple chocolate cake",
                                            120,
                                            new List<Preparation>(){
                                                new(1, "bake"),
                                                new(2, "put in oven"),
                                                new(3, "do stuff")

                                            },
                                            8,
                                            recipeIngredients,
                                            0,
                                            new Annual_Events_User("cookielover123", "234", "nbdfnjgbd", 16),
                                            tags,
                                            new List<Review> { new("reviewer1", "review1",5)}
                                            );
        exampleRecipe.AverageScore=5;
        Recipe exampleRecipe2 = new("Vanilla cake",
                                            "A simple Vanilla cake",
                                            100,
                                            new List<Preparation>(){
                                                new(1, "bake"),
                                                new(2, "put in oven"),
                                                new(3, "do stuff")

                                            },
                                            6,
                                            recipeIngredients,
                                            0,
                                            new Annual_Events_User("hello1234", "678", "ngjveuiwsnhg", 24),
                                            tags,
                                            new List<Review> { new("reviewer1", "review1",4)}
                                            );
        exampleRecipe2.AverageScore=4;
        return (exampleRecipe, exampleRecipe2);
    }
}
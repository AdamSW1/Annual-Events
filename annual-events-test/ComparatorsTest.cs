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
        AuthenticationManager manager = new AuthenticationManager();
        

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
        AuthenticationManager manager = new AuthenticationManager();

        // Act
        (Recipe recipe1, Recipe recipe2) = AddExampleRecipes();

        // Assert
        

    }

    // Tests CompareByServings
    [TestMethod]
    public void TestServingsComparator()
    {
        // Arrange
        IComparer<Recipe> servingsComparator = new CompareByServings();
        AuthenticationManager manager = new AuthenticationManager();

        // Act
        (Recipe recipe1, Recipe recipe2) = AddExampleRecipes();

        // Assert

    }

    // Tests CompareByTime
    [TestMethod]
    public void TestTimeComparator()
    {
        // Arrange
        IComparer<Recipe> timeComparator = new CompareByTime();
        AuthenticationManager manager = new AuthenticationManager();

        // Act
        (Recipe recipe1, Recipe recipe2) = AddExampleRecipes();

        // Assert

    }

    private static (Recipe, Recipe) AddExampleRecipes()
    {
        Ingredient flour = new Ingredient("flour", "6 cups", 7);
        Ingredient egg = new Ingredient("egg", "4", 3);
        List<Ingredient> ingredients = new List<Ingredient>() { flour, egg };
        List<string> tags = new List<string>() { "cake", "chocolate" };
        Recipe exampleRecipe = new Recipe("Chocolate cake",
                                            "A simple chocolate cake",
                                            120,
                                            "mix, put in oven, do stuff",
                                            8,
                                            5,
                                            ingredients,
                                            0,
                                            new User("cookielol132", "234", "nbdfnjgbd", 16),
                                            tags
                                            );
        Recipe exampleRecipe2 = new Recipe("Vanilla cake",
                                            "A simple Vanilla cake",
                                            100,
                                            "mix, put in oven, do stuff",
                                            6,
                                            4,
                                            ingredients,
                                            0,
                                            new User("hello1234", "678", "ngjveuiwsnhg", 24),
                                            tags
                                            );
        return (exampleRecipe, exampleRecipe2);
    }
}
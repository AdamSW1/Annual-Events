using Comparators;
using RecipeInfo;
using BusinessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace annual_events_test;

[TestClass]
public class ComparatorsTest
{
    // Sample recipes for testing
    Recipe recipe1 = new Recipe("cheesecake", "tasty cheesecake for birthday party", 2, "do stuff", 2, 3, new List<Ingredient> {new Ingredient("cheese", "2", 3)}, 2, new User("cakelover123", "1234", "hello its me", 15));
    Recipe recipe2 = new Recipe("cheese pizza", "tasty cheese pizza", 3, "do stuff", 2, 3, new List<Ingredient> {new Ingredient("cheese", "2", 3)}, 2, new User("cakelover123", "1234", "hello its me", 15));
    Recipe recipe3 = new Recipe("cheese pizza", "tasty cheese pizza", 3, "do stuff", 2, 5, new List<Ingredient> {new Ingredient("cheese", "2", 3), new Ingredient("test", "test", 3)}, 2, new User("cakelover123", "1234", "hello its me", 15));
    
    // Tests CompareByIngredients
    [TestMethod]
    public void TestIngredientsComparatorAllCases()
    {
        // Arrange
        IComparer<Recipe> ingredientComparator = new CompareByIngredients();

        // Assert
        Assert.AreEqual(ingredientComparator.Compare(recipe1, recipe2), 0);
        Assert.AreEqual(ingredientComparator.Compare(recipe2, recipe1), 0);
        Assert.AreEqual(ingredientComparator.Compare(recipe3, recipe1), 1);
        Assert.AreEqual(ingredientComparator.Compare(recipe2, recipe3), -1);
    }

    // Tests CompareByRating
    [TestMethod]
    public void TestRatingComparator()
    {
        // Arrange
        IComparer<Recipe> ratingComparator = new CompareByRating();
        
        // Assert
        Assert.AreEqual(ratingComparator.Compare(recipe1, recipe2), 0);
        Assert.AreEqual(ratingComparator.Compare(recipe2, recipe1), 0);
        Assert.AreEqual(ratingComparator.Compare(recipe1, recipe3), -1);
         Assert.AreEqual(ratingComparator.Compare(recipe3, recipe2), 1);
    }

    // Tests CompareByServings
    [TestMethod]
    public void TestServingsComparator()
    {
        // Arrange
        IComparer<Recipe> servingsComparator = new CompareByServings();
        
        // Assert
        
    }

    // Tests CompareByTime
    [TestMethod]
    public void TestTimeComparator()
    {
        // Arrange
        IComparer<Recipe> timeComparator = new CompareByTime();
        
        // Assert

    }
}
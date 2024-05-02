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
        IComparer<Recipe> ingredientComparator = new CompareByTime();
        UserTest user1 = new UserTest();
        Recipe recipe1 = new Recipe("cheesecake", "tasty cheesecake for birthday party", 2, "do stuff", 2, 4, new List<Ingredient> {new Ingredient("cheese", "2", 3)}, 2, new User("cakelover123", "1234", "hello its me", 15));
        Recipe recipe2 = new Recipe("cheese pizza", "tasty cheese pizza", 3, "do stuff", 2, 4, new List<Ingredient> {new Ingredient("cheese", "2", 3)}, 2, new User("cakelover123", "1234", "hello its me", 15));

        Assert.AreEqual(ingredientComparator.Compare(recipe1, recipe2), -1);
    }

    // Tests CompareByRating
    [TestMethod]
    public void TestRatingComparator()
    {
        throw new NotImplementedException();
    }

    // Tests CompareByServings
    [TestMethod]
    public void TestServingsComparator()
    {
        throw new NotImplementedException();
    }

    // Tests CompareByTime
    [TestMethod]
    public void TestTimeComparator()
    {
        throw new NotImplementedException();
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;
using sorters;
using System;
using System.Net.Http.Headers;
using System.Diagnostics;
using RecipeInfo;
namespace annual_events_test;

[TestClass]

public class UtilsTest
{
    [TestMethod]
    public void ValidateTagsTest()
    {
        //Arrange
        Utils utils = new Utils();
        List<string> tags = new List<string> { "vegetarian", "vegan" };
        List<RecipeTags> tags_expected = new List<RecipeTags> { RecipeTags.vegetarian, RecipeTags.vegan };
        //Act
        List<RecipeTags> tags_test = Utils.ValidateTags(tags);
        //Assert
        CollectionAssert.AreEquivalent(tags_test, tags_expected);
    }

    [TestMethod]
    public void CheckDouble_GreaterThan0_returnsTrue()
    {
        //arrange
        double test = 1;

        //act
        bool check = Utils.CheckDouble(test);

        //assert
        Assert.IsTrue(check);
    }

    [TestMethod]
    public void CheckDouble_LessThan0_returnsFalse()
    {
        //arrange
        double test = -10;

        //act
        bool check = Utils.CheckDouble(test);

        //assert
        Assert.IsFalse(check);
    }

    [TestMethod]
    public void CheckString_validString_ReturnsTrue()
    {
        //arrange
        string test = "hello";

        //act
        bool check = Utils.CheckString(test);

        //assert
        Assert.IsTrue(check);
    }

    [TestMethod]
    public void CheckString_whitespaceString_ReturnsFalse()
    {
        //arrange
        string test = "  ";

        //act
        bool check = Utils.CheckString(test);

        //assert
        Assert.IsFalse(check);
    }

    [TestMethod]
    public void CheckString_emptyString_ReturnsFalse()
    {
        //arrange
        string test = "";

        //act
        bool check = Utils.CheckString(test);

        //assert
        Assert.IsFalse(check);
    }

    [TestMethod]
    public void CheckName_validName_returnsTrue()
    {
        //arrange
        string name = "Adam";
        //act
        bool check = Utils.CheckName(name);
        //assert
        Assert.IsTrue(check);
    }
    [TestMethod]
    public void CheckName_NameMoreThan30_returnsFalse()
    {
        //arrange
        string name = "I am more than 30 characters long";
        //act
        bool check = Utils.CheckName(name);
        //assert
        Assert.IsFalse(check);
    }

    [TestMethod]
    public void CheckName_emptyName_returnsFalse()
    {
        //arrange
        string name = "";
        //act
        bool check = Utils.CheckName(name);
        //assert
        Assert.IsFalse(check);
    }
    [TestMethod]
    public void CheckName_whitespaceName_returnsFalse()
    {
        //arrange
        string name = "      ";
        //act
        bool check = Utils.CheckName(name);
        //assert
        Assert.IsFalse(check);
    }

    [TestMethod]
    public void CheckLongString_LengthLessThan2000_returnsTrue()
    {
        //arrange
        string test = "i am less than 2000 characters";
        //act
        bool check = Utils.CheckLongString(test);
        //assert
        Assert.IsTrue(check);
    }

    [TestMethod]
    public void CheckLongString_TooLongString_returnsFalse()
    {
        //arrange
        string test = "";
        for (int i = 0; i < 2001; i++)
        {
            test += "a";
        }
        //act
        bool check = Utils.CheckLongString(test);
        //assert
        Assert.IsFalse(check);
    }

    [TestMethod]
    public void CheckInt_GreaterThan0_returnsTrue()
    {
        //arrange
        int test = 1;
        //act
        bool check = Utils.CheckInt(test);
        //assert
        Assert.IsTrue(check);
    }

    [TestMethod]
    public void CheckInt_LessThan0_returnsFalse()
    {
        //arrange
        int test = -1;
        //act
        bool check = Utils.CheckInt(test);
        //assert
        Assert.IsFalse(check);
    }

    [TestMethod]
    public void CheckScore_Between0And5_returnsTrue()
    {
        //arrange
        int test = 2;
        //act
        bool check = Utils.CheckScore(test);
        //assert
        Assert.IsTrue(check);
    }

    [TestMethod]
    public void CheckScore_LessThan0_returnsFalse()
    {
        //arrange
        int test = -1;
        //act
        bool check = Utils.CheckScore(test);
        //assert
        Assert.IsFalse(check);
    }

    [TestMethod]
    public void CheckScore_GreaterThan5_returnsFalse()
    {
        //arrange
        int test = 6;
        //act
        bool check = Utils.CheckScore(test);
        //assert
        Assert.IsFalse(check);
    }

    [TestMethod]
    public void CheckList_NonEmptyListStrings_ReturnsTrue()
    {
        //arrange
        List<string> list = new() { "hi" };
        //act
        bool check = Utils.CheckList(list);
        //assert
        Assert.IsTrue(check);
    }

    [TestMethod]
    public void CheckList_NonEmptyListIngredient_ReturnsTrue()
    {
        //arrange
        List<Ingredient> list = new() { new Ingredient("name", "quantity", 10) };
        //act
        bool check = Utils.CheckList(list);
        //assert
        Assert.IsTrue(check);
    }

    [TestMethod]
    public void CheckList_EmptyListString_ReturnsFalse()
    {
        //arrange
        List<string> list = new() { };
        //act
        bool check = Utils.CheckList(list);
        //assert
        Assert.IsFalse(check);
    }

    [TestMethod]
    public void CheckList_EmptyListIngredient_ReturnsFalse()
    {
        //arrange
        List<Ingredient> list = new() { };
        //act
        bool check = Utils.CheckList(list);
        //assert
        Assert.IsFalse(check);
    }

    [TestMethod]
    public void CheckMultiStringInput_hasCommas_returnsTrue()
    {
        //arrange
        string test = "hello, hi,goodbye";
        //act
        bool check = Utils.CheckMultiStringInput(test);
        //assert
        Assert.IsTrue(check);
    }

    [TestMethod]
    public void CheckMultiStringInput_noCommas_returnsFalse()
    {
        //arrange
        string test = "hello hi goodbye";
        //act
        bool check = Utils.CheckMultiStringInput(test);
        //assert
        Assert.IsFalse(check);
    }

    [TestMethod]
    public void CheckRecipeInList_RecipeExists_returnTrue()
    {
        //assert
        List<Recipe> list = CreateExampleList();
        Recipe recipe = new(
                name: "recipe1",
                description: "description1",
                cookingTime: 3,
                preparation: new List<string>(){"do"},
                servings: 2,
                ratings: 2,
                ingredients: new List<Ingredient>{new Ingredient("cheese","2",3)},
                favourite: 2,
                owner: new User("Joe", "password", "Joe", 30),
                tags: new List<string> { "vegetarian", "vegan" },
                reviews: new List<Review> { new("reviewer1", "review1",0), new Review("reviewer2", "review2",0) });
        //act
        bool check = Utils.CheckRecipeInList(list, recipe.Name);

        //assert
        Assert.IsTrue(check);
    }

        [TestMethod]
    public void CheckRecipeInList_RecipeDoesntExists_returnFalse()
    {
        //assert
        List<Recipe> list = CreateExampleList();
        Recipe recipe = new(
                name: "I dont exist",
                description: "description1",
                cookingTime: 3,
                preparation: new List<string>(){"do"},
                servings: 2,
                ratings: 2,
                ingredients: new List<Ingredient>{new Ingredient("cheese","2",3)},
                favourite: 2,
                owner: new User("Joe", "password", "Joe", 30),
                tags: new List<string> { "vegetarian", "vegan" },
                reviews: new List<Review> { new("reviewer1", "review1",0), new Review("reviewer2", "review2",0) });
        //act
        bool check = Utils.CheckRecipeInList(list, recipe.Name);

        //assert
        Assert.IsFalse(check);
    }

    private List<Recipe> CreateExampleList()
    {
        List<string> tags = new List<string> { "vegetarian", "vegan" };
        List<string> review = new List<string> { "review1", "review2" };
        List<Recipe> recipes = new List<Recipe>{
            new(
                name: "recipe1",
                description: "description1",
                cookingTime: 3,
                preparation: new List<string>(){"do"},
                servings: 2,
                ratings: 2,
                ingredients: new List<Ingredient>{new Ingredient("cheese","2",3)},
                favourite: 2,
                owner: new User("Joe", "password", "Joe", 30),
                tags: tags,
                reviews: new List<Review> { new("reviewer1", "review1",0), new Review("reviewer2", "review2",0) }),
            new(
                name: "recipe2",
                description: "description2",
                cookingTime: 0,
                preparation: new List<string>(){"dont do"},
                servings: 1,
                ratings: 3,
                ingredients: new List<Ingredient> {new Ingredient("carrot", "2", 3)},
                favourite: 0,
                owner:new User("Boe", "password", "Boe", 30),
                tags: tags,
                reviews: new List<Review> { new("reviewer1", "review1",0) })};

        return recipes;
    }
}

//arrange
//act
//assert
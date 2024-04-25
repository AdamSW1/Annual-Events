using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;
using sorters;
using System;
using System.Net.Http.Headers;
using System.Diagnostics;
using RecipeInfo;
namespace annual_events_test;

[TestClass]

public class SearchTest
{
    [TestMethod]
    public void SearchRecipesByKeywordTest()
    {
        //Arrange
        List<string> tags = new List<string> { "vegetarian", "vegan" };
        List<Recipe> recipes = CreateExampleList();
        Search search = new Search(recipes);
        List<Recipe> expected_recipes = new List<Recipe> { recipes[0] };
        //Act
        List<Recipe> newRecipes = search.SearchRecipesByKeyword("recipe1");
        //Assert
        CollectionAssert.AreEquivalent(newRecipes, expected_recipes);
    }
    [TestMethod]
    public void SearchRecipesByTagsTest()
    {
        //Arrange
        List<string> tags = new List<string> { "vegetarian", "vegan" };
        List<Recipe> recipes = CreateExampleList();
        Search search = new Search(recipes);
        //Act
        List<Recipe> newRecipes = search.SearchRecipesByTags(tags);
        //Assert
        CollectionAssert.AreEquivalent(newRecipes, recipes);
    }
    [TestMethod]
    public void SearchRecipesByTimeConstraintTest()
    {
        //Arrange
        List<string> tags = new List<string> { "vegetarian", "vegan" };
        List<Recipe> recipes = CreateExampleList();
        Search search = new Search(recipes);
        //Act
        List<Recipe> newRecipes = search.SearchRecipesByTimeConstraint(5);
        //Assert
        Assert.AreEqual(newRecipes.Count, 1);
    }
    [TestMethod]
    public void SearchRecipesByRatingTest()
    {
        //Arrange
        List<string> tags = new List<string> { "vegetarian", "vegan" };
        List<Recipe> recipes = CreateExampleList();
        Search search = new Search(recipes);
        //Act
        List<Recipe> newRecipes = search.SearchRecipesByRating(2);
        Assert.AreEqual(newRecipes.Count, 1);
    }
    [TestMethod]
    public void SearchRecipesByServingsTest()
    {
        //Arrange
        List<string> tags = new List<string> { "vegetarian", "vegan" };
        List<Recipe> recipes = CreateExampleList();
        Search search = new Search(recipes);
        //Act
        List<Recipe> newRecipes = search.SearchRecipesByServings(1);
        //Assert
        Assert.AreEqual(newRecipes.Count, 1);
    }
    [TestMethod]
    public void SearchRecipesInFavoritesTest()
    {
        //Arrange
        List<string> tags = new List<string> { "vegetarian", "vegan" };
        List<Recipe> recipes = CreateExampleList();
        Search search = new Search(recipes);
        //Act
        List<Recipe> newRecipes = search.SearchRecipesInFavorites(0);
        //Assert
        Assert.AreEqual(newRecipes.Count, 2);
    }
    [TestMethod]
    public void SearchRecipesByOwnerUsernameTest()
    {
        //Arrange
        List<string> tags = new List<string> { "vegetarian", "vegan" };
        List<string> review = new List<string> { "review1", "review2" };
        List<Recipe> recipes = CreateExampleList();
        Search search = new Search(recipes);
        //Act
        List<Recipe> newRecipes = search.SearchRecipesByOwnerUsername("Joe");
        //Assert
        Assert.AreEqual(newRecipes[0].Owner.Username, "Joe");
    }

    private List<Recipe> CreateExampleList()
    {
        List<string> tags = new List<string> { "vegetarian", "vegan" };
        List<string> review = new List<string> { "review1", "review2" };
        List<Recipe> recipes = new List<Recipe>{
            new Recipe(
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
            new Recipe(
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
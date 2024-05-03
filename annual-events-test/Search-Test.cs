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
        List<RecipeTag> tags = new List<RecipeTag>(){new("vegan"),new("vegetarian")};
        List<Recipe> recipes = CreateExampleList();
        Search search = new(recipes);
        List<Recipe> expected_recipes = new() { recipes[0] };
        //Act
        List<Recipe> newRecipes = search.SearchRecipesByKeyword("recipe1");
        //Assert
        CollectionAssert.AreEquivalent(newRecipes, expected_recipes);
    }
    [TestMethod]
    public void SearchRecipesByTagsTest()
    {
        //Arrange
        List<RecipeTag> tags = new List<RecipeTag>(){new("vegan"),new("vegetarian")};
        List<Recipe> recipes = CreateExampleList();
        Search search = new(recipes);
        //Act
        List<Recipe> newRecipes = search.SearchRecipesByTags(tags)!;
        //Assert
        CollectionAssert.AreEquivalent(newRecipes, recipes);
    }
    [TestMethod]
    public void SearchRecipesByTimeConstraintTest()
    {
        //Arrange
        List<RecipeTag> tags = new List<RecipeTag>(){new("vegan"),new("vegetarian")};
        List<Recipe> recipes = CreateExampleList();
        Search search = new(recipes);
        //Act
        List<Recipe> newRecipes = search.SearchRecipesByTimeConstraint(5);
        //Assert
        Assert.AreEqual(newRecipes.Count, 1);
    }
    [TestMethod]
    public void SearchRecipesByRatingTest()
    {
        //Arrange
        List<RecipeTag> tags = new List<RecipeTag>(){new("vegan"),new("vegetarian")};
        List<Recipe> recipes = CreateExampleList();
        Search search = new(recipes);
        //Act
        List<Recipe> newRecipes = search.SearchRecipesByRating(2);
        Assert.AreEqual(newRecipes.Count, 1);
    }
    [TestMethod]
    public void SearchRecipesByServingsTest()
    {
        //Arrange
        List<RecipeTag> tags = new List<RecipeTag>(){new("vegan"),new("vegetarian")};
        List<Recipe> recipes = CreateExampleList();
        Search search = new(recipes);
        //Act
        List<Recipe> newRecipes = search.SearchRecipesByServings(1);
        //Assert
        Assert.AreEqual(newRecipes.Count, 1);
    }
    [TestMethod]
    public void SearchRecipesInFavoritesTest()
    {
        //Arrange
        List<RecipeTag> tags = new List<RecipeTag>(){new("vegan"),new("vegetarian")};
        List<Recipe> recipes = CreateExampleList();
        Search search = new(recipes);
        //Act
        List<Recipe> newRecipes = search.SearchRecipesInFavorites(0);
        //Assert
        Assert.AreEqual(newRecipes.Count, 1);
    }
    [TestMethod]
    public void SearchRecipesByOwnerUsernameTest()
    {
        //Arrange
        List<RecipeTag> tags = new List<RecipeTag>(){new("vegan"),new("vegetarian")};
        List<string> review = new() { "review1", "review2" };
        List<Recipe> recipes = CreateExampleList();
        Search search = new(recipes);
        //Act
        List<Recipe> newRecipes = search.SearchRecipesByOwnerUsername("Joe");
        //Assert
        Assert.AreEqual(newRecipes[0].Owner.Username, "Joe");
    }

    private List<Recipe> CreateExampleList()
    {
        List<RecipeTag> tags = new List<RecipeTag>(){new("vegan"),new("vegetarian")};
        List<string> review = new() { "review1", "review2" };
        List<Recipe> recipes = new()
        {
            new(
                name: "recipe1",
                description: "description1",
                cookingTime: 3,
                preparation: new List<Preparation>(){new(1,"do")},
                servings: 2,
                ingredients: new List<RecipeIngredient>{new() { Ingredient = new Ingredient("carrot",3)}},
                favourite: 2,
                owner: new Annual_Events_User("Joe", "password", "Joe", 30),
                tags: tags,
                reviews: new List<Review> { new("reviewer1", "review1",0), new("reviewer2", "review2",0) }),
            new(
                name: "recipe2",
                description: "description2",
                cookingTime: 0,
                preparation: new List<Preparation>(){new(1,"do")},
                servings: 1,
                ingredients: new List<RecipeIngredient>{new() { Ingredient = new Ingredient("carrot",3)}},
                favourite: 0,
                owner:new Annual_Events_User("Boe", "password", "Boe", 30),
                tags: tags,
                reviews: new List<Review> { new("reviewer1", "review1",2) })};

        return recipes;
    }
    
}
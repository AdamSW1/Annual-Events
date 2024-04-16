using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;
using sorters;
using System;
using System.Net.Http.Headers;
using System.Diagnostics;
using RecipeInfo;
namespace tests;

[TestClass]

public class SearchTest
{
    [TestMethod]
    public void SearchRecipesByKeywordTest()
    {
        throw new NotImplementedException();
    }
    [TestMethod]
    public void SearchRecipesByTagsTest()
    {
        List<string> tags = new List<string>{"vegetarian","vegan"};
        List<Recipe> recipes = new List<Recipe>{new Recipe("recipe1","description1",RecipeTags.vegetarian,RecipeTags.vegan,30,"instruction1",null)};

        throw new NotImplementedException();
    }
    [TestMethod]
    public void SearchRecipesByTimeConstraintTest()
    {
        throw new NotImplementedException();
    }
    [TestMethod]
    public void SearchRecipesByRatingTest()
    {
        throw new NotImplementedException();
    }
    [TestMethod]
    public void SearchRecipesByServingsTest()
    {
        throw new NotImplementedException();
    }
    [TestMethod]
    public void SearchRecipesInFavoritesTest()
    {
        throw new NotImplementedException();
    }
    [TestMethod]
    public void SearchRecipesByOwnerUsernameTest()
    {
        throw new NotImplementedException();
    }
}
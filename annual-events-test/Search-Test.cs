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
        //Arrange
        Utils utils = new Utils();
        List<string> tags = new List<string>{"vegetarian","vegan"};
        List<RecipeTags> tags_expected = new List<RecipeTags>{RecipeTags.vegetarian,RecipeTags.vegan};
        //Act
        List<RecipeTags> tags_test = utils.ValidateTags(tags);
        //Assert
        CollectionAssert.AreEquivalent(tags_test,tags_expected);
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
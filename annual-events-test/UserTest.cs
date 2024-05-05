using System;
using System.Net.Http.Headers;
using System.Diagnostics;
using RecipeInfo;
using BusinessLayer;
namespace annual_events_test;

[TestClass]
public class UserTest
{
    //Test for adding a recipe to a user's list
    [TestMethod]
    public void AddRecipeTest_addingOneRecipe()
    {
        //arrange
        Annual_Events_User user = new("Test","TestPass","description",20);
        Recipe test = CreateExampleRecipe(user); 
        
        //act
        user.AddRecipe(test);

        //assert
        Assert.AreEqual(test, user.Recipes[0]);
    }

    //Test for authentication
    [TestMethod]
    public void AuthenticationTest()
    {
        //arrange
        Annual_Events_User user = new("Test","TestPass","description",20);
        string name = "Test";
        string password = "TestPass";
        //act

        //assert
        Assert.IsTrue(user.Authentication(name,password));

    }
    private Recipe CreateExampleRecipe(Annual_Events_User cur){
        Ingredient flour = new("flour", 7);
        Ingredient egg = new("egg", 3);
        List<Ingredient> ingredients = new() { flour, egg };
        List<RecipeTag> tags = new List<RecipeTag>(){new("cake"),new("chocolate")};
        List<RecipeIngredient> recipIngredients = new List<RecipeIngredient>{new RecipeIngredient{Ingredient = new Ingredient("carrot",3)}};
        Recipe exampleRecipe = new("Chocolate cake",
                                            "A simple chocolate cake",
                                            120,
                                            new List<Preparation>{ new(1, "do")},
                                            8,
                                            recipIngredients,
                                            0,
                                            cur,
                                            tags,null
                                            );

        return exampleRecipe;
    }
}
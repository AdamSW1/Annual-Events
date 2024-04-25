using System;
using System.Net.Http.Headers;
using System.Diagnostics;
using RecipeInfo;
using BusinessLayer;
namespace annual_events_test;

[TestClass]
public class UserTest
{
    //Test for creating a user in the database
    [TestMethod]
    public void CreateUserTest()
    {
        throw new NotImplementedException("No Database to test");
    }
    //Test for adding a recipe to a user's list
    [TestMethod]
    public void AddRecipeTest_addingOneRecipe()
    {
        //arrange
        Annual_Events_User user = new Annual_Events_User("Test","TestPass","description",20);
        Recipe test = CreateExampleRecipe(user); 
        
        //act
        user.AddRecipe(test);

        //assert
        Assert.AreEqual(test, user.Recipes[0]);
    }

    
    //Test for step verification
    [TestMethod]
    public void StepVerificationTest()
    {
        throw new NotImplementedException();
    }
    //Test for hashed password
    [TestMethod]
    public void HashedPasswordTest()
    {
        throw new NotImplementedException();
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
    //Test for deleting a user
    [TestMethod]
    public void DeleteUserTest()
    {
        throw new NotImplementedException("No database to test");
    }

    private Recipe CreateExampleRecipe(Annual_Events_User cur){
        Ingredient flour = new Ingredient("flour", "6 cups", 7);
        Ingredient egg = new Ingredient("egg", "4", 3);
        List<Ingredient> ingredients = new List<Ingredient>() { flour, egg };
        List<string> tags = new List<string>() { "cake", "chocolate" };
        Recipe exampleRecipe = new Recipe("Chocolate cake",
                                            "A simple chocolate cake",
                                            120,
                                            new List<Preparation>{ new Preparation(1, "do")},
                                            8,
                                            ingredients,
                                            0,
                                            cur,
                                            tags,null
                                            );

        return exampleRecipe;
    }
}
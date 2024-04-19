using System;
using System.Net.Http.Headers;
using System.Diagnostics;
using RecipeInfo;
namespace annual_events_test;

[TestClass]
public class UserTest
{
    //Test for creating a user
    [TestMethod]
    public void CreateUserTest()
    {
        throw new NotImplementedException();
    }
    //Test for adding a recipe to a user's list
    [TestMethod]
    public void AddToFavRecipeTest()
    {
        throw new NotImplementedException();
    }
    //Test for adding a recipe to a user's list
    [TestMethod]
    public void AddRecipeTest_addingOneRecipe()
    {
        //arrange
        User user = new User("Test","TestPass","description",20);
        Recipe test = CreateExampleRecipe(user); 
        
        //act
        user.AddRecipe(test);

        //assert
        Assert.AreEqual(test, user.Recipes[0]);
    }


    //Test for removing a recipe from a user's list
    [TestMethod]
    public void RemoveFromFavRecipeTest()
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }
    //Test for deleting a user
    [TestMethod]
    public void DeleteUserTest()
    {
        throw new NotImplementedException();
    }

    private Recipe CreateExampleRecipe(User cur){
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
                                            cur,
                                            tags,null
                                            );

        return exampleRecipe;
    }
}
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
        User user = new User("Test","TestPass","description",20);
        Recipe test = CreateExampleRecipe(user); 
        
        //act
        user.AddRecipe(test);

        //assert
        Assert.AreEqual(test, user.Recipes[0]);
    }

    //Test for adding a recipe to a user's list
    [TestMethod]
    public void AddToFavRecipeTest()
    {
        //arrange
        User user1 = new User("Test","TestPass","description",20);
        User user2 = new User("Test","TestPass","description",20);
        Recipe test = CreateExampleRecipe(user1);

        //act
        user1.AddRecipe(test);
        user2.AddToFavRecipe(test);

        //assert
        Assert.AreEqual(test, user2.FavRecipes[0]);
    }

    //Test for removing a recipe from a user's list
    [TestMethod]
    public void RemoveFromFavRecipeTest()
    {
        //arrange
        User user1 = new User("Test","TestPass","description",20);
        User user2 = new User("Test","TestPass","description",20);
        Recipe test = CreateExampleRecipe(user1);

        //act
        user2.AddToFavRecipe(test);
        user2.RemoveFromFavRecipe(test);
        

        //assert
        CollectionAssert.DoesNotContain(user2.FavRecipes, test);

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
        User user = new("Test","TestPass","description",20);
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
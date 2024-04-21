using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;
using System;
using RecipeInfo;
using System.Reflection;
namespace annual_events_test;

[TestClass]
public class RecipeTest
{
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

    //Test for creating a recipe
    [TestMethod]
    public void CreateRecipeTest()
    {
        throw new NotImplementedException();
    }
    //Test for updating a recipe
    [TestMethod]
    public void UpdateRecipeTest()
    {
        throw new NotImplementedException();
    }
    //Test for deleting a recipe
    [TestMethod]
    public void DeleteRecipeTest()
    {
        throw new NotImplementedException();
    }
    //Test for rating a recipe
    [TestMethod]
    public void RateRecipeTest()
    {
        throw new NotImplementedException();
    }

    [TestMethod]
    public void AddRecipe_ValidInput_Success()
    {
        // Arrange
        var user = new User("testUser", "password", "Test user", 30);
        Recipe test = CreateExampleRecipe(user);

        // Act
        // Add the recipe to the user's list
        user.AddRecipe(test);

        // Assert
        Assert.AreEqual(1, user.Recipes.Count); // User should have 1 recipe added
    }

    [TestMethod]
    public void AddRecipe_IncorrectRecipeAmount_Success()
    {
        // Arrange
        var user = new User("testUser2", "password2", "Test user 2", 30);
        Recipe test = CreateExampleRecipe(user);

        // Act
        // Add the recipe to the user's list
        user.AddRecipe(test);

        // Assert
        // User should only have 1 recipe added
        Assert.AreNotEqual(2, user.Recipes.Count);
    }

    [TestMethod]
    public void DeleteRecipe_ExistingRecipe_DeletesSuccessfully()
    {
        // Arrange
        var user = new User("testUser", "password", "Test user", 30);
        Recipe test = CreateExampleRecipe(user);
        user.AddRecipe(test);

        // Act
        RecipeManager.DeleteRecipe(user, test);

        // Assert
        Assert.AreEqual(0, user.Recipes.Count, "Recipe should be deleted");
    }
    
    [TestMethod]
    public void AddRecipeToFav_ValidInput_Success()
    {
        // Arrange
        User user = new User("testUser", "password", "Test user", 30);
        Recipe test = CreateExampleRecipe(user);

        // Act
        // Add the recipe to the user's list
        user.AddToFavRecipe(test);

        // Assert
        Assert.AreEqual(1, user.FavRecipes.Count); // User should have 1 recipe added
    }

    [TestMethod]
    public void AddFavRecipe_IncorrectFavRecipeAmount_Success()
    {
        // Arrange
        var user = new User("testUser2", "password2", "Test user 2", 30);
        Recipe test = CreateExampleRecipe(user);

        // Act
        // Add the recipe to the user's list
        user.AddRecipe(test);

        // Assert
        // User should only have 1 recipe added
        Assert.AreNotEqual(2, user.FavRecipes.Count);
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

    [TestMethod]
    public void UpdateRecipe_WithExistingRecipe_Success()
    {
        // Arrange
        var user = new User("testUser2", "password2", "Test user 2", 30);
        Recipe test = CreateExampleRecipe(user);
        user.AddRecipe(test);
        string updatedRecipeName = "Updated Test Recipe";
        string updatedDescription = "Updated Test Description";
        double updatedCookingTime = 75;
        string updatedPreparation = "Updated Test Preparation";
        int updatedServings = 6;
        int updatedRatings = 4;
        // Act
        // Update the recipe
        bool result = user.UpdateRecipe(test.Name, updatedRecipeName, updatedDescription, updatedCookingTime, updatedPreparation, updatedServings, updatedRatings);
        
        // Assert
        // User should only have 1 recipe added
        Recipe updatedRecipe = user.Recipes.FirstOrDefault(r => r.Name == updatedRecipeName);
        Assert.IsNotNull(updatedRecipe); // Ensure that the recipe exists in the user's recipes
        Assert.AreEqual(updatedDescription, updatedRecipe.Description);
        Assert.AreEqual(updatedCookingTime, updatedRecipe.CookingTime);
        Assert.AreEqual(updatedPreparation, updatedRecipe.Preparation);
        Assert.AreEqual(updatedServings, updatedRecipe.Servings);
        Assert.AreEqual(updatedRatings, updatedRecipe.Ratings);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void UpdateRecipe_WithNonExistingRecipe_Success()
    {
        // Arrange
        var user = new User("testUser2", "password2", "Test user 2", 30);
        Recipe test = CreateExampleRecipe(user);
        user.AddRecipe(test);
        string updatedRecipeName = "Updated Test Recipe";
        string updatedDescription = "Updated Test Description";
        double updatedCookingTime = 75;
        string updatedPreparation = "Updated Test Preparation";
        int updatedServings = 6;
        int updatedRatings = 4;
        // Act
        // Update the recipe
        bool result = user.UpdateRecipe("Testing failure", updatedRecipeName, updatedDescription, updatedCookingTime, updatedPreparation, updatedServings, updatedRatings);

        // Assert
        // User should only have 1 recipe added
        Assert.IsFalse(result);
    }
}
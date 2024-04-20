using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;
using System;
using RecipeInfo;
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
        RecipeManager.DeleteRecipe(user, test.Name);

        // Assert
        Assert.AreEqual(0, user.Recipes.Count, "Recipe should be deleted");
    }

    [TestMethod]
    public void DeleteRecipe_NonExistingRecipe_NothingDeleted()
    {
        // Arrange
        var user = new User("testUser", "password", "Test user", 30);
        Recipe test = CreateExampleRecipe(user);
        user.AddRecipe(test);
        var recipeToDelete = "Non-existing Recipe";

        // Act
        RecipeManager.DeleteRecipe(user, recipeToDelete);

        // Assert
        Assert.AreEqual(1, user.Recipes.Count, "No recipe should be deleted");
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

    [TestMethod]
    public void DeleteFavRecipe_NonExistingRecipe_NothingDeleted()
    {
        // Arrange
        var user = new User("testUser", "password", "Test user", 30);
        Recipe test = CreateExampleRecipe(user);
        user.AddRecipe(test);
        var favRecipeToDelete = "Non-existing Recipe";

        // Act
        RecipeManager.DeleteFavRecipe(user, favRecipeToDelete);

        // Assert
        Assert.AreEqual(0, user.FavRecipes.Count, "No recipe should be deleted");
    }

    [TestMethod]
    public void DeleteFavRecipe_ExistingRecipe_Successful()
    {
        // Arrange
        var user = new User("testUser", "password", "Test user", 30);
        Recipe test = CreateExampleRecipe(user);
        user.AddRecipe(test);
        var recipeToDelete = "Chocolate cake";

        // Act
        RecipeManager.DeleteRecipe(user, recipeToDelete);

        // Assert
        Assert.AreEqual(0, user.FavRecipes.Count, "Fav recipe should be deleted");
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;
using System;
using RecipeInfo;
namespace annual_events_test;

[TestClass]
public class RecipeTest
{
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
    public void AddFavourite_plusOne_expect2favourites()
    {
        throw new NotImplementedException();
    }

    [TestMethod]
    public void AddFavourite_removeOne_expect1favourite()
    {
        throw new NotImplementedException();
    }

    [TestMethod]
    public void AddRecipe_ValidInput_Success()
    {
        // Arrange
        var user = new User("testUser", "password", "Test user", 30);
        var recipeManager = new RecipeManager();
        string recipeName = "Test Recipe";
        string description = "Test Description";
        double cookingTime = 60;
        string preparation = "Test Preparation";
        int servings = 4;
        int ratings = 5;
        var ingredients = new List<Ingredient> { new Ingredient("Ingredient 1", "100g", 5.99) };
        var tags = new List<string> { "vegetarian", "vegan" };

        // Act
        Recipe newRecipe = new Recipe(recipeName, description, cookingTime, preparation, servings, ratings, ingredients, 0, user, tags,null);

        // Add the recipe to the user's list
        user.AddRecipe(newRecipe);

        // Assert

        Assert.AreEqual(1, user.Recipes.Count); // User should have 1 recipe added
    }

    [TestMethod]
    public void AddRecipe_IncorrectRecipeAmount_Success()
    {
        // Arrange
        var user = new User("testUser2", "password2", "Test user 2", 30);
        var recipeManager = new RecipeManager();
        string recipeName = "Test Recipe";
        string description = "Test Description";
        double cookingTime = 60;
        string preparation = "Test Preparation";
        int servings = 4;
        int ratings = 5;
        var ingredients = new List<Ingredient> { new Ingredient("Ingredient 1", "100g", 5.99) };
        var tags = new List<string> { "vegetarian", "vegan" };

        // Act
        Recipe newRecipe = new Recipe(recipeName, description, cookingTime, preparation, servings, ratings, ingredients, 0, user, tags,null);

        // Add the recipe to the user's list
        user.AddRecipe(newRecipe);

        // Assert
        // User should only have 1 recipe added
        Assert.AreNotEqual(2, user.Recipes.Count);
    }

    [TestMethod]
    public void DeleteRecipe_ExistingRecipe_DeletesSuccessfully()
    {
        // Arrange
        var user = new User("testUser", "password", "Test user", 30);
        var recipeName = "Test Recipe";
        var description = "Test Description";
        double cookingTime = 60;
        string preparation = "Test Preparation";
        int servings = 4;
        int ratings = 5;
        var ingredients = new List<Ingredient> { new Ingredient("Ingredient 1", "100g", 5.99) };
        var tags = new List<string> { "vegetarian", "vegan" };
        List<string> reviews = new List<string>{"review1", "review2"};
        var newRecipe = new Recipe(recipeName, description, cookingTime, preparation, servings, ratings, ingredients, 0, user, tags, reviews);
        user.AddRecipe(newRecipe);

        // Act
        RecipeManager.DeleteRecipe(user, recipeName);

        // Assert
        Assert.AreEqual(0, user.Recipes.Count, "Recipe should be deleted");
    }

    [TestMethod]
    public void DeleteRecipe_NonExistingRecipe_NothingDeleted()
    {
        // Arrange
        var user = new User("testUser", "password", "Test user", 30);
        var recipeManager = new RecipeManager();
        var recipeName = "Test Recipe";
        var description = "Test Description";
        double cookingTime = 60;
        string preparation = "Test Preparation";
        int servings = 4;
        int ratings = 5;
        var ingredients = new List<Ingredient> { new Ingredient("Ingredient 1", "100g", 5.99) };
        var tags = new List<string> { "vegetarian", "vegan" };
        List<string> reviews = new List<string>{"review1", "review2"};
        var newRecipe = new Recipe(recipeName, description, cookingTime, preparation, servings, ratings, ingredients, 0, user, tags, reviews);
        user.AddRecipe(newRecipe);
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
        string recipeName = "Test Recipe";
        string description = "Test Description";
        double cookingTime = 60;
        string preparation = "Test Preparation";
        int servings = 4;
        int ratings = 5;
        var ingredients = new List<Ingredient> { new Ingredient("Ingredient 1", "100g", 5.99) };
        var tags = new List<string> { "vegetarian", "vegan" };
        List<string> reviews = new List<string>{"review1", "review2"};

        // Act
        Recipe newRecipe = new Recipe(recipeName, description, cookingTime, preparation, servings, ratings, ingredients, 0, user, tags, reviews);

        // Add the recipe to the user's list
        user.AddToFavRecipe(newRecipe);

        // Assert

        Assert.AreEqual(1, user.FavRecipes.Count); // User should have 1 recipe added
    }

    [TestMethod]
    public void AddFavRecipe_IncorrectFavRecipeAmount_Success()
    {
        // Arrange
        var user = new User("testUser2", "password2", "Test user 2", 30);
        var recipeManager = new RecipeManager();
        string recipeName = "Test Recipe";
        string description = "Test Description";
        double cookingTime = 60;
        string preparation = "Test Preparation";
        int servings = 4;
        int ratings = 5;
        var ingredients = new List<Ingredient> { new Ingredient("Ingredient 1", "100g", 5.99) };
        var tags = new List<string> { "vegetarian", "vegan" };
        List<string> reviews = new List<string>{"review1", "review2"};

        // Act
        Recipe newRecipe = new Recipe(recipeName, description, cookingTime, preparation, servings, ratings, ingredients, 0, user, tags, reviews);

        // Add the recipe to the user's list
        user.AddRecipe(newRecipe);

        // Assert
        // User should only have 1 recipe added
        Assert.AreNotEqual(2, user.FavRecipes.Count);
    }

    [TestMethod]
    public void DeleteFavRecipe_NonExistingRecipe_NothingDeleted()
    {
        // Arrange
        var user = new User("testUser", "password", "Test user", 30);
        var recipeName = "Test Recipe";
        var description = "Test Description";
        double cookingTime = 60;
        string preparation = "Test Preparation";
        int servings = 4;
        int ratings = 5;
        var ingredients = new List<Ingredient> { new Ingredient("Ingredient 1", "100g", 5.99) };
        var tags = new List<string> { "vegetarian", "vegan" };
        List<string> reviews = new List<string>{"review1", "review2"};
        var newRecipe = new Recipe(recipeName, description, cookingTime, preparation, servings, ratings, ingredients, 0, user, tags, reviews);
        user.AddRecipe(newRecipe);
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
        var recipeManager = new RecipeManager();
        var recipeName = "Test Recipe";
        var description = "Test Description";
        double cookingTime = 60;
        string preparation = "Test Preparation";
        int servings = 4;
        int ratings = 5;
        var ingredients = new List<Ingredient> { new Ingredient("Ingredient 1", "100g", 5.99) };
        var tags = new List<string> { "vegetarian", "vegan" };
        List<string> reviews = new List<string>{"review1", "review2"};
        var newRecipe = new Recipe(recipeName, description, cookingTime, preparation, servings, ratings, ingredients, 0, user, tags, reviews);
        user.AddRecipe(newRecipe);
        var recipeToDelete = "Test Recipe";

        // Act
        RecipeManager.DeleteRecipe(user, recipeToDelete);

        // Assert
        Assert.AreEqual(0, user.FavRecipes.Count, "Fav recipe should be deleted");
    }
}
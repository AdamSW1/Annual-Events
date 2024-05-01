// RecipeManager.cs
using System.Linq;
using System.Collections.Generic;
using BusinessLayer;
namespace RecipeInfo;


public class RecipeManager
{
    public static void AddRecipe(Annual_Events_User user, string recipeName, string description,
        double cookingTime, List<Preparation> preparation,
        int servings, List<Ingredient> ingredients,
        List<RecipeTag> tags)
    {
        // Create recipe
        Recipe newRecipe = new(recipeName, description, cookingTime, preparation, servings, ingredients, 0, user, tags, new List<Review>());

        // Add the recipe to the user's list
        user.AddRecipe(newRecipe);
    }

    //Will delete from the DB in the future, maybe?
    public static void DeleteRecipe(Annual_Events_User user, Recipe recipeToDelete)
    {
        user.DeleteRecipe(recipeToDelete);
    }

    public static void AddToFavRecipe(Annual_Events_User user, Recipe recipeToAdd)
    {
        user.AddToFavRecipe(recipeToAdd);
    }
    public static void DeleteFavRecipe(Annual_Events_User user, Recipe recipeToDelete)
    {
        user.RemoveFromFavRecipe(recipeToDelete);
    }

    public static void UpdateRecipe(string newName, string newDescription, double newCookingTime, List<Preparation> newPreparation, int newServings, Recipe recipeToUpdate)
    {
            recipeToUpdate.Name = newName;
            recipeToUpdate.Description = newDescription;
            recipeToUpdate.CookingTime = newCookingTime;
            recipeToUpdate.Preparation = newPreparation;
            recipeToUpdate.Servings = newServings;
    }
}

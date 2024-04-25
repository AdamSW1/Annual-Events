// RecipeManager.cs
using System.Linq;
using System.Collections.Generic;
using BusinessLayer;
namespace RecipeInfo;


public class RecipeManager
{
    public static void AddRecipe(User user, string recipeName, string description,
        double cookingTime, List<string> preparation,
        int servings, double ratings, List<Ingredient> ingredients,
        List<string> tags)
    {
        // Create recipe
        Recipe newRecipe = new(recipeName, description, cookingTime, preparation, servings, ratings, ingredients, 0, user, tags, new List<Review>());

        // Add the recipe to the user's list
        user.AddRecipe(newRecipe);
    }

    //Will delete from the DB in the future, maybe?
    public static void DeleteRecipe(User user, Recipe recipeToDelete)
    {
        user.DeleteRecipe(recipeToDelete);
    }

    public static void AddToFavRecipe(User user, Recipe recipeToAdd)
    {
        user.AddToFavRecipe(recipeToAdd);
    }
    public static void DeleteFavRecipe(User user, Recipe recipeToDelete)
    {
        user.RemoveFromFavRecipe(recipeToDelete);
    }

    public static void UpdateRecipe(string newName, string newDescription, double newCookingTime, List<string> newPreparation, int newServings, Recipe recipeToUpdate)
    {
            recipeToUpdate.Name = newName;
            recipeToUpdate.Description = newDescription;
            recipeToUpdate.CookingTime = newCookingTime;
            recipeToUpdate.Preparation = newPreparation;
            recipeToUpdate.Servings = newServings;
    }
}

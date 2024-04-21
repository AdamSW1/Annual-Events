// RecipeManager.cs
using System.Linq;
using System.Collections.Generic;
using BusinessLayer;
using RecipeInfo;

public class RecipeManager
{
    public void AddRecipe(User user, string recipeName, string description,
        double cookingTime, string preparation,
        int servings, double ratings, List<Ingredient> ingredients,
        List<string> tags)
    {
        // Create recipe
        Recipe newRecipe = new Recipe(recipeName, description, cookingTime, preparation, servings, ratings, ingredients, 0, user, tags, null);

        // Add the recipe to the user's list
        user.AddRecipe(newRecipe);
    }

    //Will delete from the DB in the future, maybe?
    public static void DeleteRecipe(User user, Recipe recipeToDelete)
    {
        user.DeleteRecipe(recipeToDelete);
    }

    public static void AddToFavRecipe(User user, string recipeName)
    {
        Recipe recipeToAdd = user.Recipes.Find(r => r.Name == recipeName);
        
        if (recipeToAdd != null)
        {
            user.AddToFavRecipe(recipeToAdd);
            Console.WriteLine($"\nRecipe '{recipeName}' added to favorites successfully!");
        }
        else
        {
            Console.WriteLine($"\nRecipe '{recipeName}' not found in your recipes.");
        }
    }

    public static void DeleteFavRecipe(User user, string recipeName)
    {
        Recipe recipeToDelete = user.Recipes.Find(r => r.Name == recipeName);
        
        if (recipeToDelete != null)
        {
            user.RemoveFromFavRecipe(recipeToDelete);
            Console.WriteLine($"\nRecipe '{recipeName}' removed from favorites successfully!");
        }
        else
        {
            Console.WriteLine($"\nRecipe '{recipeName}' not found in your recipes.");
        }
    }

    public void UpdateRecipe(string newName, string newDescription, double newCookingTime, string newPreparation, int newServings, double newRatings, Recipe recipeToUpdate)
    {
            recipeToUpdate.Name = newName;
            recipeToUpdate.Description = newDescription;
            recipeToUpdate.CookingTime = newCookingTime;
            recipeToUpdate.Preparation = newPreparation;
            recipeToUpdate.Servings = newServings;
            recipeToUpdate.Ratings = newRatings;
    }
}

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

    public static void DeleteRecipe(User user, string recipeName)
    {
        Console.WriteLine("\nDelete a Recipe:");
        
        Recipe recipeToDelete = user.Recipes.Find(r => r.Name == recipeName);
        
        if (recipeToDelete != null)
        {
            
            user.DeleteRecipe(recipeToDelete);
            Console.WriteLine($"\nRecipe '{recipeName}' deleted successfully!");
        }
        else
        {
            Console.WriteLine($"\nRecipe '{recipeName}' not found in your recipes.");
        }
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

    public static void UpdateRecipe(User user, string recipeName)
    {
        Utils utils = new Utils();
        Console.WriteLine("\nUpdate Recipe Information:");
        Recipe recipeToUpdate = user.Recipes.Find(r => r.Name == recipeName);

        if (recipeToUpdate != null)
        {
            Console.WriteLine($"Updating recipe '{recipeName}'...");

            Console.Write("New Recipe Name: ");
            string newName = Utils.CheckName();
            recipeToUpdate.Name = newName;

            Console.Write("New Description: ");
            string newDescription = Utils.CheckName100Limit();
            recipeToUpdate.Description = newDescription;

            Console.Write("New Cooking Time (minutes): ");
            double newCookingTime = Utils.CheckDouble();
            recipeToUpdate.CookingTime = newCookingTime;

            Console.Write("New Preparations: ");
            string newPreparation = Utils.CheckName100Limit();
            recipeToUpdate.Preparation = newPreparation;

            Console.Write("New Servings: ");
            int newServings = Utils.CheckServings();
            recipeToUpdate.Servings = newServings;
            
            Console.Write("New Ratings: ");
            double newRatings = Utils.CheckRatings();
            recipeToUpdate.Ratings = newRatings;
            
            Console.WriteLine($"\nRecipe '{recipeName}' updated successfully!");
        }
        else
        {
            Console.WriteLine($"\nRecipe '{recipeName}' not found in your recipes.");
        }
    }

    
}

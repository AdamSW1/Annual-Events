// RecipeManager.cs
using System.Linq;
using System.Collections.Generic;
using BusinessLayer;
using RecipeInfo;

public class RecipeManager
{
    public void AddRecipe(User user)
    {
        Console.WriteLine("\nAdd a Recipe:");
        Console.Write("Recipe Name: ");
        string recipeName = Console.ReadLine() ?? "null";
        Console.Write("Description: ");
        string description = Console.ReadLine() ?? "null";
        Console.Write("Cooking Time (minutes): ");
        double cookingTime = double.Parse(Console.ReadLine() ?? "null");
        Console.Write("Preparation: ");
        string preparation = Console.ReadLine() ?? "null";
        Console.Write("Servings: ");
        int servings = int.Parse(Console.ReadLine() ?? "null");
        Console.Write("Ratings: ");
        double ratings = double.Parse(Console.ReadLine() ?? "null");

        // Get ingredients
        List<Ingredient> ingredients = new List<Ingredient>();
        Console.WriteLine("\nEnter Ingredients (press Enter without typing to finish):");
        while (true)
        {
            Console.Write("Ingredient Name (press Enter to finish): ");
            string ingredientName = Console.ReadLine() ?? "null";
            if (string.IsNullOrWhiteSpace(ingredientName))
                break;

            Console.Write("Weight/Quantity: ");
            string quantity = Console.ReadLine() ?? "null";
            Console.Write("Price: ");
            double price = double.Parse(Console.ReadLine() ?? "null");

            ingredients.Add(new Ingredient(ingredientName, quantity, price));
        }

        // TEMP artificial tags
        List<string> tags = new List<string> { "vegetarian", "vegan" };

        // Create recipe
        Recipe newRecipe = new Recipe(recipeName, description, cookingTime, preparation, servings, ratings, ingredients, 0, user, tags,null);

        // Add the recipe to the user's list
        user.AddRecipe(newRecipe);
        
        Console.WriteLine("\nRecipe added successfully!");
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
        Console.WriteLine("\nUpdate Recipe Information:");
        Recipe recipeToUpdate = user.Recipes.Find(r => r.Name == recipeName);

        if (recipeToUpdate != null)
        {
            Console.WriteLine($"Updating recipe '{recipeName}'...");

            Console.Write("New Recipe Name: ");
            string newName = Console.ReadLine();
            recipeToUpdate.Name = newName;

            Console.Write("New Description: ");
            string newDescription = Console.ReadLine();
            recipeToUpdate.Description = newDescription;

            Console.Write("New Cooking Time (minutes): ");
            double newCookingTime = Convert.ToDouble(Console.ReadLine());
            recipeToUpdate.CookingTime = newCookingTime;

            Console.Write("New Preparations: ");
            string newPreparation = Console.ReadLine();
            recipeToUpdate.Preparation = newPreparation;

            Console.Write("New Servings: ");
            int newServings = Convert.ToInt32(Console.ReadLine());
            recipeToUpdate.Servings = newServings;
            
            Console.Write("New Ratings: ");
            int newRatings = Convert.ToInt32(Console.ReadLine());
            recipeToUpdate.Ratings = newRatings;
            
            Console.WriteLine($"\nRecipe '{recipeName}' updated successfully!");
        }
        else
        {
            Console.WriteLine($"\nRecipe '{recipeName}' not found in your recipes.");
        }
    }

    
}

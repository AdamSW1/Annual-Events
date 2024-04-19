// RecipeManager.cs
using System;
using System.Collections.Generic;
using BusinessLayer;
using RecipeInfo;

class RecipeManager
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
        int ratings = int.Parse(Console.ReadLine() ?? "null");

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


}

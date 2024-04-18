using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using BusinessLayer;
using RecipeInfo;

class Program
{

    public static void Main(string[] args)
    {
        AuthenticationManager AuthManager = new AuthenticationManager();
        // Login
        var loginCredentials = InitLogin();

        if (AuthManager.Login(loginCredentials.Item1, loginCredentials.Item2))
        {
            Console.WriteLine($"Welcome, {AuthManager.CurrentUser.Username}!");
            while(true) 
            {
                Init(AuthManager);
            }
        }
        else
        {
            Console.WriteLine("Invalid username or password.");
        }

    }


    public static void Init(AuthenticationManager AuthManager)
    {

        string[] options = new string[] { "Add a recipe", "See your recipes", "See all recipes", "LogOut\n" };

        string? choice = Utils.GetUserChoice("What do you want to do?", options);

        if (choice == null)
        {
            return;
        }
        else if (choice == options[0])
        {
            AddAnotherRecipe(AuthManager);
        }
        else if (choice == options[1])
        {
            AuthManager.CurrentUser.DisplayRecipes();
        }
        else if (choice == options[2])
        {
            DisplayAllRecipes(AuthManager);
        }
        else if (choice == options[3])
        {
            AuthManager.Logout();
            Console.WriteLine("\nLogged out.");
            Console.WriteLine("\nDo you wish to login? yes/no");
            string answer = Console.ReadLine();
            if (answer == "yes")
            {
                while (true)
                {
                    var loginCredentials = InitLogin();
                    if (AuthManager.Login(loginCredentials.Item1, loginCredentials.Item2))
                    {
                        Console.WriteLine($"Welcome, {AuthManager.CurrentUser.Username}!");
                        break; // Exit the loop if login is successful
                    }
                    else
                    {
                        Console.WriteLine("Invalid username or password. Please try again.");
                    }
                }
            }
            else
            {
                System.Environment.Exit(1);
            }
        }
    }

    public static (string, string) InitLogin()
    {
        Console.WriteLine("Login:");
        Console.Write("Username: ");
        string username = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();
        return (username, password);
    }

    public static void AddAnotherRecipe(AuthenticationManager AuthManager)
    {
        // Prompt user to add recipe
        Console.WriteLine("\nAdd a Recipe:");
        Console.Write("Recipe Name: ");
        string recipeName = Console.ReadLine();
        Console.Write("Description: ");
        string description = Console.ReadLine();
        Console.Write("Cooking Time (minutes): ");
        double cookingTime = double.Parse(Console.ReadLine());
        Console.Write("Preparation: ");
        string preparation = Console.ReadLine();
        Console.Write("Servings: ");
        int servings = int.Parse(Console.ReadLine());
        Console.Write("Ratings: ");
        int ratings = int.Parse(Console.ReadLine());

        // Get ingredients
        List<Ingredient> ingredients = new List<Ingredient>();
        Console.WriteLine("\nEnter Ingredients (press Enter without typing to finish):");
        while (true)
        {
            Console.Write("Ingredient Name (press Enter to finish): ");
            string ingredientName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(ingredientName))
                break;

            Console.Write("Weight/Quantity: ");
            string quantity = Console.ReadLine();
            Console.Write("Price: ");
            double price = double.Parse(Console.ReadLine());

            ingredients.Add(new Ingredient(ingredientName, quantity, price));
        }
        //TEMP artificial tags
        List<string> tags = new List<string> { "vegetarian", "vegan" };
        // Create recipe
        Recipe newRecipe = new Recipe(recipeName, description, cookingTime, preparation, servings, ratings, ingredients, 0, AuthManager.CurrentUser, tags);

        // Add the recipe to the user's list
        AuthManager.CurrentUser.AddRecipe(newRecipe);

        Console.WriteLine("\nRecipe added successfully!");

    }

    public static void DisplayAllRecipes(AuthenticationManager AuthManager)
    {
        Console.WriteLine("All Recipes:\n");
        List<Recipe> allRecipes = AuthManager.GetAllRecipesFromAllUsers();
        foreach (var recipe in allRecipes)
        {
            recipe.DisplayRecipeInfo();
        }
    }
}

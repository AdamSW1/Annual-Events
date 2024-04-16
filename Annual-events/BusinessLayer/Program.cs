using System;
using System.Collections.Generic;
using BusinessLayer;
using RecipeInfo;

class Program
{
    public static AuthenticationManager AuthManager = new AuthenticationManager();

    public static void Main(string[] args)
    {
        // Login
        var loginCredentials = AuthManager.InitLogin();

        if (AuthManager.Login(loginCredentials.Item1, loginCredentials.Item2))
        {
            Console.WriteLine($"Welcome, {AuthManager.CurrentUser.Username}!");

            while (true)
            {
                Init();
            }

        }
        else
        {
            Console.WriteLine("Invalid username or password.");
        }

    }

    
    public static void Init()
    {

        string[] options = new string[] { "Add a recipe", "See your recipes", "LogOut\n" };

        string? choice = GetUserChoice("What do you want to do?", options);



        if (choice == null)
        {
            return;
        }
        else if (choice == options[0])
        {
            AddAnotherRecipe();
        }
        else if (choice == options[1])
        {
            AuthManager.CurrentUser.DisplayRecipes();
        }
        else if (choice == options[2])
        {
            AuthManager.Logout();
            Console.WriteLine("\nLogged out.");
            Console.WriteLine("\nDo you wish to login? yes/no");
            string answer = Console.ReadLine() ?? "null";
            answer = answer.ToLower();
            if (answer == "yes" || answer == "y")
            {
                while (true)
                {
                    var loginCredentials = AuthManager.InitLogin();
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
    public static string? GetUserChoice(string prompt, string[] options)
    {
        Console.WriteLine(prompt);
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i + 1} | {options[i]}");
        }

        string choice = Console.ReadLine() ?? "null";

        if (int.TryParse(choice, out int choice1))
        {
            return options[choice1 - 1];
        }
        else if (options.Contains(choice))
        {
            return choice;
        }
        else if (string.IsNullOrWhiteSpace(choice))
        {
            return null;
        }
        return null;
    }

    public static void AddAnotherRecipe()
    {
        // Prompt user to add recipe
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

        // Create recipe
        Recipe newRecipe = new Recipe(recipeName, description, cookingTime, preparation, servings, ratings, ingredients, 0, AuthManager.CurrentUser);

        // Add the recipe to the user's list
        AuthManager.CurrentUser.AddRecipe(newRecipe);

        Console.WriteLine("\nRecipe added successfully!");

    }
}

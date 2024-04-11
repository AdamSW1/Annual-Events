using System;
using System.Collections.Generic;
using BusinessLayer;
using RecipeInfo;

class Program
{

    public static void Main(string[] args)
    {
        AuthenticationManager AuthManager = new AuthenticationManager();
        // Login
        Console.WriteLine("Login:");
        Console.Write("Username: ");
        string username = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();


        if (AuthManager.Login(username, password))
        {
            Console.WriteLine($"Welcome, {AuthManager.CurrentUser.Username}!");

            while (true){
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

        string[] options = new string[] { "Add a recipe", "See your recipes", "LogOut\n" };

        string? choice = GetUserChoice("What do you want to do?", options);



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
            AuthManager.Logout();
            Console.WriteLine("\nLogged out.");
            System.Environment.Exit(1);
        }


    }
    public static string? GetUserChoice(string prompt, string[] options)
    {
        Console.WriteLine(prompt);
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i + 1} | {options[i]}");
        }

        string choice = Console.ReadLine();

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

        // Create recipe
        Recipe newRecipe = new Recipe(recipeName, description, cookingTime, preparation, servings, ratings, ingredients, 0, AuthManager.CurrentUser);

        // Add the recipe to the user's list
        AuthManager.CurrentUser.AddRecipe(newRecipe);

        Console.WriteLine("\nRecipe added successfully!");

    }
}

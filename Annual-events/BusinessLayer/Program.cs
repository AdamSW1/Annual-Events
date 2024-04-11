using System;
using System.Collections.Generic;
using BusinessLayer;
using RecipeInfo; // Assuming the namespace of the business layer classes

class Program
{
    static void Main(string[] args)
    {
        // Create an instance of the AuthenticationManager
        AuthenticationManager authManager = new AuthenticationManager();

        // Login
        Console.WriteLine("Login:");
        Console.Write("Username: ");
        string username = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();

        if (authManager.Login(username, password))
        {
            Console.WriteLine($"Welcome, {authManager.CurrentUser.Username}!");

            // Display user info
            authManager.CurrentUser.DisplayUserInfo();

            // Add a recipe

            Ingredient tomato = new Ingredient("Tomato", 2, 1.5);
            Ingredient onion = new Ingredient("Onion", 1, 0.75);
            Ingredient garlic = new Ingredient("Garlic", 0.1, 0.2);

            List<Ingredient> ingredients = new List<Ingredient> { tomato, onion, garlic };
            Recipe newRecipe = new Recipe("Spaghetti", "Classic Italian dish", 30, "Cook spaghetti, sauté onions and garlic, mix with tomato sauce", 4, 5, ingredients, 0);
            
            authManager.CurrentUser.AddRecipe(newRecipe);

            // Display recipes
            Console.WriteLine("\nRecipes:");
            authManager.CurrentUser.DisplayRecipes();

            // Logout
            authManager.Logout();
            Console.WriteLine("\nLogged out.");
        }
        else
        {
            Console.WriteLine("Invalid username or password.");
        }
    }
}

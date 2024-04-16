using System;
using System.Collections.Generic;
using BusinessLayer;
using RecipeInfo;

class Program
{
    static void Main(string[] args)
    {
        // Create an instance of the AuthenticationManager
        AuthenticationManager authManager = new AuthenticationManager();
        Utils utils = new Utils();

        // Login
        Console.WriteLine("Login:");
        Console.Write("Username: ");
        string username = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();

        if (authManager.Login(username, password))
        {
            Console.WriteLine($"Welcome, {authManager.CurrentUser.Username}!");

            bool addAnotherRecipe = true;
            while (addAnotherRecipe)
            {
                // Prompt user to add recipe
                Console.WriteLine("\nAdd a Recipe:");
                Console.Write("Recipe Name: ");
                string recipeName = Console.ReadLine();
                Console.Write("Description: ");
                string description = Console.ReadLine();
                Console.Write("Cooking Time (minutes): ");
                //double cookingTime = double.Parse(Console.ReadLine());
                double cookingTime = utils.CheckInput();
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
                    double quantity = double.Parse(Console.ReadLine());
                    Console.Write("Price: ");
                    double price = double.Parse(Console.ReadLine());

                    ingredients.Add(new Ingredient(ingredientName, quantity, price));
                }

                // Create recipe
                Recipe newRecipe = null;// = new Recipe(recipeName, description, cookingTime, preparation, servings, ratings, ingredients, 0, authManager.CurrentUser);

                // Add the recipe to the user's list
                authManager.CurrentUser.AddRecipe(newRecipe);

                Console.WriteLine("\nRecipe added successfully!");

                // Ask if the user wants to add another recipe or log off
                Console.Write("\nDo you want to add another recipe? (yes/no) ");
                string choice = Console.ReadLine();
                if (choice.ToLower() == "no")
                {
                    addAnotherRecipe = false;
                    break; 
                }
            }

            // Display recipes
            Console.WriteLine("\nYour Recipes:");
            authManager.CurrentUser.DisplayRecipes();

            // Logout
            Console.Write("\nDo you want to log off (yes/no)? ");
            string logoutChoice = Console.ReadLine();
            if (logoutChoice.ToLower() == "yes")
            {
                authManager.Logout();
                Console.WriteLine("\nLogged out.");
            }
        }
        else
        {
            Console.WriteLine("Invalid username or password.");
        }
    }

}

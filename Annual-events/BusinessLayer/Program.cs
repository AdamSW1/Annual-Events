using System;
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
            RecipeManager recipeManager = new RecipeManager();
            while (true)
            {
                Init(AuthManager, recipeManager);
            }
        }
        else
        {
            Console.WriteLine("Invalid username or password.");
        }
    }

    public static void Init(AuthenticationManager AuthManager, RecipeManager recipeManager)
    {
        string[] options = new string[] { "Add a recipe", "See your recipes", "See all recipes", "LogOut\n" };

        string? choice = Utils.GetUserChoice("What do you want to do?", options);

        if (choice == null)
        {
            return;
        }
        else if (choice == options[0])
        {
            recipeManager.AddRecipe(AuthManager.CurrentUser);
        }
        else if (choice == options[1])
        {
            AuthManager.CurrentUser.DisplayRecipes();
        }
        else if (choice == options[2])
        {
            AuthManager.GetAllRecipesFromAllUsers().ForEach(recipe => recipe.DisplayRecipeInfo());
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
                Environment.Exit(1);
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
}

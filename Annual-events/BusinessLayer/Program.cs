using System;
using System.Xml.Serialization;
using BusinessLayer;
using RecipeInfo;

class Program
{
    public static AuthenticationManager AuthManager = new AuthenticationManager();
    public static RecipeManager recipeManager = new RecipeManager();
    public static string seperator = "-----------------------";
    public static void Main(string[] args)
    {
        // Login
        var loginCredentials = InitLogin();

        if (AuthManager.Login(loginCredentials.Item1, loginCredentials.Item2))
        {
            Console.WriteLine($"Welcome, {AuthManager.CurrentUser.Username}!");
            AddExampleRecipes();
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
        string[] options = new string[] { "Add a recipe", "See your recipes", "See all recipes", "Search recipes", "LogOut\n" };

        Console.WriteLine();
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
            Console.WriteLine($"\n{seperator}\n");
            AuthManager.CurrentUser.DisplayRecipes();
        }
        else if (choice == options[2])
        {
            AuthManager.GetAllRecipesFromAllUsers().ForEach(recipe => {Console.WriteLine($"\n{seperator}\n"); recipe.DisplayRecipeInfo();});
        }
        else if (choice == options[3])
        {
            Search search = new Search(AuthManager.GetAllRecipesFromAllUsers());

            string[] searchOptions = new string[] { "By keyword" };
            string searchType = Utils.GetUserChoice("How do you want to search?", searchOptions) ?? "";

            if (string.IsNullOrEmpty(searchType))
            {
                return;
            }

            if (searchType == searchOptions[0] || searchType == "1")
            {
                Console.Write("Enter a Keyword: ");
                string keyword = Console.ReadLine() ?? " ";

                Console.WriteLine(seperator);
                List<Recipe> recipes = search.SearchRecipesByKeyword(keyword);
                if (recipes.Count == 0)
                {

                    Console.Write("No recipes found with that keyword");
                    return;
                }

                foreach (Recipe recipe in recipes)
                {
                    Console.WriteLine("");
                    recipe.DisplayRecipeInfo();
                }
            }

        }
        else if (choice == options[4])
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

    /// <summary>
    /// A method that adds an example recipe to the fake database
    /// so recipes viewing can be done without creating one first
    /// 
    /// </summary>
    public static void AddExampleRecipes()
    {
        Ingredient flour = new Ingredient("flour", "6 cups", 7);
        Ingredient egg = new Ingredient("egg", "4", 3);
        List<Ingredient> ingredients = new List<Ingredient>() { flour, egg };
        List<string> tags = new List<string>() { "cake", "chocolate" };
        Recipe exampleRecipe = new Recipe("Chocolate cake",
                                            "A simple chocolate cake",
                                            120,
                                            "mix, put in oven, do stuff",
                                            8,
                                            5,
                                            ingredients,
                                            0,
                                            AuthManager.CurrentUser,
                                            tags
                                            );
        Recipe exampleRecipe2 = new Recipe("Vanilla cake",
                                            "A simple Vanilla cake",
                                            100,
                                            "mix, put in oven, do stuff",
                                            6,
                                            4,
                                            ingredients,
                                            0,
                                            AuthManager.CurrentUser,
                                            tags
                                            );
        
        AuthManager.CurrentUser.AddRecipe(exampleRecipe);
        AuthManager.CurrentUser.AddRecipe(exampleRecipe2);
    }

    public static (string, string) InitLogin()
    {
        Console.WriteLine("-----Login------");
        Console.WriteLine("(for testing: try username: user1 and password: password1)");
        Console.WriteLine(seperator);
        Console.Write("Username: ");
        string username = Console.ReadLine() ?? "null";
        Console.Write("Password: ");
        string password = Console.ReadLine() ?? "null";
        return (username, password);
    }
}

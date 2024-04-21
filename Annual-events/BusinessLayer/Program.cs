using System;
using System.Xml.Serialization;
using BusinessLayer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RecipeInfo;
class Program
{
    
    public static RecipeManager recipeManager = new RecipeManager();
    public static string seperator = "-----------------------";
    public static void Main(string[] args)
    {
        (string, string) loginCredentials = new("null", "null");
        string[] loginOptions = new string[] { "Login", "Create Account" };
        string? choice = Utils.GetUserChoice("Login or create an account", loginOptions);

        if (choice == null)
        {
            System.Environment.Exit(1);
        }

        else if (choice == loginOptions[0])
        {
            // Login
            loginCredentials = InitLogin();
        }

        else if (choice == loginOptions[1])
        {
            loginCredentials = Register();
        }

        if (AuthenticationManager.Instance.Login(loginCredentials.Item1, loginCredentials.Item2))
        {
            Console.WriteLine($"Welcome, {AuthenticationManager.Instance.CurrentUser.Username}!");
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
        string[] options = new string[] 
        {   
            "Add a recipe", 
            "See your recipes", 
            "See your Favourite Recipes", 
            "See all recipes", 
            "Search recipes", 
            "Update a recipe", 
            "Delete a Recipe", 
            "Add a Recipe to Favourites", 
            "Remove a recipe from Favourites List",
            "Add a review to another user's recipe", 
            "View your reviews", 
            "LogOut\n" 
        };

        Console.WriteLine();
        string? choice = Utils.GetUserChoice("What do you want to do?", options);

        if (choice == null)
        {
            return;
        }
        else if (choice == options[0])
        {
            recipeManager.AddRecipe(AuthenticationManager.Instance.CurrentUser);
        }
        else if (choice == options[1])
        {
            Console.WriteLine($"\n{seperator}\n");
            Console.WriteLine(AuthenticationManager.Instance.CurrentUser.DisplayRecipes());
        }
        else if (choice == options[2])
        {
            AuthenticationManager.Instance.CurrentUser.ViewFavRecipes();
        }
        else if (choice == options[3])
        {
            AuthenticationManager.Instance.GetAllRecipesFromAllUsers().ForEach(
                recipe =>
                {
                    Console.WriteLine($"\n{seperator}\n");
                    Console.WriteLine(recipe.DisplayRecipeInfo());
                }
                );
        }
        else if (choice == options[4])
        {
            Search search = new Search(AuthenticationManager.Instance.GetAllRecipesFromAllUsers());

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
                    Console.WriteLine(recipe.DisplayRecipeInfo());
                }
            }

        }
        else if (choice == options[5])
        {
            Console.WriteLine("\nEnter the name of the recipe you want to update/modify:");
            string recipeName = Console.ReadLine();
            RecipeManager.UpdateRecipe(AuthenticationManager.Instance.CurrentUser, recipeName);
        }
        else if (choice == options[6])
        {
            Console.WriteLine("\nEnter the name of the recipe you want to delete:");
            string recipeName = Console.ReadLine();
            RecipeManager.DeleteRecipe(AuthenticationManager.Instance.CurrentUser, recipeName);
        }
        else if (choice == options[7])
        {
            Console.WriteLine("\nEnter the name of your favourite recipe:");
            string recipeName = Console.ReadLine();
            RecipeManager.AddToFavRecipe(AuthenticationManager.Instance.CurrentUser, recipeName);
        }
        else if (choice == options[8])
        {
            Console.WriteLine("\nEnter the name of the recipe (Favourites) you want to delete:");
            string recipeName = Console.ReadLine();
            RecipeManager.DeleteFavRecipe(AuthenticationManager.Instance.CurrentUser, recipeName);
        }
        else if (choice == options[9])
        {
            GiveReviewToAnotherUser();
        }
        else if (choice == options[10])
        {
            ViewReviewsFromUserRecipes();
        }
        else if (choice == options[11])
        {
            AuthenticationManager.Instance.Logout();
            Console.WriteLine("\nLogged out.");
            Console.WriteLine("\nDo you wish to login? yes/no");
            string answer = Console.ReadLine() ?? "null";
            answer = answer.ToLower();
            if (answer == "yes" || answer == "y")
            {
                while (true)
                {
                    var loginCredentials = InitLogin();
                    if (AuthenticationManager.Instance.Login(loginCredentials.Item1, loginCredentials.Item2))
                    {
                        Console.WriteLine($"Welcome, {AuthenticationManager.Instance.CurrentUser.Username}!");
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
        Console.WriteLine("-----Login------");
        Console.WriteLine("(for testing: try username: user1 and password: password1)");
        Console.WriteLine(seperator);
        Console.Write("Username: ");
        string username = Console.ReadLine() ?? "null";
        Console.Write("Password: ");
        string password = Console.ReadLine() ?? "null";
        return (username, password);
    }

    public static (string, string) Register()
    {
        Console.WriteLine("---Register---");
        Console.WriteLine(seperator);

        Console.Write("enter a username: ");
        string username = Console.ReadLine() ?? "null";

        while (username == "null" || string.IsNullOrEmpty(username))
        {
            Console.Write("Enter a valid username: ");
            username = Console.ReadLine() ?? "null";
        }

        Console.Write("Enter a password: ");
        string password = Console.ReadLine() ?? "null";

        while (password == "null" || string.IsNullOrEmpty(password))
        {
            Console.Write("Enter a valid password: ");
            password = Console.ReadLine() ?? "null";
        }

        Console.Write("Enter your age: ");
        string InputAge = Console.ReadLine() ?? "null";
        int age;
        while (InputAge == "null" || !int.TryParse(InputAge, out age))
        {
            Console.Write("Enter a valid age: ");
            InputAge = Console.ReadLine() ?? "null";
        }

        Console.Write("Enter your description (or leave blank): ");
        string description = Console.ReadLine() ?? "";

        User newUser = new(username, password, description, age);
        AuthenticationManager.Instance.AddUser(newUser);

        return (username, password);
    }

    public static void ViewReviewsFromUserRecipes()
    {
        Console.WriteLine("Reviews from your recipes:");

        foreach (var recipe in AuthenticationManager.Instance.CurrentUser.Recipes)
        {
            Console.WriteLine($"Reviews for recipe '{recipe.Name}':");

            if (recipe.Reviews.Count == 0)
            {
                Console.WriteLine("No reviews yet.");
            }
            else
            {
                foreach (var review in recipe.Reviews)
                {
                    Console.WriteLine($"- Review by {review.ReviewerUsername}: {review.ReviewText}");
                }
            }
            
            Console.WriteLine();
        }
    }

    public static void GiveReviewToAnotherUser()
    {
        Console.WriteLine("Select a recipe to review:");
        
        // Retrieve list of all recipes from all users using AuthenticationManager method
        List<Recipe> recipeList = AuthenticationManager.Instance.GetAllRecipesFromAllUsers();

        if (recipeList.Count == 0)
        {
            Console.WriteLine("No recipes found from other users.");
            return;
        }

        foreach (Recipe recipe in recipeList)
        {
            Console.WriteLine($"{recipe.Name} by {recipe.Owner.Username}");
        }

        Console.Write("Enter the name of the recipe you want to review: ");
        string recipeName = Console.ReadLine();

        Recipe selectedRecipe = recipeList.Find(r => r.Name == recipeName);

        if (selectedRecipe != null)
        {
            Console.Write("Enter your review: ");
            string reviewText = Console.ReadLine();

            // Add review to the selected recipe
            selectedRecipe.AddReview(AuthenticationManager.Instance.CurrentUser, reviewText); 

            Console.WriteLine("Review added successfully!");
        }
        else
        {
            Console.WriteLine("Recipe not found.");
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
                                            AuthenticationManager.Instance.CurrentUser,
                                            tags, null
                                            );
        Recipe exampleRecipe2 = new Recipe("Vanilla cake",
                                            "A simple Vanilla cake",
                                            100,
                                            "mix, put in oven, do stuff",
                                            6,
                                            4,
                                            ingredients,
                                            0,
                                            AuthenticationManager.Instance.CurrentUser,
                                            tags, null
                                            );

        AuthenticationManager.Instance.CurrentUser.AddRecipe(exampleRecipe);
        AuthenticationManager.Instance.CurrentUser.AddRecipe(exampleRecipe2);
    }
    
}


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
            //Add a recipe
            CreateAddRecipe(AuthenticationManager.Instance.CurrentUser);
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
            UpadtingRecipe(AuthenticationManager.Instance.CurrentUser);
        }
        else if (choice == options[6])
        {
            Console.WriteLine("\nEnter the name of the recipe you want to delete:");
            DeletingRecipe(AuthenticationManager.Instance.CurrentUser);
        }
        else if (choice == options[7])
        {
            Console.WriteLine("\nEnter the name of your favourite recipe:");
            AddingToFavRecipe(AuthenticationManager.Instance.CurrentUser);
        }
        else if (choice == options[8])
        {
            Console.WriteLine("\nEnter the name of the recipe (Favourites) you want to delete:");
            RemovingFromFavRecipe(AuthenticationManager.Instance.CurrentUser);
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

    private static void DeletingRecipe(User user)
    {
        string recipeName;
        Recipe recipeToDelete;
        FindRecipe(user, out recipeName, out recipeToDelete);
        Console.WriteLine($"\nRecipe '{recipeName}' deleted successfully!");
        RecipeManager.DeleteRecipe(user, recipeToDelete);
    }

    private static void RemovingFromFavRecipe(User user)
    {
        string recipeName;
        Recipe recipeToDelete;
        FindRecipe(user, out recipeName, out recipeToDelete);
        Console.WriteLine($"\nRecipe '{recipeName}' removed from favorites successfully!");
        RecipeManager.DeleteFavRecipe(user, recipeToDelete);
    }

    private static void AddingToFavRecipe(User user)
    {
        string recipeName;
        Recipe recipeToAdd;
        FindRecipe(user, out recipeName, out recipeToAdd);
        Console.WriteLine($"\nRecipe '{recipeName}' added to favorites successfully!");
        RecipeManager.AddToFavRecipe(user, recipeToAdd);
    }


    private static void FindRecipe(User user, out string recipeName, out Recipe recipeToDelete)
    {
        string localRecipeName = Console.ReadLine();
        recipeToDelete = user.Recipes.Find(r => r.Name == localRecipeName);
        recipeName = localRecipeName;
        if (recipeToDelete == null)
        {
            Console.WriteLine($"\nRecipe '{recipeName}' not found in your recipes.");
            return;
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

    private static void CreateAddRecipe(User user)
    {
        //Get recipeName, descroption, cookingTime, preparation, servings, ratings
        Console.WriteLine("\nAdd a Recipe:");
        Console.Write("Recipe Name: ");
        string recipeName = Utils.CheckName() ?? "null";
        Console.Write("Description: ");
        string description = Utils.CheckName100Limit() ?? "null";
        Console.Write("Cooking Time (minutes): ");
        double cookingTime = Utils.CheckDouble();
        Console.Write("Preparation: ");
        string preparation = Utils.CheckName100Limit() ?? "null";
        Console.Write("Servings: ");
        int servings = Utils.CheckServings();
        Console.Write("Ratings: ");
        double ratings = Utils.CheckRatings();

        // Get ingredients
        List<Ingredient> ingredients = new List<Ingredient>();
        Console.WriteLine("\nEnter Ingredients (press Enter without typing to finish):");
        while (true)
        {
            Console.Write("Ingredient Name (press Enter to finish): ");
            string ingredientName = Utils.CheckName() ?? "null";
            if (string.IsNullOrWhiteSpace(ingredientName))
                break;

            Console.Write("Weight/Quantity: ");
            string quantity = Utils.CheckName100Limit();
            Console.Write("Price: ");
            double price = Utils.CheckDouble();

            ingredients.Add(new Ingredient(ingredientName, quantity, price));
        }

        //Get tags
        Console.WriteLine("Enter tags for the recipe (press Enter without typing to finish):");
        Console.WriteLine("Available tags:");
        foreach (var tag in Enum.GetValues(typeof(RecipeTags)))
        {
            Console.WriteLine($"{tag?.ToString()}");
        }

        List<string> tagList = new List<string>();

        while (true)
        {
            Console.Write("Tag: ");
            string tag = Console.ReadLine() ?? "null";
            if (string.IsNullOrWhiteSpace(tag))
                break;
            else
            {
                if (Enum.TryParse(tag, out RecipeTags tagEnum))
                {
                    tagList.Add(tagEnum.ToString());
                }
                else
                {
                    Console.WriteLine("Invalid tag. Please try again.");
                }
            }
        }

        recipeManager.AddRecipe(user, recipeName, description, cookingTime, preparation, servings, ratings, ingredients, tagList);
        Console.WriteLine("\nRecipe added successfully!");
    }
    
    private static void UpadtingRecipe(User user)
    {
        Console.WriteLine("\nEnter the name of the recipe you want to update/modify:");
        string recipeName;
        Recipe recipeToUpdate;
        FindRecipe(user, out recipeName, out recipeToUpdate);
        Console.WriteLine($"Updating recipe '{recipeName}'...");
        Console.Write("New Recipe Name: ");
        string newName = Utils.CheckName();

        Console.Write("New Description: ");
        string newDescription = Utils.CheckName100Limit();

        Console.Write("New Cooking Time (minutes): ");
        double newCookingTime = Utils.CheckDouble();

        Console.Write("New Preparations: ");
        string newPreparation = Utils.CheckName100Limit();

        Console.Write("New Servings: ");
        int newServings = Utils.CheckServings();  
        
        Console.Write("New Ratings: ");
        double newRatings = Utils.CheckRatings();

        recipeManager.UpdateRecipe(newName, newDescription, newCookingTime, newPreparation, newServings, newRatings, recipeToUpdate);

        Console.WriteLine($"\nRecipe '{recipeName}' updated successfully!");              
    }
}


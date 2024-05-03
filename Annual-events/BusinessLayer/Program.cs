using System;
using System.ComponentModel;
using System.Runtime.Intrinsics.Arm;

using System.Xml.Serialization;
using BusinessLayer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RecipeInfo;
using DataLayer;
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
            AddRecipe(AuthenticationManager.Instance.CurrentUser);
        }
        else if (choice == options[1])
        {
            Console.WriteLine($"\n{seperator}\n");
            Console.WriteLine(AuthenticationManager.Instance.CurrentUser.DisplayRecipes());
        }
        else if (choice == options[2])
        {
            Console.WriteLine(AuthenticationManager.Instance.CurrentUser.ViewFavRecipes());
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
                List<Recipe> recipes = Search.SearchRecipesByKeyword(keyword, AuthenticationManager.Instance.GetAllRecipesFromAllUsers());
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
            Console.WriteLine("Your recipes: ");
            AuthenticationManager.Instance.CurrentUser.Recipes.ForEach(recipe => Console.WriteLine($"{recipe.Name}"));
            Console.WriteLine("\nEnter the name of the recipe you want to update/modify:");
            UpdatingRecipe();
        }
        else if (choice == options[6])
        {
            Console.WriteLine("Your recipes: ");
            AuthenticationManager.Instance.CurrentUser.Recipes.ForEach(recipe => Console.WriteLine($"{recipe.Name}"));
            Console.WriteLine("\nEnter the name of the recipe you want to delete:");
            DeletingRecipe();
        }
        else if (choice == options[7])
        {
            Console.WriteLine("\nEnter the name of your favourite recipe:");
            AddingToFavRecipe();
        }
        else if (choice == options[8])
        {
            Console.WriteLine(AuthenticationManager.Instance.CurrentUser.ViewFavRecipes());
            Console.WriteLine("\nEnter the name of the recipe (Favourites) you want to delete:");
            RemovingFromFavRecipe();
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

        Annual_Events_User newUser = new(username, password, description, age);
        AuthenticationManager.Instance.AddUser(newUser);

        return (username, password);
    }
    private static void AddRecipe(Annual_Events_User user)
    {
        //Get recipeName, descroption, cookingTime, preparation, servings, ratings
        Console.WriteLine("\nAdd a Recipe:");
        Console.Write("Recipe Name: ");
        string recipeName = GetName();
        Console.Write("Description: ");
        string description = GetLongString();
        Console.Write("Cooking Time (minutes): ");
        double cookingTime = GetDouble();
        Console.Write("Preparation: ");
        List<Preparation> preparation = GetRecipePreparation();
        Console.Write("Servings: ");
        int servings = GetInt();

        // Get ingredients
        List<Ingredient> ingredients = new();
        List<RecipeIngredient> recipeIngredients = new();
        string quantity = "";
        Console.WriteLine("\nEnter Ingredients (press Enter without typing to finish):");
        while (true)
        {
            Console.Write("Ingredient Name (press Enter to finish): ");
            //no call to getName function since entering nothing exits the loop 
            string ingredientName = Console.ReadLine() ?? " ";
            if (string.IsNullOrWhiteSpace(ingredientName))
                break;

            Console.Write("Weight/Quantity: ");
            quantity = GetString();
            if (int.TryParse(quantity, out int result) && result < 0)
            {
                quantity = GetInt(true).ToString();
            }
            Console.Write("Price: ");
            double price = GetDouble();


            recipeIngredients.Add(new RecipeIngredient { Ingredient = new Ingredient(ingredientName, price), Quantity = quantity });
        }


        //Get tags
        Console.WriteLine("Enter tags for the recipe (press Enter without typing to finish):");
        Console.WriteLine("Available tags:");
        foreach (var tag in Enum.GetValues(typeof(RecipeTags)))
        {
            Console.WriteLine($"{tag?.ToString()}");
        }

        List<RecipeTag> tagList = new();

        while (true)
        {
            Console.Write("Tag: ");
            string tag = Console.ReadLine() ?? " ";
            if (string.IsNullOrWhiteSpace(tag))
                break;
            else
            {
                if (Enum.TryParse(tag, out RecipeTags tagEnum))
                {
                    RecipeTag tagObj = new(tagEnum.ToString());
                    tagList.Add(tagObj);
                }
                else
                {
                    Console.WriteLine("Invalid tag. Please try again.");
                }
            }
        }

        Recipe newRecipe = new(recipeName, description, cookingTime, preparation, servings, recipeIngredients, 0, user, tagList, new List<Review>());
        RecipeManager.AddRecipe(newRecipe);
        Console.WriteLine("\nRecipe added successfully!");
    }

    private static void UpdatingRecipe()
    {
        if (!FindRecipe(AuthenticationManager.Instance.CurrentUser, out string recipeName, out Recipe recipeToUpdate))
        {
            return;
        }

        Console.WriteLine($"Updating recipe '{recipeName}'...");
        Console.Write("New Recipe Name: ");
        recipeToUpdate.Name = GetName();

        Console.Write("New Description: ");
        recipeToUpdate.Description = GetLongString();

        Console.Write("New Cooking Time (minutes): ");
        recipeToUpdate.CookingTime = GetDouble();

        Console.Write("New Preparations: ");
        recipeToUpdate.Preparation = GetRecipePreparation();

        Console.Write("New Servings: ");
        recipeToUpdate.Servings = GetInt();

        RecipeServices.Instance.DbContext.SaveChanges();
        Console.WriteLine($"\nRecipe '{recipeName}' updated successfully!");
    }

    private static void DeletingRecipe()
    {
        Annual_Events_User user = AuthenticationManager.Instance.CurrentUser;
        if (!FindRecipe(user, out string recipeName, out Recipe recipeToDelete))
        {
            return;
        }

        Console.WriteLine($"\nRecipe '{recipeName}' deleted successfully!");
        RecipeManager.DeleteRecipe(user, recipeToDelete);
    }

    private static void RemovingFromFavRecipe()
    {
        Annual_Events_User user = AuthenticationManager.Instance.CurrentUser;
        if (!FindRecipe(user, out string recipeName, out Recipe recipeToDelete))
        {
            return;
        }
        recipeToDelete.RemoveFavourite();
        Console.WriteLine($"\nRecipe '{recipeName}' removed from favorites successfully!");
        RecipeManager.DeleteFavRecipe(user, recipeToDelete);
    }

    private static void AddingToFavRecipe()
    {
        Annual_Events_User user = AuthenticationManager.Instance.CurrentUser;
        if (!FindRecipe(user, out string recipeName, out Recipe recipeToAdd))
        {
            return;
        }
        recipeToAdd.AddFavourite();
        Console.WriteLine($"\nRecipe '{recipeName}' added to favorites successfully!");
        RecipeManager.AddToFavRecipe(user, recipeToAdd);
    }


    private static bool FindRecipe(Annual_Events_User user, out string recipeName, out Recipe recipeToDelete)
    {
        string localRecipeName = GetName();
        recipeToDelete = GetRecipeByName(user.Recipes, localRecipeName)!;
        recipeName = localRecipeName;
        if (recipeToDelete == null)
        {
            Console.WriteLine($"\nRecipe '{recipeName}' not found in your recipes.");
            return false;
        }
        return true;
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
                    Console.WriteLine(review);
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

        recipeList.ForEach(recipe => Console.WriteLine($"{recipe.Name} by {recipe.Owner.Username}"));

        Console.Write("Enter the name of the recipe you want to review: ");
        string recipeName = GetName();

        Recipe selectedRecipe = GetRecipeByName(recipeList, recipeName)!;

        if (selectedRecipe != null)
        {
            Console.Write("Enter your review score");
            int reviewScore = GetScore();

            Console.Write("Enter your review: ");
            string reviewText = GetLongString();

            // Add review to the selected recipe
            selectedRecipe.AddReview(AuthenticationManager.Instance.CurrentUser, reviewText, reviewScore);

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
        Ingredient flour = new Ingredient("flour", 7);
        Ingredient egg = new Ingredient("egg", 3);


        List<Ingredient> ingredients = new List<Ingredient>() { flour, egg };
        List<RecipeIngredient> recipeIngredients = ingredients.Select(ingredient => new RecipeIngredient { Ingredient = ingredient, Quantity = "4" }).ToList();

        List<string> tags = new List<string>() { "cake", "chocolate" };
        Recipe exampleRecipe = new Recipe("Chocolate cake",
                                            "A simple chocolate cake",
                                            120,
                                            new List<Preparation>(){
                                                new(1, "bake"),
                                                new(2, "put in oven"),
                                                new(3, "do stuff")

                                            },
                                            8,
                                            recipeIngredients,
                                            0,
                                            AuthenticationManager.Instance.CurrentUser,
                                            new List<RecipeTag>() { new RecipeTag("vegan") }
                                            , new List<Review>()
                                            );
        Recipe exampleRecipe2 = new Recipe("Vanilla cake",
                                            "A simple Vanilla cake",
                                            100,
                                            new List<Preparation>(){
                                                new(1, "bake"),
                                                new(2, "put in oven"),
                                                new(3, "do stuff")

                                            },
                                            6,
                                            recipeIngredients,
                                            0,
                                            AuthenticationManager.Instance.CurrentUser,
                                            new List<RecipeTag>() { new RecipeTag("vegan") }
                                            , new List<Review>()
                                            );
        exampleRecipe.AverageScore = 3;
        exampleRecipe2.AverageScore = 5;

        // AuthenticationManager.Instance.CurrentUser.AddRecipe(exampleRecipe);
        // AuthenticationManager.Instance.CurrentUser.AddRecipe(exampleRecipe2);
        RecipeManager.AddRecipe(exampleRecipe);
        RecipeManager.AddRecipe(exampleRecipe2);
        AnnualEventsContext.Instance.SaveChanges();
        // AnnualEventsService.Instance.AddRecipe(exampleRecipe);

    }

    public static string GetName()
    {
        bool validInput = false;
        string name = "";
        do
        {
            name = Console.ReadLine() ?? " ";

            if (!Utils.CheckName(name))
            {
                validInput = false;
                Console.WriteLine("Invalid name. Cannot contain special characters and length of name must be 30 maximum!");
                Console.WriteLine();
            }
            else
            {
                validInput = true;
            }
        } while (validInput != true);

        return name;
    }

    public static string GetLongString()
    {
        string val = "";
        bool validInput = false;

        do
        {
            val = Console.ReadLine() ?? " ";
            if (!Utils.CheckLongString(val))
            {
                validInput = false;
                Console.WriteLine("Invalid description. Cannot be longer than 2000 characters");
                Console.WriteLine();
            }
            else
            {
                validInput = true;
            }

        } while (validInput != true);

        return val;
    }
    public static List<Preparation> GetRecipePreparation()
    {
        bool validInput;
        List<Preparation> preparation = new();
        do
        {
            string prepInput = Console.ReadLine() ?? "";

            if (!Utils.CheckMultiStringInput(prepInput))
            {
                validInput = false;
                Console.WriteLine("Preparation must be a comma seperated list");
                Console.WriteLine();

            }
            else
            {
                List<string> prepString = prepInput.Split(",").ToList();
                int stepnum = 1;
                prepString.ForEach(prep =>
                {
                    if (string.IsNullOrEmpty(prep))
                    {
                        return;
                    }
                    stepnum++;
                    preparation.Add(new Preparation(stepnum, prep));
                });
                validInput = true;

            }

        } while (validInput != true);

        return preparation;
    }

    public static int GetInt(bool CallerGotNum = false)
    {
        bool validInput = false;
        int val;
        do
        {
            if (CallerGotNum)
            {
                Console.WriteLine("Input must be a whole non negative number");
            }
            if (!int.TryParse(Console.ReadLine(), out val) || !Utils.CheckInt(val))
            {
                validInput = false;
                Console.WriteLine("Input must be a whole non negative number");
                Console.WriteLine();
            }
            else
            {
                validInput = true;
            }

        } while (validInput != true);

        return val;
    }

    public static int GetScore()
    {
        bool validInput;
        int val;
        do
        {
            if (!int.TryParse(Console.ReadLine(), out val) || !Utils.CheckScore(val))
            {
                validInput = false;
                Console.WriteLine("score must be a whole non negative number between 0-5");
                Console.WriteLine();
            }
            else
            {
                validInput = true;
            }

        } while (validInput != true);

        return val;
    }


    public static double GetDouble()
    {
        bool validInput;
        double input;
        do
        {
            if (!double.TryParse(Console.ReadLine(), out input) || !Utils.CheckDouble(input))
            {
                validInput = false;
                Console.WriteLine("Input must be a number greater than 0");
                Console.WriteLine();
            }
            else
            {
                validInput = true;
            }
        } while (validInput != true);

        return input;
    }

    public static string GetString()
    {
        bool validInput = false;
        string returnVal = "";

        do
        {
            returnVal = Console.ReadLine() ?? "";

            if (!Utils.CheckString(returnVal))
            {
                validInput = false;
                Console.WriteLine("Invalid input");
                Console.WriteLine();
            }
            else
            {
                validInput = true;
            }

        } while (validInput != true);

        return returnVal;
    }

    public static Recipe? GetRecipeByName(List<Recipe> recipes, string name)
    {
        if (Utils.CheckRecipeInList(recipes, name))
        {
            return recipes.Find(r =>
            {
                string min = r.Name.ToLower().Trim();
                return min == name.ToLower().Trim();
            }) ?? throw new ArgumentException("not found");
        }
        return null;
    }
}


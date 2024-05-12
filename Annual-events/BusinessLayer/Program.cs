using System;
using System.ComponentModel;
using System.Runtime.Intrinsics.Arm;

using System.Xml.Serialization;
using BusinessLayer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RecipeInfo;
using DataLayer;
using System.Runtime.CompilerServices;
class Program
{

    public static RecipeManager recipeManager = new RecipeManager();
    public static string seperator = "-----------------------";
    public static void Main(string[] args)
    {
        Setup();
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
                Init(AuthenticationManager.Instance.CurrentUser);
            }
        }
        else
        {
            Console.WriteLine("Invalid username or password.");
        }
    }

    private static void Setup()
    {

        if (AnnualEventsUserServices.Instance.GetUserByUsername("user1") is not null || AnnualEventsUserServices.Instance.GetUserByUsername("user1") is not null)
        {
            return;
        }
        AuthenticationManager.Instance.AddUser(new Annual_Events_User("user1", "password1", "Description 1", 25));
        AuthenticationManager.Instance.AddUser(new Annual_Events_User("user2", "password2", "Description 2", 30));
    }
    public static void Init(Annual_Events_User user)
    {
        string[] options = new string[]
        {
            "Update your profile",
            "Delete your account",
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
            Console.WriteLine("Enter your new username:");
            string newUsername = Console.ReadLine() ?? "";
            Console.WriteLine("Enter your new password:");
            string newPassword = Console.ReadLine() ?? "";
            Console.WriteLine("Enter your new description:");
            string newDescription = Console.ReadLine() ?? "";
            Console.WriteLine("Enter your new age:");
            int newAge = int.Parse(Console.ReadLine() ?? "");
            Profile userProfile = new Profile();
            userProfile.UpdateProfile(user, newUsername, newPassword, newDescription, newAge);
        }
        else if (choice == options[1])
        {
            Console.WriteLine("Are you sure you want to delete your account? (yes/no)");
            string confirmDelete = Console.ReadLine()?.ToLower() ?? "";

            if (confirmDelete == "yes")
            {
                AnnualEventsUserServices.Instance.DeleteUser(AuthenticationManager.Instance.CurrentUser);
                Console.WriteLine("Your account has been deleted successfully.");
                Environment.Exit(0); // Exit the program after deleting the user
            }
            else
            {
                Console.WriteLine("Account deletion canceled.");
            }
        }
        else if (choice == options[2])
        {
            //Add a recipe
            AddRecipe();
        }
        else if (choice == options[3])
        {
            Console.WriteLine($"\n{seperator}\n");
            Console.WriteLine(AuthenticationManager.Instance.CurrentUser.DisplayRecipes());
        }
        else if (choice == options[4])
        {
            Console.WriteLine(AuthenticationManager.Instance.CurrentUser.ViewFavRecipes());
        }
        else if (choice == options[5])
        {
            RecipeServices.Instance.GetRecipes().ForEach(
                recipe =>
                {
                    Console.WriteLine($"\n{seperator}\n");
                    Console.WriteLine(recipe.DisplayRecipeInfo());
                }
                );
        }
        else if (choice == options[6])
        {
            string[] searchOptions = new string[] { "By keyword", "By tags", "By time","By rating", "By servings", "By favourite count", "By owner" };
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
                List<Recipe> recipes = Search.SearchRecipesByKeyword(keyword, RecipeServices.Instance.GetRecipes());
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
            else if (searchType == searchOptions[1] || searchType == "2")
            {
                Console.WriteLine("Enter tags for the recipe (Type with commas):");
                bool validInput;
                List<RecipeTag> tags = new();
                do
                {
                    string tag_list = Console.ReadLine() ?? "";
                    if (!Utils.CheckMultiStringInput(tag_list))
                    {
                        validInput = false;
                        Console.WriteLine("Tags must be a comma seperated list");
                        Console.WriteLine();

                    }
                    else
                    {
                        List<string> prepString = tag_list.Split(",").ToList();
                        int stepnum = 1;
                        prepString.ForEach(prep =>
                        {
                            if (string.IsNullOrEmpty(prep))
                            {
                                return;
                            }
                            stepnum++;
                            tags.Add(new RecipeTag());
                            tags[stepnum - 2].Tag = prep;
                        });
                        validInput = true;

                    }

                } while (validInput != true);                
                Console.WriteLine(seperator);
                List<Recipe>? recipes = Search.SearchRecipesByTags(tags, RecipeServices.Instance.GetRecipes());
                if (recipes is null || recipes.Count == 0)
                {
                    Console.Write("No recipes found with that tag");
                    return;
                }

                foreach (Recipe recipe in recipes)
                {
                    Console.WriteLine("");
                    Console.WriteLine(recipe.DisplayRecipeInfo());
                }
            }
            else if (searchType == searchOptions[2] || searchType == "3")
            {
                Console.Write("Enter a time: ");
                int time = int.Parse(Console.ReadLine() ?? " ");

                Console.WriteLine(seperator);
                List<Recipe> recipes = Search.SearchRecipesByTimeConstraint(time, RecipeServices.Instance.GetRecipes());
                if (recipes.Count == 0)
                {
                    Console.Write("No recipes found with that time");
                    return;
                }

                foreach (Recipe recipe in recipes)
                {
                    Console.WriteLine("");
                    Console.WriteLine(recipe.DisplayRecipeInfo());
                }
            }
            else if (searchType == searchOptions[3] || searchType == "4")
            {
                Console.Write("Enter a rating: ");
                int rating = int.Parse(Console.ReadLine() ?? " ");

                Console.WriteLine(seperator);
                List<Recipe> recipes = Search.SearchRecipesByRating(rating, RecipeServices.Instance.GetRecipes());
                if (recipes.Count == 0)
                {
                    Console.Write("No recipes found with that rating");
                    return;
                }

                foreach (Recipe recipe in recipes)
                {
                    Console.WriteLine("");
                    Console.WriteLine(recipe.DisplayRecipeInfo());
                }
            }
            else if (searchType == searchOptions[4] || searchType == "5")
            {
                Console.Write("Enter a servings: ");
                int servings = int.Parse(Console.ReadLine() ?? " ");

                Console.WriteLine(seperator);
                List<Recipe> recipes = Search.SearchRecipesByServings(servings, RecipeServices.Instance.GetRecipes());
                if (recipes.Count == 0)
                {
                    Console.Write("No recipes found with that servings");
                    return;
                }

                foreach (Recipe recipe in recipes)
                {
                    Console.WriteLine("");
                    Console.WriteLine(recipe.DisplayRecipeInfo());
                }
            }
            else if (searchType == searchOptions[5] || searchType == "6")
            {
                Console.Write("Enter a favourite: ");
                int favourite = int.Parse(Console.ReadLine() ?? " ");

                Console.WriteLine(seperator);
                List<Recipe> recipes = Search.SearchRecipesInFavorites(favourite, RecipeServices.Instance.GetRecipes());
                if (recipes.Count == 0)
                {
                    Console.Write("No recipes found with that favourite");
                    return;
                }

                foreach (Recipe recipe in recipes)
                {
                    Console.WriteLine("");
                    Console.WriteLine(recipe.DisplayRecipeInfo());
                }
            }
            else if (searchType == searchOptions[6] || searchType == "7")
            {
                Console.Write("Enter a owner username: ");
                string ownerUsername = Console.ReadLine() ?? " ";

                Console.WriteLine(seperator);
                List<Recipe> recipes = Search.SearchRecipesByOwnerUsername(ownerUsername, RecipeServices.Instance.GetRecipes());
                if (recipes.Count == 0)
                {
                    Console.Write("No recipes found with that owner username");
                    return;
                }
                foreach (Recipe recipe in recipes)
                {
                    Console.WriteLine("");
                    Console.WriteLine(recipe.DisplayRecipeInfo());
                }
            }
            else
            {
                Console.WriteLine("Invalid choice");
            }

        }
        else if (choice == options[7])
        {
            Console.WriteLine("Your recipes: ");
            AuthenticationManager.Instance.CurrentUser.Recipes.ForEach(recipe => Console.WriteLine($"{recipe.Name}"));
            Console.WriteLine("\nEnter the name of the recipe you want to update/modify:");
            UpdatingRecipe();
        }
        else if (choice == options[8])
        {
            Console.WriteLine("Your recipes: ");
            AuthenticationManager.Instance.CurrentUser.Recipes.ForEach(recipe => Console.WriteLine($"{recipe.Name}"));
            Console.WriteLine("\nEnter the name of the recipe you want to delete:");
            DeletingRecipe();
        }
        else if (choice == options[9])
        {
            Console.WriteLine("\nEnter the name of your favourite recipe:");
            AddingToFavRecipe();
        }
        else if (choice == options[10])
        {
            Console.WriteLine(AuthenticationManager.Instance.CurrentUser.ViewFavRecipes());
            Console.WriteLine("\nEnter the name of the recipe (Favourites) you want to delete:");
            RemovingFromFavRecipe();
        }
        else if (choice == options[11])
        {
            GiveReviewToAnotherUser();
        }
        else if (choice == options[12])
        {
            ViewReviewsFromUserRecipes();
        }
        else if (choice == options[13])
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

        Annual_Events_User checkUserExists = AnnualEventsUserServices.Instance.GetUserByUsername(username);
        while (username == "null" || string.IsNullOrEmpty(username) || checkUserExists != null)
        {
            if (checkUserExists != null)
            {
                Console.Write("User with that name already exists");
            }
            else
            {
                Console.Write("Enter a valid username: ");
            }
            username = Console.ReadLine() ?? "null";
            checkUserExists = AnnualEventsUserServices.Instance.GetUserByUsername(username);
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

        if (string.IsNullOrEmpty(description))
        {
            description = " ";
        }

        Annual_Events_User newUser = new(username, password, description, age);
        AuthenticationManager.Instance.AddUser(newUser);

        return (username, password);
    }

    private static void AddRecipe()
    {
        //Get recipeName, descroption, cookingTime, preparation, servings, ratings
        Annual_Events_User user = AuthenticationManager.Instance.CurrentUser;
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
        Console.WriteLine($"\nRecipe {recipeName} added successfully!");
    }

    private static void UpdatingRecipe()
    {
        if (!FindCurrentUserRecipe(AuthenticationManager.Instance.CurrentUser, out string recipeName, out Recipe recipeToUpdate))
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

        AnnualEventsContext.Instance.SaveChanges();
        Console.WriteLine($"\nRecipe '{recipeName}' updated successfully!");
    }

    private static void DeletingRecipe()
    {
        Annual_Events_User user = AuthenticationManager.Instance.CurrentUser;
        if (!FindCurrentUserRecipe(user, out string recipeName, out Recipe recipeToDelete))
        {
            return;
        }

        Console.WriteLine($"\nRecipe '{recipeName}' deleted successfully!");
        RecipeManager.DeleteRecipe(user, recipeToDelete);
    }

    private static void RemovingFromFavRecipe()
    {
        Annual_Events_User user = AuthenticationManager.Instance.CurrentUser;
        if (!FindRecipe(out string recipeName, out Recipe? recipeToDelete))
        {
            return;
        }
        recipeToDelete!.RemoveFavourite();
        Console.WriteLine($"\nRecipe '{recipeName}' removed from favorites successfully!");
        RecipeManager.DeleteFavRecipe(user, recipeToDelete);
    }


    private static void AddingToFavRecipe()
    {
        Annual_Events_User userFrom = AuthenticationManager.Instance.CurrentUser;
        if (!FindRecipe(out string recipeName, out Recipe? recipeToAdd))
        {
            return;
        }
        recipeToAdd!.AddFavourite();
        Console.WriteLine($"\nRecipe '{recipeName}' added to favorites successfully!");
        RecipeManager.AddToFavRecipe(userFrom, recipeToAdd);
        RecipeServices.Instance.DbContext.SaveChanges();
    }


    private static bool FindCurrentUserRecipe(Annual_Events_User user, out string recipeName, out Recipe recipeToFind)
    {
        string localRecipeName = GetName();
        recipeToFind = GetRecipeByName(user.Recipes, localRecipeName)!;
        recipeName = localRecipeName;
        if (recipeToFind == null)
        {
            Console.WriteLine($"\nRecipe '{recipeName}' not found in your recipes.");
            return false;
        }
        return true;
    }

    private static bool FindRecipe(out string recipeName, out Recipe? recipeToFind)
    {
        string name = GetName();
        recipeToFind = RecipeServices.Instance.GetRecipe(name);
        recipeName = name;
        if (recipeToFind == null)
        {
            Console.WriteLine($"\nRecipe '{name}' not found in your recipes.");
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

            if (AnnualEventsService.Instance.GetReviewsForRecipe(recipe).Count == 0)
            {
                Console.WriteLine("No reviews yet.");
            }
            else
            {
                foreach (var review in AnnualEventsService.Instance.GetReviewsForRecipe(recipe))
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
        List<Recipe> recipeList = RecipeServices.Instance.GetRecipes();

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

    public static void UpdateReview(Review reviewToUpdate)
    {
        Console.WriteLine($"Updating review by {reviewToUpdate.ReviewerUsername} ");
        Console.Write("New Review Text: ");
        reviewToUpdate.ReviewText = GetLongString();

        Console.Write("New Score: ");
        reviewToUpdate.Score = GetScore();

        Console.Write("New reviewer: ");
        reviewToUpdate.ReviewerUsername = GetString();

        RecipeServices.Instance.DbContext.SaveChanges();
        Console.WriteLine($"Review updated successfully!");
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
        List<RecipeIngredient> recipeIngredients2 = ingredients.Select(ingredient => new RecipeIngredient { Ingredient = ingredient, Quantity = "4" }).ToList();

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
                                            recipeIngredients2,
                                            0,
                                            AuthenticationManager.Instance.CurrentUser,
                                            new List<RecipeTag>() { new RecipeTag("vegan") }
                                            , new List<Review>()
                                            );

        exampleRecipe.AverageScore = 3;
        exampleRecipe2.AverageScore = 5;

        //check if the recipes already exist
        Recipe? checkRecipe1Exists = RecipeServices.Instance.GetRecipe(exampleRecipe.Name);
        Recipe? checkRecipe2Exists = RecipeServices.Instance.GetRecipe(exampleRecipe2.Name);
        if (checkRecipe1Exists is null)
        {
            RecipeManager.AddRecipe(exampleRecipe);
            AnnualEventsContext.Instance.SaveChanges();
        }
        if (checkRecipe2Exists is null)
        {
            RecipeManager.AddRecipe(exampleRecipe2);
            AnnualEventsContext.Instance.SaveChanges();
        }

        return;
        
        // RecipeManager.AddRecipe(exampleRecipe);
        // RecipeManager.AddRecipe(exampleRecipe2);
        // AnnualEventsContext.Instance.SaveChanges();
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
                    preparation.Add(new Preparation(stepnum, prep));
                    stepnum++;
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


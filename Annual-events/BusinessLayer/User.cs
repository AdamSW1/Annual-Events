using DataLayer;
using RecipeInfo;
namespace BusinessLayer;
public class Annual_Events_User
{

    public int Annual_Events_UserId { get; set; }
    private List<Recipe> _recipes = new List<Recipe>();
    public List<Recipe> Recipes
    {
        get { return _recipes; }
        set { _recipes = value; }
    }
    private List<Recipe>? _favRecipes = new List<Recipe>();
    public List<Recipe> FavRecipes
    {
        get
        {
            if (_favRecipes == null || _favRecipes.Count == 0)
            {
                _favRecipes = RecipeServices.Instance.GetRecipesFavByUser(this);
            }
            return _favRecipes;
        }
        set { _favRecipes = value; }
    }

    private string? _username;
    public string? Username
    {
        get
        {
            return _username;
        }
        set
        {
            if (value != null && !Utils.CheckName(value))
            {
                throw new ArgumentException("Invalid name");
            }
            _username = value;
        }
    }
    private string _password;
    public string Password
    {
        get
        {
            return _password;
        }
        set
        {
            _password = value;
        }
    }
    private string? _description;
    public string? Description
    {
        get
        {
            return _description;
        }
        set
        {
            _description = value;
        }
    }
    private int _age;
    public int Age
    {
        get
        {
            return _age;
        }
        set
        {
            if (Utils.CheckInt(value) == false)
            {
                throw new ArgumentException("invalid age");
            }
            _age = value;
        }
    }
    private byte[]? _profilePicture; // (GUI) store images in database??
    public byte[]? ProfilePicture
    {
        get
        {
            return _profilePicture;
        }
        set
        {
            _profilePicture = value;
        }
    }


    // constructor 
    public Annual_Events_User(string username, string password, string description, int age, byte[] profilePicture)
    {
        _username = username;
        _password = password;
        _description = description;
        _age = age;
        _profilePicture = profilePicture;
    }

    public Annual_Events_User(){}


    // override object.Equals
    public override bool Equals(Object? obj)
    {

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Annual_Events_User user = (Annual_Events_User)obj;
        return _username == user._username && _description == user._description && _age == user._age;
    }

    // override object.GetHashCode
    public override int GetHashCode()
    {
        return HashCode.Combine(_username, _description, _age);
    }

    public string DisplayUserInfo()
    {
        string returnStr = "";
        returnStr += $"Username: {_username} \n";
        returnStr += $"Description: {_description}\n";
        returnStr += $"Age: {_age}\n";

        return returnStr;
    }

    public void AddToFavRecipe(Recipe recipe)
    {
        FavRecipes.Add(recipe);
    } // lets them add a recipe to an [] list

    public void AddRecipe(Recipe recipe)
    {
        _recipes.Add(recipe);
    }
    public void RemoveFromFavRecipe(Recipe recipe)
    {
        FavRecipes.Remove(recipe);
    }

    public void DeleteRecipe(Recipe recipe)
    {
        _recipes.Remove(recipe);
    }
    public string DisplayRecipes()
    {
        string returnStr = "";
        returnStr += $"Recipes for user: {_username}\n";

        foreach (var recipe in Recipes)
        {
            returnStr += "\n";
            returnStr += recipe.DisplayRecipeInfo();
        }

        return returnStr;
    }

    public bool Authentication(string enteredUsername, string enteredPassword)
    {
        return Username == enteredUsername && Password == enteredPassword;
    } // verifies their passwords and usernames with database


    // should delete their account definitely
    public void DeleteAccount(string authenticatedUsername)
    {
        if (authenticatedUsername != Username)
        {
            throw new UnauthorizedAccessException("Cant delete another user");
        }
    }

    public string ViewFavRecipes()
    {
        string returnStr = "";
        returnStr += "Favorite Recipes:\n";
        foreach (var recipe in FavRecipes)
        {
            returnStr += $"{recipe.Name} by {recipe.Owner.Username}\n"; // Assuming Name property exists in Recipe class
        }
        return returnStr;
    }

    // Database implementation of UpdateRecipe, does both functionalities
    internal bool UpdateRecipe(string name, string updatedRecipeName, string updatedDescription, double updatedCookingTime, List<Preparation> updatedPreparation, int updatedServings, int updatedRatings)
    {
        Recipe recipeToUpdate = Recipes.FirstOrDefault(r => r.Name == name)!;
        if (recipeToUpdate != null)
        {
            // Update the recipe's properties with the provided values
            recipeToUpdate.Name = updatedRecipeName;
            recipeToUpdate.Description = updatedDescription;
            recipeToUpdate.CookingTime = updatedCookingTime;
            recipeToUpdate.Preparation = updatedPreparation;
            recipeToUpdate.Servings = updatedServings;
            recipeToUpdate.AverageScore = updatedRatings;
            return true;
        }
        else
        {
            // Recipe with the specified name not found or user not found
            return false;
        }

    }

}


using System.Collections.Generic;
using RecipeInfo;
namespace BusinessLayer;
public class User
{

    private string _username;
    public string Username
    {
        get
        {
            return _username;
        }
        set
        {
            if(!Utils.CheckName(value)){
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
    private string _description;
    public string Description
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
            if (Utils.CheckInt(value) == false){
                throw new ArgumentException("invalid age");
            }
            _age = value;
        }
    }
    private byte[]? profile_picture; // (GUI) store images in database??

    private List<Recipe> _recipes = new List<Recipe>();
    public List<Recipe> Recipes
    {
        get { return _recipes; }
    }

    private List<Recipe> _favRecipes = new List<Recipe>();
    public List<Recipe> FavRecipes
    {
        get { return _favRecipes; }
    }

    // constructor 
    public User(string username, string password, string description, int age)
    {
        _username = username;
        _password = password;
        _description = description;
        _age = age;
        hashPassword(password);
    }

    // override object.Equals
    public override bool Equals(Object? obj)
    {

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        User user = (User)obj;
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
        _favRecipes.Add(recipe);
    } // lets them add a recipe to an [] list

    public void AddRecipe(Recipe recipe)
    {
        recipe.Owner = this;
        _recipes.Add(recipe);
    } // just adding a recipe to a list, other than Fav

    public void RemoveFromFavRecipe(Recipe recipe)
    {
        _favRecipes.Remove(recipe);
    } // removes a recipe from the [] list

    public void DeleteRecipe(Recipe recipe)
    {
        _recipes.Remove(recipe);
    }
    public string DisplayRecipes()
    {
        string returnStr = "";
        returnStr += $"Recipes for user: {_username}\n";

        foreach (var recipe in _recipes)
        {
            returnStr += "\n";
            returnStr += recipe.DisplayRecipeInfo();
        }

        return returnStr;
    }

    public void StepVerification()
    {
    } // optional but asks them 2 specific infos

    public void hashPassword(string password)
    {
        // algo here
    } // hashes their password

    public bool Authentication(string enteredUsername, string enteredPassword)
    {
        return Username == enteredUsername && Password == enteredPassword;
    } // verifies their passwords and usernames with database

    public void DeleteAccount()
    {
    } // should delete their account definitely

    public string ViewFavRecipes()
    {
        string returnStr = "";
        returnStr += "Favorite Recipes:\n";
        foreach (var recipe in _favRecipes)
        {
            returnStr += $"{recipe.Name}\n"; // Assuming Name property exists in Recipe class
        }

        return returnStr;
    }

    internal bool UpdateRecipe(string name, string updatedRecipeName, string updatedDescription, double updatedCookingTime, List<string> updatedPreparation, int updatedServings, int updatedRatings)
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
            // Recipe with the specified name not found
            return false;
        }
    }

}


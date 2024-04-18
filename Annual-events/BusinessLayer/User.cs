    using System.Collections.Generic;
using RecipeInfo;

class User
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
            _age = value; 
        }
    }
    private byte[]? profile_picture; // (GUI) store images in database??

    private List<Recipe> _recipes = new List<Recipe>();
    public List<Recipe> Recipes 
    {
        get { return _recipes; }
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
            _recipes.Add(recipe);
        } // lets them add a recipe to an [] list

        public void AddRecipe(Recipe recipe)
        {
            recipe.Owner = this;
            _recipes.Add(recipe);
        } // just adding a recipe to a list, other than Fav

        public void RemoveFromFavRecipe(Recipe recipe)
        {
            _recipes.Remove(recipe);
        } // removes a recipe from the [] list

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

}

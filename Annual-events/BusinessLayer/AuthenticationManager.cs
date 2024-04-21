using System;
using System.Collections.Generic;
using RecipeInfo;
namespace BusinessLayer;

public class AuthenticationManager
{
    private static AuthenticationManager? _instance;
    public static AuthenticationManager Instance
    {
        get
        {
            _instance ??= new AuthenticationManager();
            return _instance;
        }
    }


    private List<User> users = new();
    private static User? _currentUser;

    private AuthenticationManager()
    {
        // Test data for now, since we dont have a database.
        users.Add(new User("user1", "password1", "Description 1", 25));
        users.Add(new User("user2", "password2", "Description 2", 30));
    }

    public User CurrentUser
    {
        get
        {
            return _currentUser;
        }
    }
    public void AddUser(User user)
    {
        users.Add(user);
    }

    public bool Login(string username, string password)
    {
        foreach (var user in users)
        {
            if (user.Authentication(username, password))
            {
                _currentUser = user;
                return true;
            }
        }
        return false;
    }

    public void Logout()
    {
        _currentUser = null;
    }

    public List<Recipe> GetAllRecipesFromAllUsers()
    {
        List<Recipe> allRecipes = new List<Recipe>();
        foreach (var user in users)
        {
            allRecipes.AddRange(user.Recipes);
        }
        return allRecipes;
    }
}

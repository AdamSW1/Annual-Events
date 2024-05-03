using System;
using System.Collections.Generic;
using DataLayer;
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


    private List<Annual_Events_User> Users {get;} = new();
    private Annual_Events_User? _currentUser;

    public Annual_Events_User CurrentUser
    {
        get
        {
            return _currentUser!;
        }
    }
    public void AddUser(Annual_Events_User user)
    {
        Users.Add(user);
    }
    private AuthenticationManager()
    {
        // Test data for now, since we dont have a database.
        Users.Add(new Annual_Events_User("user1", "password1", "Description 1", 25));
        Users.Add(new Annual_Events_User("user2", "password2", "Description 2", 30));

        AnnualEventsContext.Instance.Annual_Events_User.Add(Users[0]);
        AnnualEventsContext.Instance.Annual_Events_User.Add(Users[1]);
        AnnualEventsContext.Instance.SaveChanges();
    }
    public bool Login(string username, string password)
    {
        foreach (var user in Users)
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
        foreach (var user in Users)
        {
            allRecipes.AddRange(user.Recipes);
        }
        return allRecipes;
    }
}

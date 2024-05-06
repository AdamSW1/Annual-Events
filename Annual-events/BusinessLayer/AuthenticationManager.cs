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


    private List<Annual_Events_User> Users {get;set;} = AnnualEventsContext.Instance.Annual_Events_User.ToList();
    /// <summary>
    /// Updates the list of users to match the database
    /// </summary>
    private void UpdateUsers(){
        Users = AnnualEventsContext.Instance.Annual_Events_User.ToList();
    }
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
        AnnualEventsUserServices.Instance.AddUser(user);
        AnnualEventsContext.Instance.SaveChanges();
        UpdateUsers();
    }

    private AuthenticationManager()
    {}
    public bool Login(string username, string password)
    {
        if(AnnualEventsUserServices.Instance.VerifyLogin(username, password)){
            _currentUser = AnnualEventsUserServices.Instance.GetUserByUsername(username);
            return true;
        }
        return false;
    }

    public void Logout()
    {
        _currentUser = null;
    }


}

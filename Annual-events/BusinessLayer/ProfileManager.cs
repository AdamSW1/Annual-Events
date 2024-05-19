using BusinessLayer;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using RecipeInfo;

namespace DataLayer;
public class Profile// The Profile class is used to manage a Annual_Events_User
{
    public Annual_Events_User Annual_Events_User { get; set; }

    public Profile() // Constructs the Profile object with a Annual_Events_User
    {
    }
     // Updates the profile of a user
    public void UpdateProfile(Annual_Events_User user, string Username,string password,string description, int age)
    {
        user.Username = Username;
        user.Password = AnnualEventsUserServices.Instance.HashPassword(password);
        user.Description = description;
        user.Age = age;
        AnnualEventsUserServices.Instance.DbContext.SaveChanges();
    }
    public void UpdatePFP() // Updates the profile picture of the user
    {
        //Avalonian
        throw new NotImplementedException();
    }
    public void UpdateDescription(string _description) // Updates the description of the user
    {
        var user = AnnualEventsUserServices.Instance.GetUserByUsername(Annual_Events_User.Username);
        user.Description = _description;
        AnnualEventsUserServices.Instance.DbContext.SaveChanges();
    }
}
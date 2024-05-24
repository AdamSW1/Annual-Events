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
    public void UpdateProfile(Annual_Events_User user, string username, string description, int age = 0, string password = null)
    {
        if (!string.IsNullOrEmpty(username))
        {
            user.Username = username;
        }

        if (!string.IsNullOrEmpty(password))
        {
            if (password.Length >= 5)
            {
                user.Password = AnnualEventsUserServices.Instance.HashPassword(password);
            }
            else
            {
                throw new ArgumentException("Password must be at least 5 characters long.");
            }
        }

        if (string.IsNullOrEmpty(password))
        {
            user.Password = BusinessLayer.AuthenticationManager.Instance.CurrentUser.Password;
        }

        if (description != null)
        {
            user.Description = description;
        }

        if (age > 0)
        {
            user.Age = age;
        }
        
        AnnualEventsUserServices.Instance.DbContext.SaveChanges();
    }
    public void UpdatePFP(byte[] pfp) // Updates the profile picture of the user
    {
        BusinessLayer.AuthenticationManager.Instance.CurrentUser.ProfilePicture = pfp;
        AnnualEventsUserServices.Instance.DbContext.SaveChanges();
        
    }
    public void UpdateDescription(string _description) // Updates the description of the user
    {
        var user = AnnualEventsUserServices.Instance.GetUserByUsername(Annual_Events_User.Username);
        user.Description = _description;
        AnnualEventsUserServices.Instance.DbContext.SaveChanges();
    }
}
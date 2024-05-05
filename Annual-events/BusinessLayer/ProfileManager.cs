using DataLayer;using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using RecipeInfo;

namespace BusinessLayer;
class Profile// The Profile class is used to manage a Annual_Events_User
{
    public Annual_Events_User Annual_Events_User { get; set; }

    public Profile(Annual_Events_User _user) // Constructs the Profile object with a Annual_Events_User
    {
       Annual_Events_User = _user;
    }
    public void UpdatePWD(string _password) // Updates the password of a user
    {
        var user = AnnualEventsUserServices.Instance.GetUserByUsername(Annual_Events_User.Username);
        user.Password = _password;
        //Needa hash the password
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
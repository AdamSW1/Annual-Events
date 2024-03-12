namespace BusinessLayer;
class Profile // The Profile class is used to manage a User
{
    private User User { get; set; }

    public Profile(User _user) // Constructs the Profile object with a User
    {
        User = _user;
    }
    public void UpdatePWD(string _password) // Updates the password of a user
    {
        
    }
    public void UpdatePFP() // Updates the profile picture of the user
    {
        
    }
    public void UpdateDescription(string _description) // Updates the description of the user
    {
        
    }
}
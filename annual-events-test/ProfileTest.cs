namespace annual_events_test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;
using DataLayer;

[TestClass]
public class ProfileTests
{
    // Checks if the password is updated
    [TestMethod]
    public void UpdatePasswordTest()
    {
        // Arrange
        var user = new Annual_Events_User ("testUser", "password", "Test user", 30);

        var newPassword = "newPassword";
        Profile profile = new Profile();
        // Act
        profile.UpdateProfile(user,user.Username,newPassword,user.Description,user.Age);
        // Assert
        Assert.IsTrue(AnnualEventsUserServices.Instance.VerifyPassword(user.Password, AnnualEventsUserServices.Instance.HashPassword(newPassword)));
    }
    // Checks if the profile picture is changed, might be related to GUI 
    [TestMethod]
    public void UpdateProfilePictureTest()
    {
        // Arrange
        // Act
        // Assert
    }
    // Checks if the description of the user is updated
    [TestMethod]
    public void UpdateDescriptionTest()
    {
        // Arrange
        var user = new Annual_Events_User ("testUser", "password", "Test user", 30);
        var newdesc = "newPassword";
        Profile profile = new Profile();
        // Act
        profile.UpdateProfile(user,user.Username,user.Password,newdesc,user.Age);
        // Assert
        Assert.AreEqual(newdesc, user.Description);
    }
}
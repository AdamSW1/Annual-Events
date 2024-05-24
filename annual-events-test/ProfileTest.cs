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
        var user = new Annual_Events_User ("testUser", "password", "Test user", 30, null);

        var newPassword = "newPassword";
        Profile profile = new Profile();
        // Act
        profile.UpdateProfile(user,user.Username,user.Description,user.Age,newPassword);
        // Assert
        Assert.IsTrue(AnnualEventsUserServices.Instance.VerifyPassword(newPassword,user.Password));
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
        var user = new Annual_Events_User ("testUser", "password", "Test user", 30,null);
        var newdesc = "newPassword";
        Profile profile = new Profile();
        // Act
        profile.UpdateProfile(user,user.Username,newdesc,user.Age,user.Password);
        // Assert
        Assert.AreEqual(newdesc, user.Description);
    }
}
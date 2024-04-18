using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;
using RecipeInfo;
namespace annual_events_test;

[TestClass]
public class AuthenticationManagerTest
{
    [TestMethod]
    public void Login_ValidCredentials_Success()
    {
        // Arrange
        var authManager = new AuthenticationManager();
        string username = "user1";
        string password = "password1";

        // Act
        bool loginResult = authManager.Login(username, password);

        // Assert
        Assert.IsTrue(loginResult);
    }

    [TestMethod]
    public void Login_CurrentUser_Success()
    {
        // Arrange
        var authManager = new AuthenticationManager();
        string username = "user1";
        string password = "password1";

        // Act
        bool loginResult = authManager.Login(username, password);

        // Assert
        Assert.IsNotNull(authManager.CurrentUser);
    }

    [TestMethod]
    public void Login_InvalidCredentials_Failure()
    {
        // Arrange
        var authManager = new AuthenticationManager();
        string username = "user1 ";
        string password = "password1 ";

        // Act
        bool loginResult = authManager.Login(username, password);

        // Assert
        Assert.IsFalse(loginResult);
        Assert.IsNull(authManager.CurrentUser);
    }


    [TestMethod]
    public void Login_InvalidCredentialsCurrentUser_Failure()
    {
        // Arrange
        var authManager = new AuthenticationManager();
        string username = "user1 ";
        string password = "password1 ";

        // Act
        bool loginResult = authManager.Login(username, password);

        // Assert
        Assert.IsNull(authManager.CurrentUser);
    }

    [TestMethod]
    public void GetAllRecipesFromAllUsers_NoUsers_ReturnsEmptyList()
    {
        // Arrange
        var authManager = new AuthenticationManager();

        // Act
        List<Recipe> allRecipes = authManager.GetAllRecipesFromAllUsers();

        // Assert
        Assert.AreEqual(0, allRecipes.Count);
    }
}

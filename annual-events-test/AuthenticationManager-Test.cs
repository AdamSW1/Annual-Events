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
        string username = "user1";
        string password = "password1";

        // Act
        bool loginResult = AuthenticationManager.Instance.Login(username, password);

        // Assert
        Assert.IsTrue(loginResult);
    }

    [TestMethod]
    public void Login_CurrentUser_Success()
    {
        // Arrange
        string username = "user1";
        string password = "password1";

        // Act
        bool loginResult = AuthenticationManager.Instance.Login(username, password);

        // Assert
        Assert.IsNotNull(AuthenticationManager.Instance.CurrentUser);
    }
    [TestInitialize]
    public void TestInitialize()
    {
        AuthenticationManager.Instance.Logout();
    }

    [TestMethod]
    public void Login_InvalidCredentials_Failure()
    {
        // Arrange
        string username = "user1 ";
        string password = "password1 ";

        // Act
        bool loginResult = AuthenticationManager.Instance.Login(username, password);

        // Assert
        Assert.IsFalse(loginResult);
        Assert.IsNull(AuthenticationManager.Instance.CurrentUser);
    }

    [TestMethod]
    public void GetAllRecipesFromAllUsers_NoUsers_ReturnsEmptyList()
    {
        // Arrange

        // Act
        List<Recipe> allRecipes = AuthenticationManager.Instance.GetAllRecipesFromAllUsers();

        // Assert
        Assert.AreEqual(0, allRecipes.Count);
    }
}

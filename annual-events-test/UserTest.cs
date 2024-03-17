using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;

[TestClass]
public class UserTests
{
    [TestMethod]
    // Creating a user
    public void CreateUserTest()
    {
        // Arrange

        // Act

        // Assert
    }
    // Adding a recipe to a list
    [TestMethod]
    public void AddToFavRecipeTest()
    {
        // Arrange

        // Act

        // Assert
    }
    // Add recipe to a regular list
    [TestMethod]
    public void AddRecipeTest()
    {
        // Arrange

        // Act

        // Assert
    }
    // Removing a recipe from a regular list
    [TestMethod]
    public void RemoveFromFavRecipeTest()
    {
        // Arrange

        // Act

        // Assert
    }
    // 2 Step Verification test, should return false if it failed, otherwise true
    [TestMethod]
    public void StepVerificationTest()
    {
        // Arrange

        // Act

        // Assert
    }
    // Testing the hash method if it hashes correctly
    [TestMethod]
    public void HashedPasswordTest()
    {
        // Arrange

        // Act

        // Assert
    }
    // goes hand in hand with HashedPasswordTest, verifies if username is correct + password
    [TestMethod]
    public void AuthenticationTest()
    {
        // Arrange

        // Act

        // Assert
    }
    // Checks if the account is removed from either a list or database
    [TestMethod]
    public void DeleteAccountTest()
    {
        // Arrange

        // Act

        // Assert
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using RecipeInfo;
using System.Linq;
using BusinessLayer;
using System.Security.Cryptography;
using System.Text;
namespace DataLayer.Tests
{
    [TestClass]
    public class AnnualEventsUserServicesTests
    {

        private static void ConfigureDbSetMock<T>(
        IQueryable<T> data, Mock<DbSet<T>> mockDbSet)
        where T : class
        {
            mockDbSet.As<IQueryable<T>>().Setup(mock => mock.Provider)
            .Returns(data.Provider);
            mockDbSet.As<IQueryable<T>>().Setup(mock => mock.Expression)
            .Returns(data.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(mock => mock.ElementType)
            .Returns(data.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(mock => mock.GetEnumerator())
            .Returns(data.GetEnumerator());
        }

        [TestMethod]
        public void AddUser_WhenCalled_ShouldAddUserToDatabase()
        {
            // Arrange
            var username = "Testing";
            var password = "Password";
            var hashedPassword = HashPassword(password);
            var user = new Annual_Events_User(username, hashedPassword, "Test test test", 5,null);

            var mockContext = new Mock<AnnualEventsContext>();
            var mockSet = new Mock<DbSet<Annual_Events_User>>();
            mockContext.Setup(m => m.Annual_Events_User).Returns(mockSet.Object);
            var userService = new AnnualEventsUserServices(mockContext.Object);

            // Act
            userService.AddUser(user);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Annual_Events_User>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void GetUserByUsername_UserExists_ShouldReturnUser()
        {
            // Arrange
            string username = "Testing";
            var user = new Annual_Events_User(username, "HashedPassword", "Test user", 30,null);

            var userList = new List<Annual_Events_User> { user };

            var mockSet = new Mock<DbSet<Annual_Events_User>>();
            ConfigureDbSetMock(userList.AsQueryable(), mockSet); // Configure mock DbSet

            var mockContext = new Mock<AnnualEventsContext>();
            mockContext.Setup(m => m.Annual_Events_User).Returns(mockSet.Object);

            var userService = new AnnualEventsUserServices(mockContext.Object);

            // Act
            var resultUser = userService.GetUserByUsername(username);

            // Assert
            Assert.IsNotNull(resultUser);
            Assert.AreEqual(username, resultUser.Username);
        }

        [TestMethod]
        public void GetUserByUsername_NonExistingUser_ShouldReturnNull()
        {
            // Arrange
            string username = "NonExistingUser";

            var userList = new List<Annual_Events_User>(); // Empty list

            var mockSet = new Mock<DbSet<Annual_Events_User>>();
            ConfigureDbSetMock(userList.AsQueryable(), mockSet); // Configure mock DbSet

            var mockContext = new Mock<AnnualEventsContext>();
            mockContext.Setup(m => m.Annual_Events_User).Returns(mockSet.Object);

            var userService = new AnnualEventsUserServices(mockContext.Object);

            // Act
            var resultUser = userService.GetUserByUsername(username);

            // Assert
            Assert.IsNull(resultUser);
        }

        [TestMethod]
        public void VerifyLogin_ValidCredentials_ShouldReturnTrue()
        {
            // Arrange
            var username = "Testing";
            var password = "Password";
            var storedHashedPassword = HashPassword(password);

            // Create a mock user in the database
            var mockUser = new Annual_Events_User(username, storedHashedPassword, "Test test test", 5,null);
            var userList = new List<Annual_Events_User> { mockUser };

            var mockSet = new Mock<DbSet<Annual_Events_User>>();
            ConfigureDbSetMock(userList.AsQueryable(), mockSet);

            var mockContext = new Mock<AnnualEventsContext>();
            mockContext.Setup(m => m.Annual_Events_User).Returns(mockSet.Object);

            var userService = new AnnualEventsUserServices(mockContext.Object);

            // Act
            bool loginResult = userService.VerifyLogin(username, password);

            // Assert
            Assert.IsTrue(loginResult);
        }

        [TestMethod]
        public void VerifyLogin_IncorrectCredentials_ShouldReturnFalse()
        {
            // Arrange
            var username = "Testing";
            var correctPassword = "CorrectPassword";
            var incorrectPassword = "IncorrectPassword";

            // Create a mock user with correct password
            var mockUser = new Annual_Events_User(username, HashPassword(correctPassword), "Test test test", 5,null);
            var userList = new List<Annual_Events_User> { mockUser };

            var mockSet = new Mock<DbSet<Annual_Events_User>>();
            ConfigureDbSetMock(userList.AsQueryable(), mockSet);

            var mockContext = new Mock<AnnualEventsContext>();
            mockContext.Setup(m => m.Annual_Events_User).Returns(mockSet.Object);

            var userService = new AnnualEventsUserServices(mockContext.Object);

            // Act
            bool loginResult = userService.VerifyLogin(username, incorrectPassword);

            // Assert
            Assert.IsFalse(loginResult);
        }

        [TestMethod]
        public void DeleteUser_WhenCalled_ShouldDeleteUserFromDatabase()
        {
            // Arrange
            var user = new Annual_Events_User("Testing", "Password", "Test test test", 5,null);

            var mockSet = new Mock<DbSet<Annual_Events_User>>();
            var mockContext = new Mock<AnnualEventsContext>();
            mockContext.Setup(m => m.Annual_Events_User).Returns(mockSet.Object);

            var userService = new AnnualEventsUserServices(mockContext.Object);

            // Act
            userService.DeleteUser(user);

            // Assert
            mockSet.Verify(m => m.Remove(It.IsAny<Annual_Events_User>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Compute hash from the password
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert hashed bytes to string
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }

}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using RecipeInfo;
using System.Linq;
using BusinessLayer;
namespace DataLayer.Tests
{
    [TestClass]
    public class AnnualEventsUserServicesTests
    {
        [TestMethod]
        public void AddUser_WhenCalled_ShouldAddUserToDatabase()
        {
            // Arrange
            var user = new Annual_Events_User("Testing", "Password", "Test test test", 5);

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
        public void DeleteUser_WhenCalled_ShouldDeleteUserFromDatabase()
        {
            // Arrange
            var user = new Annual_Events_User("Testing", "Password", "Test test test", 5);

            var mockContext = new Mock<AnnualEventsContext>();
            var mockSet = new Mock<DbSet<Annual_Events_User>>();
            mockContext.Setup(m => m.Annual_Events_User).Returns(mockSet.Object);
            var userService = new AnnualEventsUserServices(mockContext.Object);

            // Act
            userService.DeleteUser(user);

            // Assert
            mockContext.Verify(m => m.Recipe.RemoveRange(It.IsAny<List<Recipe>>()), Times.Exactly(2)); // Assuming 2 calls to RemoveRange for user's recipes and favorite recipes
            mockContext.Verify(m => m.Annual_Events_User.Remove(It.IsAny<Annual_Events_User>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }

}

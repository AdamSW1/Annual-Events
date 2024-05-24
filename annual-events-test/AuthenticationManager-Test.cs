using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;
using RecipeInfo;
using DataLayer;
using Moq;
using Microsoft.EntityFrameworkCore;
namespace annual_events_test;

[TestClass]
public class AuthenticationManagerTest
{
    private Recipe CreateExampleRecipe(Annual_Events_User cur)
    {
        Ingredient flour = new("flour", 7);
        Ingredient egg = new("egg", 3);
        List<Ingredient> ingredients = new() { flour, egg };
        List<RecipeIngredient> recipeIngredients = ingredients.Select(ingredient => new RecipeIngredient { Ingredient = ingredient, Quantity = "4" }).ToList();

        List<RecipeTag> tags = new List<RecipeTag>() { new("cake"), new("chocolate") };
        Recipe exampleRecipe = new(
            name: "Chocolate cake",
            description: "A simple chocolate cake",
            cookingTime: 120,
            preparation: new List<Preparation>()
                {
                    new(1, "bake"),
                    new(2, "put in oven"),
                    new(3, "do stuff")
                },
            servings: 8,
            ingredients: recipeIngredients,
            favourite: 0,
            owner: cur,
            tags: tags,
            reviews: new List<Review> { new("reviewer1", "review1", 4) }
                                            );

        return exampleRecipe;
    }
    private Recipe CreateExampleRecipe2(Annual_Events_User cur)
    {
        Ingredient flour = new("oil", 7);
        Ingredient egg = new("beer", 3);
        List<Ingredient> ingredients = new() { flour, egg };
        List<RecipeIngredient> recipeIngredients = ingredients.Select(ingredient => new RecipeIngredient { Ingredient = ingredient, Quantity = "3" }).ToList();

        List<RecipeTag> tags = new List<RecipeTag>() { new("cake"), new("beer"), new("chocolate") };
        Recipe exampleRecipe = new(
            name: "Chocolate beer cake",
            description: "A simple beer chocolate cake",
            cookingTime: 100,
            preparation: new List<Preparation>()
                {
                    new(1, "bake"),
                    new(2, "put in oven"),
                    new(3, "take out")
                },
            servings: 8,
            ingredients: recipeIngredients,
            favourite: 1,
            owner: cur,
            tags: tags,
            reviews:   new List<Review> { new("reviewer2", "review2", 3) }
            );

        return exampleRecipe;
    }
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
        //Arrange
        var user = new Annual_Events_User("testUser", "password", "Test user", 30,null);
        user = null;
        var data_list = new List<Recipe>
        {
            CreateExampleRecipe(user),
            CreateExampleRecipe2(user)
        };
        var data = data_list.AsQueryable();
        //context
        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(data.GetEnumerator());
        var mockContext = new Mock<AnnualEventsContext>();
        mockContext.Setup(r => r.Recipe).Returns(mockSet.Object);
        var service = RecipeServices.Instance;
        service.DbContext = mockContext.Object;
        //act
        var allRecipes = service.GetRecipesFavByUser(user);
        Assert.AreEqual(0,allRecipes.Count);
        Assert.IsNotNull(allRecipes);
    }
}

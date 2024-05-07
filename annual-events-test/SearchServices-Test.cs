using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;
using System;
using RecipeInfo;
using System.Reflection;
using Moq;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
namespace annual_events_test;

[TestClass]

public class SearchServicesTest 
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
    public void SearchRecipesByKeywordTest() 
    {
        // Arrange
        var user = new Annual_Events_User("testUser", "password", "Test user", 30);
        var data_list = new List<Recipe>
        {
            CreateExampleRecipe(user),
            CreateExampleRecipe2(user)
        };
        var data = data_list.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(data.GetEnumerator());
        
        var mockContext = new Mock<AnnualEventsContext>();
        mockContext.Setup(c => c.Recipe).Returns(mockSet.Object);

        // Act
        var search = Search.SearchRecipesByKeyword("chocolate", mockContext.Object.Recipe.ToList());
        var search2 = Search.SearchRecipesByKeyword("yo", mockContext.Object.Recipe.ToList());

        // Assert
        Assert.AreEqual(2, search.Count);
        Assert.AreEqual(0, search2.Count);
    }

    [TestMethod]
    public void SearchRecipesByTagsTest() 
    {
        // Arrange
        var user = new Annual_Events_User("testUser", "password", "Test user", 30);
        var data_list = new List<Recipe>
        {
            CreateExampleRecipe(user),
            CreateExampleRecipe2(user)
        };
        var data = data_list.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(data.GetEnumerator());
        
        var mockContext = new Mock<AnnualEventsContext>();
        mockContext.Setup(c => c.Recipe).Returns(mockSet.Object);
        List<RecipeTag> tags = new List<RecipeTag>() { new("cake"), new("chocolate") };
        List<RecipeTag> tags2 = new List<RecipeTag>() { new("yo"), new("hello") };

        // Act
        var search = Search.SearchRecipesByTags(tags, mockContext.Object.Recipe.ToList());
        var search2 = Search.SearchRecipesByTags(tags2, mockContext.Object.Recipe.ToList());

        // Assert
        Assert.AreEqual(2, search.Count);
        Assert.AreEqual(null, search2);
    }

    [TestMethod]
    public void SearchRecipesByTimeConstraintTest() 
    {
        // Arrange
        var user = new Annual_Events_User("testUser", "password", "Test user", 30);
        var data_list = new List<Recipe>
        {
            CreateExampleRecipe(user),
            CreateExampleRecipe2(user)
        };
        var data = data_list.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(data.GetEnumerator());
        
        var mockContext = new Mock<AnnualEventsContext>();
        mockContext.Setup(c => c.Recipe).Returns(mockSet.Object);

        // Act
        var search = Search.SearchRecipesByTimeConstraint(118, mockContext.Object.Recipe.ToList());
        var search2 = Search.SearchRecipesByTimeConstraint(160, mockContext.Object.Recipe.ToList());

        // Assert
        Assert.AreEqual(1, search.Count);
        Assert.AreEqual(0, search2.Count);
    }

    [TestMethod]
    public void SearchRecipesByRatingTest() 
    {
        // Arrange
        var user = new Annual_Events_User("testUser", "password", "Test user", 30);
        var data_list = new List<Recipe>
        {
            CreateExampleRecipe(user),
            CreateExampleRecipe2(user)
        };
        var data = data_list.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(data.GetEnumerator());
        
        var mockContext = new Mock<AnnualEventsContext>();
        mockContext.Setup(c => c.Recipe).Returns(mockSet.Object);

        // Act
        var search = Search.SearchRecipesByRating(4, mockContext.Object.Recipe.ToList());
        var search2 = Search.SearchRecipesByRating(1, mockContext.Object.Recipe.ToList());

        // Assert
        Assert.AreEqual(1, search.Count);
        Assert.AreEqual(0, search2.Count);
    }

    [TestMethod]
    public void SearchRecipesByServingsTest() 
    {
        // Arrange
        var user = new Annual_Events_User("testUser", "password", "Test user", 30);
        var data_list = new List<Recipe>
        {
            CreateExampleRecipe(user),
            CreateExampleRecipe2(user)
        };
        var data = data_list.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(data.GetEnumerator());
        
        var mockContext = new Mock<AnnualEventsContext>();
        mockContext.Setup(c => c.Recipe).Returns(mockSet.Object);

        // Act
        var search = Search.SearchRecipesByServings(8, mockContext.Object.Recipe.ToList());
        var search2 = Search.SearchRecipesByServings(1, mockContext.Object.Recipe.ToList());

        // Assert
        Assert.AreEqual(2, search.Count);
        Assert.AreEqual(0, search2.Count);
    }

    [TestMethod]
    public void SearchRecipesInFavouritesTest() 
    {
        // Arrange
        var user = new Annual_Events_User("testUser", "password", "Test user", 30);
        var data_list = new List<Recipe>
        {
            CreateExampleRecipe(user),
            CreateExampleRecipe2(user)
        };
        var data = data_list.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(data.GetEnumerator());
        
        var mockContext = new Mock<AnnualEventsContext>();
        mockContext.Setup(c => c.Recipe).Returns(mockSet.Object);

        // Act
        var search = Search.SearchRecipesInFavorites(1, mockContext.Object.Recipe.ToList());
        var search2 = Search.SearchRecipesInFavorites(8, mockContext.Object.Recipe.ToList());

        // Assert
        Assert.AreEqual(1, search.Count);
        Assert.AreEqual(0, search2.Count);
    }

    [TestMethod]
    public void SearchRecipesByOwnerUsernameTest() 
    {
        // Arrange
        var user = new Annual_Events_User("testUser", "password", "Test user", 30);
        var data_list = new List<Recipe>
        {
            CreateExampleRecipe(user),
            CreateExampleRecipe2(user)
        };
        var data = data_list.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(data.GetEnumerator());
        
        var mockContext = new Mock<AnnualEventsContext>();
        mockContext.Setup(c => c.Recipe).Returns(mockSet.Object);

        // Act
        var search = Search.SearchRecipesByOwnerUsername("testUser", mockContext.Object.Recipe.ToList());
        var search2 = Search.SearchRecipesByOwnerUsername("hello123", mockContext.Object.Recipe.ToList());

        // Assert
        Assert.AreEqual(2, search.Count);
        Assert.AreEqual(0, search2.Count);
    }

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

}
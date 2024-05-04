using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;
using System;
using RecipeInfo;
using System.Reflection;
using Moq;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using RecipeInfo;
using System.Security.Cryptography.X509Certificates;
namespace annual_events_test;

[TestClass]

public class RecipeServicesTest
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

    private Recipe CreateExampleRecipe(Annual_Events_User cur)
    {
        Ingredient flour = new("flour", 7);
        Ingredient egg = new("egg", 3);
        List<Ingredient> ingredients = new() { flour, egg };
        List<RecipeIngredient> recipeIngredients = ingredients.Select(ingredient => new RecipeIngredient { Ingredient = ingredient, Quantity = "4" }).ToList();

        List<RecipeTag> tags = new List<RecipeTag>() { new("cake"), new("chocolate") };
        Recipe exampleRecipe = new("Chocolate cake",
                                            "A simple chocolate cake",
                                            120,
                                            new List<Preparation>(){
                                                new(1, "bake"),
                                                new(2, "put in oven"),
                                                new(3, "do stuff")
                                            },
                                            8,
                                            recipeIngredients,
                                            0,
                                            cur,
                                            tags, new List<Review> { new("reviewer1", "review1", 4) }
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
        Recipe exampleRecipe = new("Chocolate beer cake",
                                            "A simple beer chocolate cake",
                                            120,
                                            new List<Preparation>(){
                                                new(1, "bake"),
                                                new(2, "put in oven"),
                                                new(3, "take out")
                                            },
                                            8,
                                            recipeIngredients,
                                            0,
                                            cur,
                                            tags, new List<Review> { new("reviewer2", "review2", 3) }
                                            );

        return exampleRecipe;
    }

    //Test the method GetRecipes 
    [TestMethod]
    public void Get_All_Recipes()
    {
        //Arrange
        var user = new Annual_Events_User("testUser", "password", "Test user", 30);
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
        var recipes = service.GetRecipes();
        //Assert
        Assert.AreEqual("Chocolate cake", recipes[0].Name);
        Assert.AreEqual("Chocolate beer cake", recipes[1].Name);
    }
    [TestMethod]
    public void Get_Recipe_By_Name()
    {
        //Arrange
        var user = new Annual_Events_User("testUser", "password", "Test user", 30);
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
        var recipe = service.GetRecipe("Chocolate cake");
        //Assert
        Assert.AreEqual("Chocolate cake", recipe.Name);
    }

}
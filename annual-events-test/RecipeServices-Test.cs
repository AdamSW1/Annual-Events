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
    //Test the method GetRecipes when null
    [TestMethod]
    public void Get_All_Recipes_When_Null()
    {
        //Arrange 
        var user = new Annual_Events_User("testUser", "password", "Test user", 30);
        var data_list = new List<Recipe>();
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
        Assert.AreEqual(0, recipes.Count);
    }
    //Test Get a recipe by a given name
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
    //Test the method GetRecipe when no recipe by given name
    [TestMethod]
    public void Get_Recipe_By_Name_When_No_Recipe_By_Given_Name()
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
        var recipe = service.GetRecipe("bobo cake");
        //Assert
        Assert.IsNull(recipe);
    }
    //Test the method GetRecipesByRating
    [TestMethod]
    public void Get_Recipes_By_Rating()
    {
        //Arrange
        var user = new Annual_Events_User("testUser", "password", "Test user", 30);
        var data_list = new List<Recipe>
        {
            CreateExampleRecipe(user),
            CreateExampleRecipe2(user)
        };
        data_list[1].AverageScore = 4;
        data_list[0].AverageScore = 3;
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
        var recipes = service.GetRecipesByRating(4);
        //Assert
        Assert.AreEqual("Chocolate beer cake",data_list[1].Name);
    }
    //Test the method GetRecipesByRating when no recipe by given rating
    [TestMethod]
    public void Get_Recipe_By_Rating_When_No_Recipe_By_Given_Rating()
    {
        //Arrange
        var user = new Annual_Events_User("testUser", "password", "Test user", 30);
        var data_list = new List<Recipe>
        {
            CreateExampleRecipe(user),
            CreateExampleRecipe2(user)
        };
        data_list[1].AverageScore = 4;
        data_list[0].AverageScore = 3;
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
        var recipes = service.GetRecipesByRating(0);
        //Assert
        Assert.AreEqual(0,recipes.Count);
    }
    //Test the method GetRecipesByServings
    [TestMethod]
    public void Get_recipes_By_Servings()
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
        var recipes = service.GetRecipesByServings(8);
        //Assert
        Assert.AreEqual("Chocolate cake", recipes[0].Name);
        Assert.AreEqual("Chocolate beer cake", recipes[1].Name);    
    }
    //Test the method GetRecipesByServings when no recipe by given servings
    [TestMethod]
    public void Get_recipes_By_Servings_When_No_Recipe_By_Given_Servings()
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
        var recipes = service.GetRecipesByServings(100);
        //Assert
        Assert.AreEqual(0,recipes.Count);
    }
    //Test the method GetRecipesByTimeConstraint
    [TestMethod]
    public void Get_Recipes_By_Time_Constraint()
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
        var recipes = service.GetRecipesByTimeConstraint(100);
        //Assert
        Assert.AreEqual("Chocolate beer cake",recipes[0].Name);
    }
    //Test the method GetRecipesByTimeConstraint when no recipe by given time constraint
    [TestMethod]
    public void Get_Recipes_By_Time_Constraint_When_No_Give_Time_Constraint()
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
        var recipes = service.GetRecipesByTimeConstraint(6969);
        //Assert
        Assert.AreEqual(0,recipes.Count);
    }
    //Test the method GetRecipesInFavorites
    [TestMethod]
    public void Get_Recipe_By_Favourites_Count()
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
        var recipes = service.GetRecipesInFavorites(1);
        //Assert
        Assert.AreEqual("Chocolate beer cake", recipes[0].Name);
    }
    //Test the method GetRecipesInFavorites when no recipe by given favourite count
    [TestMethod]
    public void Get_Recipe_By_Favourites_Count_When_No_Given_Favourite_Count()
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
        var recipes = service.GetRecipesInFavorites(9);
        //Assert
        Assert.AreEqual(0,recipes.Count); 
    }

    //Test the method AddRecipe
    [TestMethod]
    public void Add_Recipe()
    {
        //Arrange
        var user = new Annual_Events_User("testUser", "password", "Test user", 30);
        var data_list = new List<Recipe>
        {
            CreateExampleRecipe(user),
            CreateExampleRecipe2(user)
        };
        //context
        var mockSet = new Mock<DbSet<Recipe>>();
        var mockContext = new Mock<AnnualEventsContext>();
        mockContext.Setup(r => r.Recipe).Returns(mockSet.Object);
        var service = RecipeServices.Instance;
        service.DbContext = mockContext.Object;
        //act 
        var recipe = CreateExampleRecipe(user);
        service.AddRecipe(recipe);
        //Assert
        mockSet.Verify(mock => mock.Add(It.Is<Recipe>(
            actualRecipe => recipe.Equals(actualRecipe))),Times.Once());
        mockContext.Verify(mock => mock.SaveChanges(), Times.Once());
    }
    //Test the method Delete Recipe
    [TestMethod]
    public void Delete_Recipe()
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
        mockSet.Setup(r => r.Remove(It.IsAny<Recipe>())).Callback<Recipe>((e)=>data_list.Remove(e));  //https://stackoverflow.com/questions/38556830/moq-testing-delete-method
        mockContext.Setup(r => r.Recipe).Returns(mockSet.Object);
        

        var service = RecipeServices.Instance;
        service.DbContext = mockContext.Object;
        //act 
        var del_recipe = service.GetRecipe("Chocolate cake");
        service.DeleteRecipe(del_recipe);
        //Assert
        mockSet.Verify(mock => mock.RemoveRange(It.IsAny<Recipe>()), Times.Once());
        mockContext.Verify(mock => mock.SaveChanges(), Times.Once());
    }
    //Test the method GetRecipeTag
    [TestMethod]
    public void Get_Recipe_Tag()
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
        var tag = service.GetRecipeTag("beer");
        //Assert
        Assert.AreEqual("beer", tag.Tag);
    }
    //Test the method GetIngredient
    [TestMethod]
    public void Get_Ingredient()
    {
        //Arrange
        var user = new Annual_Events_User("testUser", "password", "Test user", 30);
        var data_list = new List<Recipe>
        {
            CreateExampleRecipe(user),
            CreateExampleRecipe2(user)
        };
        var data = data_list.AsQueryable();
        //Ingredients
        Ingredient flour = new("flour", 7);
        Ingredient egg = new("egg", 3);
        List<Ingredient> ingredients = new() { flour, egg };
        List<RecipeIngredient> recipeIngredients = ingredients.Select(ingredient => new RecipeIngredient { Ingredient = ingredient, Quantity = "4" }).ToList();
        var ingData = recipeIngredients.AsQueryable();
        data_list.ForEach(recipe => recipe.RecipeIngredients.ForEach(ingredient => ingredient.Recipe = recipe));
        //context
        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(data.GetEnumerator());
        var ingMockSet = new Mock<DbSet<RecipeIngredient>>();
        ingMockSet.As<IQueryable<RecipeIngredient>>().Setup(r => r.Provider).Returns(recipeIngredients.AsQueryable().Provider);
        ingMockSet.As<IQueryable<RecipeIngredient>>().Setup(r => r.Expression).Returns(recipeIngredients.AsQueryable().Expression);
        ingMockSet.As<IQueryable<RecipeIngredient>>().Setup(r => r.ElementType).Returns(recipeIngredients.AsQueryable().ElementType);
        ingMockSet.As<IQueryable<RecipeIngredient>>().Setup(r => r.GetEnumerator()).Returns(recipeIngredients.AsQueryable().GetEnumerator());
        var mockContext = new Mock<AnnualEventsContext>();
        mockContext.Setup(r => r.Recipe).Returns(mockSet.Object);
        mockContext.Setup(r => r.RecipeIngredients).Returns(ingMockSet.Object);
        var service = RecipeServices.Instance;
        service.DbContext = mockContext.Object;
        //act 
        data_list.ForEach(recipe => service.AddRecipe(recipe));
        var recipesss = service.GetRecipes();
        var ingredient = service.GetIngredient("flour");
        //Assert
        Assert.AreEqual("flour", ingredient!.Name);
    }
    //Test the method GetRecipesByFavUser
    [TestMethod]
    public void Get_Recipes_Fav_By_User()
    {
        //Arrange
        var user = new List<Annual_Events_User>
        { new Annual_Events_User ("testUser", "password", "Test user", 30),
          new Annual_Events_User ("testUser2", "password", "Test user2", 40)
        };
        var data_list = new List<Recipe>
        {
            CreateExampleRecipe(user[0]),
            CreateExampleRecipe2(user[1])
        };
        data_list.ForEach(recipe => user.ForEach(u => u.AddToFavRecipe(recipe)));
        user.ForEach(u => data_list.ForEach(recipe => recipe.AddFavouriteBy(u)));
        data_list.ForEach(recipe => recipe.RecipeIngredients.ForEach(ingredient => ingredient.Recipe = recipe));
        var data = data_list.AsQueryable();
        var user_data = user.AsQueryable();
        //context
        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(data.GetEnumerator());
        var userMockSet = new Mock<DbSet<Annual_Events_User>>();
        userMockSet.As<IQueryable<Annual_Events_User>>().Setup(r => r.Provider).Returns(user_data.Provider);
        userMockSet.As<IQueryable<Annual_Events_User>>().Setup(r => r.Expression).Returns(user_data.Expression);
        userMockSet.As<IQueryable<Annual_Events_User>>().Setup(r => r.ElementType).Returns(user_data.ElementType);
        userMockSet.As<IQueryable<Annual_Events_User>>().Setup(r => r.GetEnumerator()).Returns(user_data.GetEnumerator());
        var mockContext = new Mock<AnnualEventsContext>();
        mockContext.Setup(r => r.Recipe).Returns(mockSet.Object);
        mockContext.Setup(u => u.Annual_Events_User).Returns(userMockSet.Object);
        var service = RecipeServices.Instance;
        service.DbContext = mockContext.Object;
        //act 
        var recipes = service.GetRecipesFavByUser(user[0]);
        //Assert
        Assert.AreEqual("Chocolate beer cake", recipes[1].Name);
    }
}
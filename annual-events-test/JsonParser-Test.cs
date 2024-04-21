using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;
using sorters;
using System;
using System.Net.Http.Headers;
using System.Diagnostics;
using API;
using System.Runtime.CompilerServices;
namespace annual_events_test;

[TestClass]

public class JsonParserTest
{
    [TestMethod]
    public void GetIngredientTestName()
    {
        // Arrange
        string ingredientName = "apple";
        USDPrice usdPrice = new USDPrice { plain = "0.72", formatted = "$0.72/each" };
        Ingredient_json expectedIngredient = new Ingredient_json { name = ingredientName, price = new Dictionary<string, USDPrice> { { "usd", usdPrice } } };

        // Act
        Ingredient_json? actualIngredient = JsonParser.GetIngredient(ingredientName);
        
        // Assert
        Assert.AreEqual(expectedIngredient.name, actualIngredient?.name);       
    }
    [TestMethod]
    public void GetIngredientTestPricePlain()
    {
        // Arrange
        string ingredientName = "apple";
        USDPrice usdPrice = new USDPrice { plain = "0.72", formatted = "$0.72/each" };
        Ingredient_json expectedIngredient = new Ingredient_json { name = ingredientName, price = new Dictionary<string, USDPrice> { { "usd", usdPrice } } };

        // Act
        Ingredient_json? actualIngredient = JsonParser.GetIngredient(ingredientName);

        // Assert     
        Assert.AreEqual(expectedIngredient.price["usd"].plain, actualIngredient?.price?["usd"].plain);
    }
    [TestMethod]
    public void GetIngredientTestPriceFormatted()
    {
        // Arrange
        string ingredientName = "apple";
        USDPrice usdPrice = new USDPrice { plain = "0.72", formatted = "$0.72/each" };
        Ingredient_json expectedIngredient = new Ingredient_json { name = ingredientName, price = new Dictionary<string, USDPrice> { { "usd", usdPrice } } };

        // Act
        Ingredient_json? actualIngredient = JsonParser.GetIngredient(ingredientName);

        // Assert
        Assert.AreEqual(expectedIngredient.price["usd"].formatted, actualIngredient?.price?["usd"].formatted);
    }
}
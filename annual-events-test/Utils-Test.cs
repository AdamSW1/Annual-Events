using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;
using sorters;
using System;
using System.Net.Http.Headers;
using System.Diagnostics;
using RecipeInfo;
namespace annual_events_test;

[TestClass]

public class UtilsTest
{
    [TestMethod]
    public void ValidateTagsTest()
    {
        //Arrange
        Utils utils = new Utils();
        List<string> tags = new List<string>{"vegetarian","vegan"};
        List<RecipeTags> tags_expected = new List<RecipeTags>{RecipeTags.vegetarian,RecipeTags.vegan};
        //Act
        List<RecipeTags> tags_test = Utils.ValidateTags(tags);
        //Assert
        CollectionAssert.AreEquivalent(tags_test,tags_expected);
    }

    [TestMethod]
    public void CheckDouble_GreaterThan0_returnsTrue(){
        //arrange
        double test = 1;
        
        //act
        bool check = Utils.CheckDouble(test);

        //assert
        Assert.IsTrue(check);
    }

    [TestMethod]
    public void CheckDouble_LessThan0_returnsFalse(){
        //arrange
        double test = -10;
        
        //act
        bool check = Utils.CheckDouble(test);

        //assert
        Assert.IsFalse(check);
    }

    [TestMethod]
    public void CheckString_validString_ReturnsTrue(){
        //arrange
        string test = "hello";

        //act
        bool check = Utils.CheckString(test);

        //assert
        Assert.IsTrue(check);
    }

    [TestMethod]
    public void CheckString_whitespaceString_ReturnsFalse(){
        //arrange
        string test = "  ";

        //act
        bool check = Utils.CheckString(test);

        //assert
        Assert.IsFalse(check);
    }

    [TestMethod]
    public void CheckString_emptyString_ReturnsFalse(){
        //arrange
        string test = "";

        //act
        bool check = Utils.CheckString(test);

        //assert
        Assert.IsFalse(check);
    }

    [TestMethod]
    public void CheckName_validName_returnsTrue(){
        //arrange
        string name = "Adam";
        //act
        bool check = Utils.CheckName(name);
        //assert
        Assert.IsTrue(check);
    }
    [TestMethod]
    public void CheckName_NameMoreThan30_returnsFalse(){
        //arrange
        string name = "I am more than 30 characters long";
        //act
        bool check = Utils.CheckName(name);
        //assert
        Assert.IsFalse(check);
    }

    [TestMethod]
    public void CheckName_emptyName_returnsFalse(){
        //arrange
        string name = "";
        //act
        bool check = Utils.CheckName(name);
        //assert
        Assert.IsFalse(check);
    }
    [TestMethod]
    public void CheckName_whitespaceName_returnsFalse(){
        //arrange
        string name = "      ";
        //act
        bool check = Utils.CheckName(name);
        //assert
        Assert.IsFalse(check);
    }

    [TestMethod]
    public void CheckLongString_LengthLessThan2000_returnsTrue(){
        //arrange
        string test="i am less than 2000 characters";
        //act
        bool check = Utils.CheckLongString(test);
        //assert
        Assert.IsTrue(check);
    }

    [TestMethod]
    public void CheckLongString_TooLongString_returnsFalse(){
        //arrange
        string test = "";
        for(int i = 0; i < 2001; i ++){
            test += "a";
        }
        //act
        bool check = Utils.CheckLongString(test);
        //assert
        Assert.IsFalse(check);
    }

    [TestMethod]
    public void CheckInt_GreaterThan0_returnsTrue(){
        //arrange
        int test = 1;
        //act
        bool check = Utils.CheckInt(test);
        //assert
        Assert.IsTrue(check);
    }

    [TestMethod]
    public void CheckInt_LessThan0_returnsFalse(){
        //arrange
        int test = -1;
        //act
        bool check = Utils.CheckInt(test);
        //assert
        Assert.IsFalse(check);
    }
}

        //arrange
        //act
        //assert
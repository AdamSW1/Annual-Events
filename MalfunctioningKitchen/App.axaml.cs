using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BusinessLayer;
using DataLayer;
using MalfunctioningKitchen.ViewModels;
using MalfunctioningKitchen.Views;
using RecipeInfo;

namespace MalfunctioningKitchen;

public partial class App : Application
{
    public override void Initialize()
    {
        Setup();
        AddExampleRecipes();
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static void Setup()
    {

        if (AnnualEventsUserServices.Instance.GetUserByUsername("user1") is not null || AnnualEventsUserServices.Instance.GetUserByUsername("user1") is not null)
        {
            return;
        }
        var nothing = new byte[] {};
        AuthenticationManager.Instance.AddUser(new Annual_Events_User("user1", "password1", "Description 1", 25, nothing));
        AuthenticationManager.Instance.AddUser(new Annual_Events_User("user2", "password2", "Description 2", 30, nothing));
    }

    private static void AddExampleRecipes(){

        Annual_Events_User user = AnnualEventsUserServices.Instance.GetUserByUsername("user1");
        Ingredient flour = new Ingredient("flour", 7);
        Ingredient egg = new Ingredient("egg", 3);


        List<Ingredient> ingredients = new List<Ingredient>() { flour, egg };
        List<RecipeIngredient> recipeIngredients = ingredients.Select(ingredient => new RecipeIngredient { Ingredient = ingredient, Quantity = "4" }).ToList();
        List<RecipeIngredient> recipeIngredients2 = ingredients.Select(ingredient => new RecipeIngredient { Ingredient = ingredient, Quantity = "4" }).ToList();

        List<string> tags = new List<string>() { "cake", "chocolate" };
        Recipe exampleRecipe = new Recipe("Chocolate cake",
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
                                            user,
                                            new List<RecipeTag>() { new RecipeTag("vegan") }
                                            , new List<Review>()
                                            );
        Recipe exampleRecipe2 = new Recipe("Vanilla cake",
                                            "A simple Vanilla cake",
                                            100,
                                            new List<Preparation>(){
                                                new(1, "bake"),
                                                new(2, "put in oven"),
                                                new(3, "do stuff")

                                            },
                                            6,
                                            recipeIngredients2,
                                            0,
                                            user,
                                            new List<RecipeTag>() { new RecipeTag("vegan") }
                                            , new List<Review>()
                                            );

        exampleRecipe.AverageScore = 3;
        exampleRecipe2.AverageScore = 5;

        //check if the recipes already exist
        Recipe? checkRecipe1Exists = RecipeServices.Instance.GetRecipe(exampleRecipe.Name);
        Recipe? checkRecipe2Exists = RecipeServices.Instance.GetRecipe(exampleRecipe2.Name);
        if (checkRecipe1Exists is null)
        {
            RecipeManager.AddRecipe(exampleRecipe);
            AnnualEventsContext.Instance.SaveChanges();
        }
        if (checkRecipe2Exists is null)
        {
            RecipeManager.AddRecipe(exampleRecipe2);
            AnnualEventsContext.Instance.SaveChanges();
        }
    }
}
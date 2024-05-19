using System.Reactive;
using ReactiveUI;
using BusinessLayer;
using RecipeInfo;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using DataLayer;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System;
namespace MalfunctioningKitchen.ViewModels;

public class RecipeViewModel : ViewModelBase
{
    private List<Recipe> _recipes = new();
    public Recipe Recipe
    {
        get => _recipe;
        set => this.RaiseAndSetIfChanged(ref _recipe, value);
    }

    private string _recipeName;
    public string RecipeName
    {
        get => _recipeName;
        set => this.RaiseAndSetIfChanged(ref _recipeName, value);
    }

    private string _stars;
    public string Stars
    {
        get => _stars;
        set => this.RaiseAndSetIfChanged(ref _stars, value);
    }

    private int _reviewCount;
    public int ReviewCount
    {
        get => _reviewCount;
        set => this.RaiseAndSetIfChanged(ref _reviewCount, value);
    }
    private string _recipeOwner;
    public string RecipeOwner
    {
        get => _recipeOwner;
        set => this.RaiseAndSetIfChanged(ref _recipeOwner, value);
    }
    private string _description;
    public string Description
    {
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }
    private List<RecipeIngredient> _ingredients = new();
    public List<RecipeIngredient> Ingredients
    {
        get => _ingredients;
        set => this.RaiseAndSetIfChanged(ref _ingredients, value);
    }
    private List<Preparation> _instructions = new();
    public List<Preparation> Instructions
    {
        get => _instructions;
        set => this.RaiseAndSetIfChanged(ref _instructions, value);
    }
    private List<Review> _reviews = new();
    public List<Review> Reviews
    {
        get => _reviews;
        set => this.RaiseAndSetIfChanged(ref _reviews, value);
    }

    public ReactiveCommand<Unit, Unit> Logout { get; }
    public ReactiveCommand<Unit, Unit> NavigateToHomePageCommand { get; }

    private Recipe _recipe = new();

    public RecipeViewModel()
    {
        //Test for 1 recipe
        Recipe = RecipeServices.Instance.GetRecipe("Vanilla Cake");
        RecipeName = Recipe.Name;
        Stars = GetStars();
        ReviewCount = Recipe.Reviews.Count;
        RecipeOwner = Recipe.Owner.Username;
        Description = Recipe.Description;
        Ingredients = Recipe.RecipeIngredients;
        Instructions = Recipe.Preparation;
        Reviews = Recipe.Reviews;

        Logout = ReactiveCommand.Create(() =>{
            AuthenticationManager.Instance.Logout();
        }); 
        NavigateToHomePageCommand = ReactiveCommand.Create(() => { });
    }

    public string GetStars()
    {
        Stars = "";
        for (int i = 0; i < (int)Recipe.AverageScore; i++)
        {
            Stars += "★";
        }
        return Stars;

    }
}
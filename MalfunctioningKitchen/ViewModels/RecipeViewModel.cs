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
using System.Linq;
namespace MalfunctioningKitchen.ViewModels;

public class RecipeViewModel : ViewModelBase
{
    private Recipe _recipe = new Recipe();
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
    private string? _description;
    public string? Description
    {
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }
    private string _ingredients;
    public string Ingredients
    {
        get => _ingredients;
        set => this.RaiseAndSetIfChanged(ref _ingredients, value);
    }
    private string _instructions;
    public string Instructions
    {
        get => _instructions;
        set => this.RaiseAndSetIfChanged(ref _instructions, value);
    }
    private string _reviews;
    public string Reviews
    {
        get => _reviews;
        set => this.RaiseAndSetIfChanged(ref _reviews, value);
    }
    
    private double _cookingTime;
    public double CookingTime
    {
        get => _cookingTime;
        set => this.RaiseAndSetIfChanged(ref _cookingTime, value);
    }
    private int _servings;
    public int Servings
    {
        get => _servings;
        set => this.RaiseAndSetIfChanged(ref _servings, value);
    }
    public string _tags;
    public string Tags
    {
        get => _tags;
        set => this.RaiseAndSetIfChanged(ref _tags, value);
    }

    private bool _faved;
    public bool Faved{
        get => _faved;
        set => this.RaiseAndSetIfChanged(ref _faved, value);
    }
    private bool _visible;
    public bool Visible{
        get => _visible;
        set => this.RaiseAndSetIfChanged(ref _visible, value);
    }

    private bool _userIsOwner;
    public bool UserIsOwner{
        get => _userIsOwner;
        set => this.RaiseAndSetIfChanged(ref _userIsOwner,value);
    }

    public ReactiveCommand<Unit, Unit> Logout { get; }
    public ReactiveCommand<Unit, Unit> NavigateToHomePageCommand { get; }
    public ReactiveCommand<Unit, Unit> NavigateToAddReviewCommand { get; }
    public ReactiveCommand<Unit, Unit> AddFav { get; }
    public ReactiveCommand<Unit, Unit> RemoveFav { get; }
    public ReactiveCommand<Unit, Unit> Edit { get; }
    public ReactiveCommand<Unit, Unit> Delete { get; }


    public RecipeViewModel(Recipe recipe)
    {
        //Test for 1 recipe
        _recipe = recipe;
        RecipeName = Recipe.Name;
        Stars = GetStars();
        ReviewCount = Recipe.Reviews.Count;
        RecipeOwner = Recipe.Owner.Username!;
        Description = Recipe.Description;
        Ingredients = GetIngredients();
        Instructions = GetPreparation();
        Reviews = GetReviewNames();
        CookingTime = Recipe.CookingTime;
        Servings = Recipe.Servings;
        Tags = GetTags();

        Visible = IsVisible(recipe);

        Logout = ReactiveCommand.Create(() =>
        {
            AuthenticationManager.Instance.Logout();
        });
        NavigateToHomePageCommand = ReactiveCommand.Create(() => { });
        NavigateToAddReviewCommand = ReactiveCommand.Create(() => { });
        AddFav = ReactiveCommand.Create(() =>
        {
            AnnualEventsUserServices.Instance.AddFavRecipes(Recipe);
            Faved = true;
        });
        RemoveFav = ReactiveCommand.Create(() =>
        {
            AnnualEventsUserServices.Instance.RemoveFavRecipes(Recipe);
            Faved = false;
        });
        Edit = ReactiveCommand.Create(() => {});
        Delete = ReactiveCommand.Create(() => {});

        // check if the recipe is already favourited by the current user
        // to decided whetehr to show the 'add' or 'remove' favourite button
        Faved = AuthenticationManager.Instance.CurrentUser.FavRecipes.Contains(recipe);

        // check if the current user owns the recipe to hide the add review button
        UserIsOwner = Recipe.Owner.Equals(AuthenticationManager.Instance.CurrentUser);

    }

    private bool IsVisible(Recipe recipe)
    {
        return recipe.Owner.Username == AuthenticationManager.Instance.CurrentUser.Username;
    }

    private string GetIngredients()
    {
        string ing = ""; 
        foreach (var recipeIngredient in Recipe.RecipeIngredients)
        {
            ing += $"\t{recipeIngredient.Quantity} {recipeIngredient.Ingredient}\n";
        }
        return ing;
    }

    private string GetPreparation()
    {
        string prep = "";
        Recipe.Preparation.ForEach(prepStep => prep += $"\t{prepStep.StepNumber} - {prepStep.Step.Trim()}\n");
        return prep;
    }

    public string GetStars()
    {
        Stars = "";
        for (int i = 0; i < (int)Recipe.AverageScore; i++)
        {
            Stars += "★";
        }
        if (Stars == "")
        {
            Stars = string.Empty;
        }
        return Stars;

    }
    public string GetReviewNames()
    {
        string reviews = "";
        foreach (var review in Recipe.Reviews)
        {
            reviews += review.ReviewerUsername + ": " + review.ReviewText + "\n";
        }
        if (reviews == "")
        {
            reviews = "No reviews yet";
        }
        return reviews;
    }
    public string GetTags()
    {
        string tags = "";
        foreach (var tag in Recipe.Tags)
        {
            tags += tag + "\n";
        }
        if (tags == "")
        {
            tags = "No tags";
        }
        return tags;
    }
}
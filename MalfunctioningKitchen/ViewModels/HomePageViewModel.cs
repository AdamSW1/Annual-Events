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
using System.Security.Cryptography;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using HarfBuzzSharp;
namespace MalfunctioningKitchen.ViewModels;

public class HomePageViewModel : ViewModelBase
{
    private ObservableCollection<Recipe> _recipes = new();

    private ObservableCollection<Review> _reviews = new();

    private Recipe _selectedRecipe = new();
    public Recipe SelectedRecipe
    {
        get => _selectedRecipe;
        set => _selectedRecipe = value;
    }

    private Review _selectedReview;
    public Review SelectedReview
    {
        get => _selectedReview;
        set => _selectedReview = value;
    }

    private bool _viewingReviews;

    public bool ViewingReviews
    {
        get => _viewingReviews;
        set => this.RaiseAndSetIfChanged(ref _viewingReviews, value);
    }

    private string _pageMessage;

    public string PageMessage{
        get => _pageMessage;
        set => this.RaiseAndSetIfChanged(ref _pageMessage,value);
    }

    public ReactiveCommand<Unit, Unit> Logout { get; }
    public ReactiveCommand<Unit, Unit> NavigateToSearchRecipeCommand { get; }
    public ReactiveCommand<Unit, Unit> NavigateToUpdateProfileCommand { get; }
    public ReactiveCommand<Unit, Recipe> ViewRecipeCommand { get; }
    public ReactiveCommand<Unit, Unit> NavigateToRecipeCommand { get; }
    public ReactiveCommand<Unit, Unit> NavigateToAddRecipeCommand { get; }
    public ReactiveCommand<Unit, Review> DeleteReviewCommand { get; }


    public ObservableCollection<Recipe> Recipes
    {
        get => this._recipes;
        set => this.RaiseAndSetIfChanged(ref _recipes, value);
    }

    public ObservableCollection<Review> Reviews
    {
        get => this._reviews;
        set => this.RaiseAndSetIfChanged(ref _reviews, value);
    }

    public HomePageViewModel()
    {
        ViewRecommendedRecipes();

        Logout = ReactiveCommand.Create(() =>
        {
            AuthenticationManager.Instance.Logout();
        });
        NavigateToSearchRecipeCommand = ReactiveCommand.Create(() => { });
        NavigateToUpdateProfileCommand = ReactiveCommand.Create(() => { });
        ViewRecipeCommand = ReactiveCommand.Create(() => { return SelectedRecipe; });
        NavigateToRecipeCommand = ReactiveCommand.Create(() => { });
        NavigateToAddRecipeCommand = ReactiveCommand.Create(() => { });
        DeleteReviewCommand = ReactiveCommand.Create(() => { return SelectedReview; });
        ViewingReviews = false;
    }

    public void ViewOwnRecipes()
    {
        ViewingReviews = false;
        Recipes.Clear();
        Reviews.Clear();
        var ownedRecipes = RecipeServices.Instance.GetRecipesByOwner(AuthenticationManager.Instance.CurrentUser);
        if(ownedRecipes.Count != 0){
            PageMessage = "Your Recipes";
        }
        else{
            PageMessage = "No recipes Written";
        }
        ownedRecipes.ForEach(recipe => Recipes.Add(recipe));
    }

    public void ViewRecommendedRecipes()
    {
        ViewingReviews = false;
        Recipes.Clear();
        Reviews.Clear();
        List<Recipe> allRecipes = RecipeServices.Instance.GetRecipes();
        var userRecipes = AuthenticationManager.Instance.CurrentUser.Recipes;
        List<RecipeTag> recommendedTags = new();
        // Gets recipes in all recipes whose tags match any of the tags in the current user's recipes
        // and werent written by the current user
        List<RecipeTag> userTags = userRecipes.SelectMany(recipe => recipe.Tags).Distinct().ToList();

        List<Recipe>? recommendedRecipes = Search.SearchRecipesByTags(
            userTags,
            allRecipes.Where(recipe => 
                recipe.Owner != AuthenticationManager.Instance.CurrentUser)
            .ToList());
        
        recommendedRecipes ??= new();

        recommendedRecipes.ForEach(recipe =>{
            recommendedTags = recipe.Tags.Intersect(userTags).ToList();
        });

        if(recommendedRecipes.Count != 0){
            PageMessage ="Your recommended recipes based on tags:\n";
            PageMessage += string.Join(", ", recommendedTags);
        }
        else{
            PageMessage = "No Recipes To Recommend";
        }

        recommendedRecipes.ForEach(recipe => Recipes.Add(recipe));
    }
    public void ViewFavouriteRecipes()
    {
        Recipes.Clear();
        Reviews.Clear();
        List<Recipe> FavRecipes = RecipeServices.Instance.GetRecipesFavByUser(AuthenticationManager.Instance.CurrentUser);

        if( FavRecipes.Count == 0 ){
            PageMessage ="No favourited recipes";
        }
        else{
            PageMessage ="Your Favourite Recipes";
        }
        FavRecipes.ForEach(recipe => Recipes.Add(recipe));
    }
    public void ViewYourReviews()
    {
        ViewingReviews = true;
        Recipes.Clear();
        Reviews.Clear();
        List<Review> reviews = AnnualEventsService.Instance.GetReviewsForUser(AuthenticationManager.Instance.CurrentUser);
        if( reviews.Count == 0 ){
            PageMessage ="No reviews";
        }
        else{
            PageMessage = "Your Reviews";
        }
        reviews.ForEach(review => Reviews.Add(review));
    }

    public void DeleteReview()
    {
        AnnualEventsService.Instance.DeleteReview(SelectedReview);
        ViewYourReviews();
    }
    public void GetRecipe(Recipe recipe)
    {
        SelectedRecipe = recipe;
        ViewRecipeCommand.Execute().Subscribe();
    }
    public void GetReview(Review review)
    {
        SelectedReview = review;
        DeleteReview();
    }

}
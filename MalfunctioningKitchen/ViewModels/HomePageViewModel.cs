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

    public bool _viewingReviews;

    private bool ViewingReviews
    {
        get => _viewingReviews;
        set => this.RaiseAndSetIfChanged(ref _viewingReviews, value);
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
        ViewAllRecipes();

        Logout = ReactiveCommand.Create(() =>
        {
            AuthenticationManager.Instance.Logout();
        });
        NavigateToSearchRecipeCommand = ReactiveCommand.Create(() => { });
        NavigateToUpdateProfileCommand = ReactiveCommand.Create(() => { });
        ViewRecipeCommand = ReactiveCommand.Create(() => { return SelectedRecipe; });
        NavigateToRecipeCommand = ReactiveCommand.Create(() => { });
        NavigateToAddRecipeCommand = ReactiveCommand.Create(() => { });
        DeleteReviewCommand = ReactiveCommand.Create( () => { return SelectedReview; });
        ViewingReviews = false;
    }

    public void ViewOwnRecipes()
    {
        ViewingReviews = false;
        Recipes.Clear();
        Reviews.Clear();
        var ownedRecipes = RecipeServices.Instance.GetRecipesByOwner(AuthenticationManager.Instance.CurrentUser);
        ownedRecipes.ForEach(recipe => Recipes.Add(recipe));
    }

    public void ViewAllRecipes()
    {
        ViewingReviews = false;
        Recipes.Clear();
        Reviews.Clear();
        List<Recipe> allRecipes = RecipeServices.Instance.GetRecipes();
        allRecipes.ForEach(recipe => Recipes.Add(recipe));
    }
    public void ViewFavouriteRecipes()
    {
        Recipes.Clear();
        Reviews.Clear();
        List<Recipe> FavRecipes = RecipeServices.Instance.GetRecipesFavByUser(AuthenticationManager.Instance.CurrentUser);
        FavRecipes.ForEach(recipe => Recipes.Add(recipe));
    }
    public void ViewYourReviews() 
    {
        ViewingReviews = true;
        Recipes.Clear();
        Reviews.Clear();
        List<Review> reviews = AnnualEventsService.Instance.GetReviewsForUser(AuthenticationManager.Instance.CurrentUser);
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
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

public class HomePageViewModel : ViewModelBase
{
    private ObservableCollection<Recipe> _recipes = new();
    private Recipe _selectedRecipe = new();
    public Recipe SelectedRecipe{
        get => _selectedRecipe;
        set => _selectedRecipe = value;
    }
    public ReactiveCommand<Unit, Unit> Logout { get; }
    public ReactiveCommand<Unit, Unit> NavigateToSearchRecipeCommand { get; }
    public ReactiveCommand<Unit, Unit> NavigateToUpdateProfileCommand { get; }
    public ReactiveCommand<Unit, Recipe> ViewRecipeCommand { get; }

    //Tempororary, ask vijay used for testing
    public ReactiveCommand<Unit, Unit> NavigateToRecipeCommand { get; }


    public ObservableCollection<Recipe> Recipes
    {
        get => this._recipes;
        set => this.RaiseAndSetIfChanged(ref _recipes, value);
    }

    public HomePageViewModel()
    {
        ViewAllRecipes();

        Logout = ReactiveCommand.Create(() =>{
            AuthenticationManager.Instance.Logout();
        }); 
        NavigateToSearchRecipeCommand = ReactiveCommand.Create(() => { });
        NavigateToUpdateProfileCommand = ReactiveCommand.Create(() => { });
        ViewRecipeCommand = ReactiveCommand.Create( () => { return SelectedRecipe; });
        NavigateToRecipeCommand = ReactiveCommand.Create(() => { });

    }

    public void ViewOwnRecipes(){
        Recipes.Clear();
        var ownedRecipes = RecipeServices.Instance.GetRecipesByOwner(AuthenticationManager.Instance.CurrentUser);
        ownedRecipes.ForEach(recipe => Recipes.Add(recipe));
    }

    public void ViewAllRecipes(){
         Recipes.Clear();
        List<Recipe> allRecipes = RecipeServices.Instance.GetRecipes();
        allRecipes.ForEach(recipe => Recipes.Add(recipe));
    }
    public void GetRecipe(Recipe recipe){
        SelectedRecipe = recipe;
        ViewRecipeCommand.Execute().Subscribe();
    }

}
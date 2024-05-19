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
    private List<Recipe> _recipes = new();

    public ReactiveCommand<Unit, Unit> Logout { get; }
    public ReactiveCommand<Unit, Unit> NavigateToSearchRecipeCommand { get; }
    public ReactiveCommand<Unit, Unit> NavigateToUpdateProfileCommand { get; }


    public List<Recipe> Recipes
    {
        get => this._recipes;
        set => this.RaiseAndSetIfChanged(ref _recipes, value);
    }

    public HomePageViewModel()
    {
        Recipes = RecipeServices.Instance.GetRecipes();
        Logout = ReactiveCommand.Create(() =>{
            AuthenticationManager.Instance.Logout();
        }); 
        NavigateToSearchRecipeCommand = ReactiveCommand.Create(() => { });
        NavigateToUpdateProfileCommand = ReactiveCommand.Create(() => { });
    }

}
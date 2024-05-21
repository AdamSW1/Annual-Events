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

public class AddRecipeViewModel : ViewModelBase
{
    private string? _recipeName;
    public string? RecipeName
    {
        get => _recipeName;
        set => this.RaiseAndSetIfChanged(ref _recipeName, value);
    }
    private string? _description;
    public string? Description
    {
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }
    private double _cookingTime;
    public double CookingTime
    {
        get => _cookingTime;
        set => this.RaiseAndSetIfChanged(ref _cookingTime, value);
    }
    private string? _instruction;
    public string? Instruction
    {
        get => _instruction;
        set => this.RaiseAndSetIfChanged(ref _instruction, value);
    }
    private List<Preparation>? _preparations = new List<Preparation>();
    public List<Preparation>? Preparations
    {
        get => _preparations;
        set => this.RaiseAndSetIfChanged(ref _preparations, value);
    }
    private int stepNum = 1;
    private int _servings;
    public int Servings
    {
        get => _servings;
        set => this.RaiseAndSetIfChanged(ref _servings, value);
    }
    private string _ingredientName;
    public string IngredientName
    {
        get => _ingredientName;
        set => this.RaiseAndSetIfChanged(ref _ingredientName, value);
    }
    private int _quantity;
    public int Quantity
    {
        get => _quantity;
        set => this.RaiseAndSetIfChanged(ref _quantity, value);
    }
    private double _price;
    public double Price
    {
        get => _price;
        set => this.RaiseAndSetIfChanged(ref _price, value);
    }
    private List<Ingredient> _ingredientList;
    public List<Ingredient> IngredientList
    {
        get => _ingredientList;
        set => this.RaiseAndSetIfChanged(ref _ingredientList, value);
    }
    private List<RecipeIngredient>? _recipeIngredientList = new List<RecipeIngredient>();
    public List<RecipeIngredient>? RecipeIngredientList
    {
        get => _recipeIngredientList;
        set => this.RaiseAndSetIfChanged(ref _recipeIngredientList, value);
    }
    private List<RecipeTag> _tags = new List<RecipeTag>();
    public List<RecipeTag> Tags
    {
        get => _tags;
        set => this.RaiseAndSetIfChanged(ref _tags, value);
    }
    private List<Review> _reviews = new List<Review>();
    public List<Review> Reviews
    {
        get => _reviews;
        set => this.RaiseAndSetIfChanged(ref _reviews, value);
    }
    private string? _errorMessage;
    public string? ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage,value);
    }
    public ReactiveCommand<Unit,Unit> AddIngredient {get;}
    public ReactiveCommand<Unit,Unit> AddStep {get;}
    public ReactiveCommand<Unit, Unit> Logout { get; }
    public ReactiveCommand<Unit, Unit> NavigateToHomePageCommand { get; }
    public ReactiveCommand<Unit, Unit> CreateRecipe { get; }

    public AddRecipeViewModel()
    {
        Logout = ReactiveCommand.Create(() =>
        {
            AuthenticationManager.Instance.Logout();
        });
        NavigateToHomePageCommand = ReactiveCommand.Create(() => { });
        AddStep = ReactiveCommand.Create(() =>
        {
            AddStepToInstructions("Step " + (_preparations.Count + 1) + ":" + _instruction);
        });
        AddIngredient = ReactiveCommand.Create(() =>
        {
            AddIngredientToList(_ingredientName, _quantity, _price);
        });
        IObservable<bool> areFilledIn = this.WhenAnyValue(
            AddRecipeViewModel => AddRecipeViewModel.RecipeName,
            AddRecipeViewModel => AddRecipeViewModel.Description,
            AddRecipeViewModel => AddRecipeViewModel.CookingTime,
            AddRecipeViewModel => AddRecipeViewModel.Servings,
            AddRecipeViewModel => AddRecipeViewModel.Preparations,
            AddRecipeViewModel => AddRecipeViewModel.IngredientList,
            AddRecipeViewModel => AddRecipeViewModel.Quantity,
            AddRecipeViewModel => AddRecipeViewModel.Price,
            (recipeName, description, cookingTime, servings, instructions, ingredientList, quantity, price) => 
            !(string.IsNullOrEmpty(recipeName) || string.IsNullOrEmpty(description) || cookingTime == 0 || servings == 0 || instructions == null || ingredientList == null || quantity == 0 || price == 0)
        );
        CreateRecipe = ReactiveCommand.Create(() =>
        {
            Recipe recipe = new Recipe(_recipeName,_description,_cookingTime,_preparations,_servings,_recipeIngredientList,0,AuthenticationManager.Instance.CurrentUser,_tags,_reviews);
            RecipeManager.AddRecipe(recipe);
            NavigateToHomePageCommand.Execute().Subscribe();
        }, areFilledIn);
    }

    public void AddStepToInstructions(string instruction)
    {
        if (_preparations == null)
        {
            _preparations = new List<Preparation>();
        }
        _preparations.Add(new Preparation(stepNum, instruction));
        stepNum++;
    }

    public void AddIngredientToList(string name,int quantity, double price)
    {
        if (_ingredientList == null)
        {
            _ingredientList = new List<Ingredient>();
        }
        if (_recipeIngredientList == null)
        {
            _recipeIngredientList = new List<RecipeIngredient>();
        }
        _ingredientList.Add(new Ingredient(_ingredientName, _price));
        _recipeIngredientList.Add(new RecipeIngredient { Ingredient = new Ingredient(name, price), Quantity = quantity.ToString()});
    }
}
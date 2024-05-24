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
using System.Windows.Input;
namespace MalfunctioningKitchen.ViewModels;

public class AddRecipeViewModel : ViewModelBase
{
    private string _recipeName;
    public string RecipeName
    {
        get => _recipeName;
        set 
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Recipe name cannot be empty.");
            }
            else
            {
                this.RaiseAndSetIfChanged(ref _recipeName, value);
            }     
        }      
    }
    private string? _description;
    public string? Description
    {
        get => _description;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Description cannot be empty.");
            }
            else
            {
                this.RaiseAndSetIfChanged(ref _description, value);
            }
        
        }
    }
    private double _cookingTime;
    public double CookingTime
    {
        get => _cookingTime;
        set
        {
            if (value == 0)
            {
                throw new ArgumentException("Cooking time cannot be 0.");
            }
            else
            {
                this.RaiseAndSetIfChanged(ref _cookingTime, value);
            }
        }
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
    private ObservableCollection<Preparation> _preparationList = new();
    public ObservableCollection<Preparation> PreparationList
    {
        get => _preparationList;
        set => this.RaiseAndSetIfChanged(ref _preparationList, value);
    }
    private int stepNum = 0;
    private double _servings;
    public double Servings
    {
        get => _servings;
        set
        {
            if (value == 0)
            {
                throw new ArgumentException("Servings cannot be 0.");
            }
            else
            {
                this.RaiseAndSetIfChanged(ref _servings, value);
            }
        
        }
    }
    private string _ingredientName;
    public string IngredientName
    {
        get => _ingredientName;
        set => this.RaiseAndSetIfChanged(ref _ingredientName, value);
    }
    private string _quantity;
    public string Quantity
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

    private ObservableCollection<RecipeIngredient> _recipeIngredientListObs = new();
    public ObservableCollection<RecipeIngredient> RecipeIngredientListObs
    {
        get => _recipeIngredientListObs;
        set => this.RaiseAndSetIfChanged(ref _recipeIngredientListObs, value);
    }
    private List<string> _allTags;
    public List<string> AllTags
    {
        get => _allTags;
        set => this.RaiseAndSetIfChanged(ref _allTags, value);
    }
    private IList<string> _selectedTags = new List<string>();
    public IList<string> SelectedTags { 
        get => _selectedTags; 
        set => this.RaiseAndSetIfChanged(ref _selectedTags, value); 
        }
    private List<Review> _reviews = new List<Review>();
    public List<Review> Reviews
    {
        get => _reviews;
        set => this.RaiseAndSetIfChanged(ref _reviews, value);
    }
    private string? _title = "Create Recipe";
    public string? Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }
    private string? _errorMessage;
    public string? ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }
    public ReactiveCommand<Unit,Unit> AddIngredient {get;}
    public ReactiveCommand<Unit,Unit> AddStep {get;}
    public ReactiveCommand<Unit, Unit> Logout { get; }
    public ReactiveCommand<Unit, Unit> NavigateToHomePageCommand { get; }
    public ReactiveCommand<Unit, Unit> CreateRecipe { get; }
    public ICommand RemoveStepCommand { get; }
    public ICommand RemoveIngredientCommand { get; }

    public AddRecipeViewModel(Recipe recipe, string typeParentPage)
    {
        //Logout command
        Logout = ReactiveCommand.Create(() =>
        {
            AuthenticationManager.Instance.Logout();
        });
        //Navigate to home page command
        NavigateToHomePageCommand = ReactiveCommand.Create(() => { });
        //Add step command to add preparation steps to the recipe
        AddStep = ReactiveCommand.Create(() =>
        {
            AddStepToInstructions(_instruction);
        });
        RemoveStepCommand = ReactiveCommand.Create((string idex) =>
        {
            RemoveStep(idex);
        });
        //Add ingredient command to add ingredients to the recipe
        AddIngredient = ReactiveCommand.Create(() =>
        {
            AddIngredientToList(_ingredientName, _quantity, _price);
        });
        RemoveIngredientCommand = ReactiveCommand.Create((Object obj) =>
        {
            RemoveIngredient(obj);
        });
        //Add all tags to the list of tags
        _allTags = new List<string>();
        foreach (var tag in Enum.GetValues(typeof(RecipeTags)))
        {
            AllTags.Add(tag.ToString()!);
        }
        //Check if all fields are filled in
        IObservable<bool> areFilledIn = CheckFilledIn();
        //Create recipe command
        CreateRecipe = ReactiveCommand.Create(() =>
        {
            try
            {
                if (typeParentPage.Equals("Edit"))
                { 
                    List<RecipeTag> tags = SelectedTags.Select(tag =>
                    {
                        RecipeTag? existingTag = RecipeServices.Instance.GetRecipeTag(tag);
                        if (existingTag != null)
                        {
                            return existingTag;
                        }
                        else
                        {
                            return new RecipeTag(tag);
                        }
                    }).ToList();
                    recipe.Name = _recipeName!;
                    recipe.Description = _description!;
                    recipe.CookingTime = _cookingTime;
                    recipe.Preparation = _preparations;
                    recipe.Servings = (int)_servings;
                    recipe.RecipeIngredients = _recipeIngredientList;
                    recipe.Owner = AuthenticationManager.Instance.CurrentUser;
                    recipe.Tags = tags;
                    recipe.Reviews = _reviews;
                    RecipeServices.Instance.DbContext.SaveChanges();
                    NavigateToHomePageCommand.Execute().Subscribe();
                }
                else
                {
                    List<RecipeTag> tags = SelectedTags.Select(tag =>
                    {
                        RecipeTag? existingTag = RecipeServices.Instance.GetRecipeTag(tag);
                        if (existingTag != null)
                        {
                            return existingTag;
                        }
                        else
                        {
                            return new RecipeTag(tag);
                        }
                    }).ToList();
                    Recipe recipe = new Recipe(_recipeName, _description, _cookingTime, _preparations, (int)_servings, _recipeIngredientList, 0, AuthenticationManager.Instance.CurrentUser, tags, _reviews);
                    RecipeManager.AddRecipe(recipe);
                    NavigateToHomePageCommand.Execute().Subscribe();
                    

                }
                ErrorMessage = "";
            }
            catch (ArgumentException exc)
            {
                ErrorMessage = exc.Message;
            }
            catch (NullReferenceException exc)
            {
                ErrorMessage = exc.Message;
            }
            catch (Exception exc)
            {
                ErrorMessage = "Could not create recipe if any fields are empty!";
            }
        }, areFilledIn);
        if (typeParentPage != null && typeParentPage.Equals("Edit"))
        {
            _recipeName = recipe.Name;
            _description = recipe.Description;
            _cookingTime = recipe.CookingTime;
            _preparations = recipe.Preparation;
            _servings = recipe.Servings;
            _recipeIngredientList = recipe.RecipeIngredients;
            _reviews = recipe.Reviews;
            _selectedTags = recipe.Tags.Select(tag => tag.Tag.ToString()).ToList();
            _title = "Edit Recipe";
            _preparationList = new ObservableCollection<Preparation>();
            for (int i = 0; i < _preparations.Count; i++)
            {
                PreparationList.Add(new Preparation(stepNum,"Step " + (i + 1) + ":" + _preparations[i].Step));
                stepNum++;  
            }
            _recipeIngredientListObs = new ObservableCollection<RecipeIngredient>();
            _recipeIngredientList.ForEach(ingredient => RecipeIngredientListObs.Add(ingredient));
        }
    }

    private IObservable<bool> CheckFilledIn()
    {
        return this.WhenAnyValue(
            AddRecipeViewModel => AddRecipeViewModel.RecipeName,
            AddRecipeViewModel => AddRecipeViewModel.Description,
            AddRecipeViewModel => AddRecipeViewModel.CookingTime,
            AddRecipeViewModel => AddRecipeViewModel.Servings,
            AddRecipeViewModel => AddRecipeViewModel.Preparations,
            AddRecipeViewModel => AddRecipeViewModel.IngredientList,
            (recipeName, description, cookingTime, servings, preparations, ingredientList) =>
            !((string.IsNullOrEmpty(recipeName) || string.IsNullOrEmpty(description)) && cookingTime != 0 && servings != 0 && preparations != null && ingredientList != null)
        );
    }

    public void AddStepToInstructions(string instruction)
    {
        if (_preparations == null)
        {
            _preparations = new List<Preparation>();
        }
        if (instruction == null || instruction.Equals(""))
        {
            throw new ArgumentException("Instruction cannot be empty.");
        }
        _preparations.Add(new Preparation(stepNum, instruction));
        PreparationList.Clear();
        for (int i = 0; i < Preparations.Count; i++)
        {
            var step = Preparations[i];
            if (step.Step.Contains("Step"))
            {
                step.Step = step.Step.Split(":")[1];
            }
            PreparationList.Add(new Preparation(i, "Step " + (i + 1) + ":" + step.Step));
        }
        stepNum++;
    }
    private void RemoveStep(string index)
    {
        var idx = Int32.Parse(index);
        if (_preparations.Count > 0)
        {
            _preparations.RemoveAt(idx);
            PreparationList.Clear();
            for (int i = 0; i < Preparations.Count; i++)
            {
                var step = Preparations[i];
                if (step.Step.Contains("Step"))
                {
                    step.Step = step.Step.Split(":")[1];
                }
                PreparationList.Add(new Preparation(i, "Step " + (i + 1) + ":" + step.Step));
            }
            stepNum--;
        }
    }

    private void RemoveIngredient(Object obj)
    {
        var idx = RecipeIngredientListObs.IndexOf((RecipeIngredient)obj);
        if (idx == -1)
        {
            return;
        }
        if (_recipeIngredientList.Count > 0)
        {
            _recipeIngredientList.RemoveAt(idx);
            RecipeIngredientListObs.Clear();
            for (int i = 0; i < RecipeIngredientList.Count; i++)
            {
                RecipeIngredientListObs.Add(RecipeIngredientList[i]);
            }
        }
    }

    public void AddIngredientToList(string name,string quantity, double price)
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
        _recipeIngredientList.Add(new RecipeIngredient { Ingredient = new Ingredient(name, price), Quantity = quantity});
        RecipeIngredientListObs.Clear();
        for (int i = 0; i < RecipeIngredientList.Count; i++)
        {
            RecipeIngredientListObs.Add(RecipeIngredientList[i]);
        }
    }
}
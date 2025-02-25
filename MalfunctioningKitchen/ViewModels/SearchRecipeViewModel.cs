using System.Reactive;
using ReactiveUI;
using BusinessLayer;
using RecipeInfo;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using System.ComponentModel;

namespace MalfunctioningKitchen.ViewModels;

public class SearchRecipeViewModel : ViewModelBase, INotifyPropertyChanged
{
    private string? _notificationMessage;
    private string? _searchKeyword;
    private List<string> _tagCriteria;
    private string? _timeConstraint;
    private string? _rating;
    private string? _servings;
    private bool _favorite = false;
    private string? _ownerUsername;
    private List<Recipe>? _searchedRecipes;
    private string? _ingredient;

    private Recipe _selectedRecipe = new();
    public Recipe SelectedRecipe
    {
        get => _selectedRecipe;
        set => _selectedRecipe = value;
    }

    public string? NotificationMessage
    {
        get => _notificationMessage;
        set => this.RaiseAndSetIfChanged(ref _notificationMessage, value);
    }

    public List<Recipe>? SearchedRecipes
    {
        get => _searchedRecipes;
        set => this.RaiseAndSetIfChanged(ref _searchedRecipes, value);
    }

    public string? SearchKeyword
    {
        get => _searchKeyword;
        set => this.RaiseAndSetIfChanged(ref _searchKeyword, value);
    }

    public List<string> TagCriteria
    {
        get => _tagCriteria;
        set => this.RaiseAndSetIfChanged(ref _tagCriteria, value);
    }

    private IList<string> _selectedTags = new List<string>();
    public IList<string> SelectedTags
    {
        get => _selectedTags;
        set => this.RaiseAndSetIfChanged(ref _selectedTags, value);
    }

    public string? TimeConstraint
    {
        get => _timeConstraint;
        set => this.RaiseAndSetIfChanged(ref _timeConstraint, value);
    }

    public string? Rating
    {
        get => _rating;
        set => this.RaiseAndSetIfChanged(ref _rating, value);
    }

    public string? Servings
    {
        get => _servings;
        set => this.RaiseAndSetIfChanged(ref _servings, value);
    }

    public bool Favorite
    {
        get => _favorite;
        set => this.RaiseAndSetIfChanged(ref _favorite, value);
    }

    public string? OwnerUsername
    {
        get => _ownerUsername;
        set => this.RaiseAndSetIfChanged(ref _ownerUsername, value);
    }

    public string? Ingredient
    {
        get => _ingredient;
        set => this.RaiseAndSetIfChanged(ref _ingredient, value);
    }

    public ReactiveCommand<Unit, Unit> Return { get; }
    public ReactiveCommand<Unit, List<Recipe>> SearchCommand { get; }
    public ReactiveCommand<Unit, Recipe> ViewRecipeCommand { get; }

    public SearchRecipeViewModel()
    {
        Return = ReactiveCommand.Create(() =>
        {
            // Implementation for Return command
        });
        ViewRecipeCommand = ReactiveCommand.Create(() => { return SelectedRecipe; });


        SearchCommand = ReactiveCommand.CreateFromTask(ExecuteSearch);
        ViewRecipeCommand = ReactiveCommand.Create(() => SelectedRecipe);

        _tagCriteria = new List<string>();
        foreach (var tag in Enum.GetValues(typeof(RecipeTags)))
        {
            TagCriteria.Add(tag.ToString()!);
        }

        _searchedRecipes = new List<Recipe>();
    }

    private async Task<List<Recipe>> ExecuteSearch()
    {
        try
        {
            List<RecipeTag> tags = SelectedTags.Select(tag => new RecipeTag(tag)).ToList();

            var timeConstraint = int.TryParse(TimeConstraint, out var time) ? time : (int?)null;
            var rating = int.TryParse(Rating, out var rate) ? rate : (int?)null;
            var servings = int.TryParse(Servings, out var serve) ? serve : (int?)null;

            bool hasSearchCriteria = !string.IsNullOrEmpty(SearchKeyword) || tags.Any() || timeConstraint.HasValue || rating.HasValue || servings.HasValue || Favorite || !string.IsNullOrEmpty(OwnerUsername) || !string.IsNullOrEmpty(Ingredient);

            if (!hasSearchCriteria)
            {
                NotificationMessage = "Please provide at least one search criteria.";
                return new List<Recipe>();
            }

            SearchedRecipes = Search.SearchRecipes(
                keyword: SearchKeyword!,
                tags: tags,
                time: timeConstraint,
                rating: rating,
                servings: servings,
                favourite: Favorite,
                ownerUsername: OwnerUsername!,
                ingredient: Ingredient!
            );

            if (SearchedRecipes == null || SearchedRecipes.Count == 0)
            {
                NotificationMessage = "No recipes found matching the criteria.";
            }
            else
            {
                NotificationMessage = $"{SearchedRecipes.Count} recipes found.";
            }
        }
        catch (Exception ex)
        {
            if (SearchedRecipes!.Count != 0){
                SearchedRecipes.Clear();
            }
            NotificationMessage = $"Error during search: {ex.Message}";
        }

        return SearchedRecipes!;
    }

    public void GetRecipe(Recipe recipe)
    {
        SelectedRecipe = recipe;
        ViewRecipeCommand.Execute().Subscribe();
    }

}


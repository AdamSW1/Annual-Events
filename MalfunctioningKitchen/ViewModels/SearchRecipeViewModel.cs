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
    private string _notificationMessage;
    private string _searchKeyword;
    private List<string> _tagCriteria;
    private string _timeConstraint;
    private string _rating;
    private string _servings;
    private string _favorite;
    private string _ownerUsername;
    private List<Recipe> _searchedRecipes;

    public string NotificationMessage
    {
        get => _notificationMessage;
        set => this.RaiseAndSetIfChanged(ref _notificationMessage, value);
    }

    public List<Recipe> SearchedRecipes
    {
        get => _searchedRecipes;
        set => this.RaiseAndSetIfChanged(ref _searchedRecipes, value);
    }

    public string SearchKeyword
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

    public string TimeConstraint
    {
        get => _timeConstraint;
        set => this.RaiseAndSetIfChanged(ref _timeConstraint, value);
    }

    public string Rating
    {
        get => _rating;
        set => this.RaiseAndSetIfChanged(ref _rating, value);
    }

    public string Servings
    {
        get => _servings;
        set => this.RaiseAndSetIfChanged(ref _servings, value);
    }

    public string Favorite
    {
        get => _favorite;
        set => this.RaiseAndSetIfChanged(ref _favorite, value);
    }

    public string OwnerUsername
    {
        get => _ownerUsername;
        set => this.RaiseAndSetIfChanged(ref _ownerUsername, value);
    }

    public ReactiveCommand<Unit, Unit> Return { get; }
    public ReactiveCommand<Unit, List<Recipe>> SearchCommand { get; }

    public SearchRecipeViewModel()
    {
        Return = ReactiveCommand.Create(() =>
        {
            // Implementation for Return command
        });

        SearchCommand = ReactiveCommand.CreateFromTask(ExecuteSearch);
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
            var favorite = int.TryParse(Favorite, out var fav) ? fav : (int?)null;

            bool hasSearchCriteria = !string.IsNullOrEmpty(SearchKeyword) || tags.Any() || timeConstraint.HasValue || rating.HasValue || servings.HasValue || favorite.HasValue || !string.IsNullOrEmpty(OwnerUsername);

            if (!hasSearchCriteria)
            {
                NotificationMessage = "Please provide at least one search criterion.";
                return new List<Recipe>();
            }

            var searchedRecipes = Search.SearchRecipes(
                keyword: SearchKeyword,
                tags: tags,
                time: timeConstraint,
                rating: rating,
                servings: servings,
                favourite: favorite,
                ownerUsername: OwnerUsername
            );

            if (searchedRecipes == null || !searchedRecipes.Any())
            {
                NotificationMessage = "No recipes found matching the criteria.";
            }
            else
            {
                SearchedRecipes = searchedRecipes;
                NotificationMessage = $"{searchedRecipes.Count} recipes found.";
            }

            // SearchedRecipes = searchedRecipes;
        }
        catch (Exception ex)
        {
            NotificationMessage = $"Error during search: {ex.Message}";
        }

        return SearchedRecipes;
    }
}


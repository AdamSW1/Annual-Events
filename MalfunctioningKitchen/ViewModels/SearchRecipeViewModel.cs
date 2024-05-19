using System.Reactive;
using ReactiveUI;
using BusinessLayer;
using RecipeInfo;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
namespace MalfunctioningKitchen.ViewModels
{
    public class SearchRecipeViewModel : ViewModelBase
    {
        private string _notificationMessage;

        public string NotificationMessage
        {
            get => _notificationMessage;
            set => this.RaiseAndSetIfChanged(ref _notificationMessage, value);
        }
        private string _searchKeyword;
        private List<RecipeTag> _tagCriteria;
        private int _timeConstraint;
        private int _rating;
        private int _servings;
        private int _favorite;
        private string _ownerUsername;

        private List<Recipe> _searchedRecipes;

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
        public List<RecipeTag> TagCriteria
        {
            get => _tagCriteria;
            set => this.RaiseAndSetIfChanged(ref _tagCriteria, value);
        }

        public int TimeConstraint
        {
            get => _timeConstraint;
            set => this.RaiseAndSetIfChanged(ref _timeConstraint, value);
        }

        public int Rating
        {
            get => _rating;
            set => this.RaiseAndSetIfChanged(ref _rating, value);
        }

        public int Servings
        {
            get => _servings;
            set => this.RaiseAndSetIfChanged(ref _servings, value);
        }

        public int Favorite
        {
            get => _favorite;
            set => this.RaiseAndSetIfChanged(ref _favorite, value);
        }

        public string OwnerUsername
        {
            get => _ownerUsername;
            set => this.RaiseAndSetIfChanged(ref _ownerUsername, value);
        }

        // Other properties omitted for brevity
        public ReactiveCommand<Unit, Unit> Return{get;}
        public ReactiveCommand<Unit, List<Recipe>> SearchByKeywordCommand { get; }
        public ReactiveCommand<Unit, List<Recipe>> SearchByTagCommand { get; }
        public ReactiveCommand<Unit, List<Recipe>> SearchByTimeCommand { get; }
        public ReactiveCommand<Unit, List<Recipe>> SearchByRatingCommand { get; }
        public ReactiveCommand<Unit, List<Recipe>> SearchByServingsCommand { get; }
        public ReactiveCommand<Unit, List<Recipe>> SearchByFavoritesCommand { get; }
        public ReactiveCommand<Unit, List<Recipe>> SearchByOwnerCommand { get; }
        public ReactiveCommand<Unit, List<Recipe>> ShowAllRecipesCommand { get; }

        public SearchRecipeViewModel()
        {
            Return = ReactiveCommand.Create(() =>
            {

            });
            SearchByKeywordCommand = ReactiveCommand.CreateFromTask(GetRecipesByKeyword);
            SearchByTagCommand = ReactiveCommand.CreateFromTask(GetRecipesByTag);
            SearchByTimeCommand = ReactiveCommand.CreateFromTask(GetRecipesByTime);
            SearchByRatingCommand = ReactiveCommand.CreateFromTask(GetRecipesByRating);
            SearchByServingsCommand = ReactiveCommand.CreateFromTask(GetRecipesByServings);
            SearchByFavoritesCommand = ReactiveCommand.CreateFromTask(GetRecipesInFavorites);
            SearchByOwnerCommand = ReactiveCommand.CreateFromTask(GetRecipesByOwner);
            // ShowAllRecipesCommand = ReactiveCommand.CreateFromTask(GetAllRecipes);
        }

        private async Task<List<Recipe>> GetRecipesByKeyword()
        {
            List<Recipe> searchResult = Search.SearchRecipesByKeyword(SearchKeyword, Search.getRecipes());
            SearchedRecipes = searchResult;
            if (searchResult != null && searchResult.Count > 0)
            {
                // Search successful, update SearchedRecipes with the search result
                SearchedRecipes = searchResult;
                // Print a success message
                NotificationMessage = "Search successful!";
            }
            else
            {
                // Search failed, clear SearchedRecipes
                SearchedRecipes.Clear();
                // Print a failure message
                NotificationMessage = "No recipes found matching the keyword.";
            }
            return SearchedRecipes;
        }

        private async Task<List<Recipe>> GetRecipesByTag()
        {
            return await Task.FromResult(Search.SearchRecipesByTags(TagCriteria, Search.getRecipes()));
        }

        private async Task<List<Recipe>> GetRecipesByTime()
        {
            return await Task.FromResult(Search.SearchRecipesByTimeConstraint(TimeConstraint, Search.getRecipes()));
        }

        private async Task<List<Recipe>> GetRecipesByRating()
        {
            return await Task.FromResult(Search.SearchRecipesByRating(Rating, Search.getRecipes()));
        }

        private async Task<List<Recipe>> GetRecipesByServings()
        {
            return await Task.FromResult(Search.SearchRecipesByServings(Servings, Search.getRecipes()));
        }

        private async Task<List<Recipe>> GetRecipesInFavorites()
        {
            return await Task.FromResult(Search.SearchRecipesInFavorites(Favorite, Search.getRecipes()));
        }

        private async Task<List<Recipe>> GetRecipesByOwner()
        {
            return await Task.FromResult(Search.SearchRecipesByOwnerUsername(OwnerUsername, Search.getRecipes()));
        }

        // private async Task<List<Recipe>> GetAllRecipes()
        // {
        //     return await Task.FromResult(Search.getRecipes());
        // }
        
    }
}

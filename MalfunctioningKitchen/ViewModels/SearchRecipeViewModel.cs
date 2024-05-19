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
        public ReactiveCommand<string, List<Recipe>> SearchByKeywordCommand { get; }
        public ReactiveCommand<string, List<Recipe>> SearchByTagCommand { get; }
        public ReactiveCommand<string, List<Recipe>> SearchByTimeCommand { get; }
        public ReactiveCommand<string, List<Recipe>> SearchByRatingCommand { get; }
        public ReactiveCommand<string, List<Recipe>> SearchByServingsCommand { get; }
        public ReactiveCommand<string, List<Recipe>> SearchByFavoritesCommand { get; }
        public ReactiveCommand<string, List<Recipe>> SearchByOwnerCommand { get; }

        public SearchRecipeViewModel()
        {
            Return = ReactiveCommand.Create(() =>
            {

            });
            SearchByKeywordCommand = ReactiveCommand.CreateFromTask<string, List<Recipe>>(GetRecipesByKeyword);
            SearchByTagCommand = ReactiveCommand.CreateFromTask<string, List<Recipe>>(GetRecipesByTag);
            SearchByTimeCommand = ReactiveCommand.CreateFromTask<string, List<Recipe>>(GetRecipesByTime);
            SearchByRatingCommand = ReactiveCommand.CreateFromTask<string, List<Recipe>>(GetRecipesByRating);
            SearchByServingsCommand = ReactiveCommand.CreateFromTask<string, List<Recipe>>(GetRecipesByServings);
            SearchByFavoritesCommand = ReactiveCommand.CreateFromTask<string, List<Recipe>>(GetRecipesInFavorites);
            SearchByOwnerCommand = ReactiveCommand.CreateFromTask<string, List<Recipe>>(GetRecipesByOwner);

            // ShowAllRecipesCommand = ReactiveCommand.CreateFromTask(GetAllRecipes);
        }

        private async Task<List<Recipe>> GetRecipesByKeyword(string keyword)
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

        private async Task<List<Recipe>> GetRecipesByTag(string keyword)
        {
            // List<Recipe> searchResult = Search.SearchRecipesByTags(SearchKeyword, Search.getRecipes());
            // if (searchResult != null && searchResult.Count > 0)
            // {
            //     SearchedRecipes = searchResult;
            //     NotificationMessage = "Search successful!";
            // }
            // else
            // {
            //     SearchedRecipes.Clear();
            //     NotificationMessage = "No recipes found matching the tag.";
            // }
            // return SearchedRecipes;
            return null;
        }

        private async Task<List<Recipe>> GetRecipesByTime(string keyword)
        {
            if (int.TryParse(SearchKeyword, out int time))
            {
                List<Recipe> searchResult = Search.SearchRecipesByTimeConstraint(time, Search.getRecipes());
                if (searchResult != null && searchResult.Count > 0)
                {
                    SearchedRecipes = searchResult;
                    NotificationMessage = "Search successful!";
                }
                else
                {
                    SearchedRecipes.Clear();
                    NotificationMessage = "No recipes found matching the Servings.";
                }
            }
            else
            {
                SearchedRecipes.Clear();
                NotificationMessage = "No recipes found matching the time constraint.";
            }
            return SearchedRecipes;
        }

        private async Task<List<Recipe>> GetRecipesByRating(string keyword)
        {
            if (int.TryParse(SearchKeyword, out int ratings))
            {
                List<Recipe> searchResult = Search.SearchRecipesByRating(ratings, Search.getRecipes());
                if (searchResult != null && searchResult.Count > 0)
                {
                    SearchedRecipes = searchResult;
                    NotificationMessage = "Search successful!";
                }
                else
                {
                    SearchedRecipes.Clear();
                    NotificationMessage = "No recipes found matching the Servings.";
                }
            }
            else
            {
                SearchedRecipes.Clear();
                NotificationMessage = "No recipes found matching the Rating.";
            }
            return SearchedRecipes;
        }

        private async Task<List<Recipe>> GetRecipesByServings(string keyword)
        {
            if (int.TryParse(SearchKeyword, out int servings))
            {
                List<Recipe> searchResult = Search.SearchRecipesByServings(servings, Search.getRecipes());
                if (searchResult != null && searchResult.Count > 0)
                {
                    SearchedRecipes = searchResult;
                    NotificationMessage = "Search successful!";
                }
                else
                {
                    SearchedRecipes.Clear();
                    NotificationMessage = "No recipes found matching the Servings.";
                }
            }
            else
            {
                NotificationMessage = "Invalid input for servings.";
            }

            return SearchedRecipes;
        }


        private async Task<List<Recipe>> GetRecipesInFavorites(string keyword)
        {
            // Assuming SearchKeyword represents the favorite status as a string
            if (int.TryParse(SearchKeyword, out int favoriteStatus))
            {
                List<Recipe> searchResult = Search.SearchRecipesInFavorites(favoriteStatus, Search.getRecipes());
                if (searchResult != null && searchResult.Count > 0)
                {
                    SearchedRecipes = searchResult;
                    NotificationMessage = "Search successful!";
                }
                else
                {
                    SearchedRecipes.Clear();
                    NotificationMessage = "No recipes found.";
                }
            }
            else
            {
                NotificationMessage = "Invalid input for favorite status.";
            }

            return SearchedRecipes;
        }


        private async Task<List<Recipe>> GetRecipesByOwner(string keyword)
        {
            List<Recipe> searchResult = Search.SearchRecipesByOwnerUsername(SearchKeyword, Search.getRecipes());
            if(searchResult != null && searchResult.Count > 0)
            {
                SearchedRecipes = searchResult;
                NotificationMessage = "Search successful!";
            }
            else
            {
                SearchedRecipes.Clear();
                NotificationMessage = "No recipes found matching this Owner.";
            }
            return SearchedRecipes;
        }
    }
}

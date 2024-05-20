using System.Reactive;
using ReactiveUI;
using BusinessLayer;
using RecipeInfo;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MalfunctioningKitchen.ViewModels
{
    public class SearchRecipeViewModel : ViewModelBase
    {
        private string _notificationMessage;
        private string _searchKeyword;
        private List<string> _tagCriteria;
        private int _timeConstraint;
        private int _rating;
        private int _servings;
        private int _favorite;
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

        // public List<string> TagCriteria
        // {
        //     get => _tagCriteria;
        //     set => this.RaiseAndSetIfChanged(ref _tagCriteria, value);
        // }

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

        public ReactiveCommand<Unit, Unit> Return { get; }
        public ReactiveCommand<Unit, List<Recipe>> SearchCommand { get; }

        public SearchRecipeViewModel()
        {
            Return = ReactiveCommand.Create(() =>
            {

            });

            SearchCommand = ReactiveCommand.CreateFromTask(ExecuteSearch);
        }

        private async Task<List<Recipe>> ExecuteSearch()
        {
            var searchedRecipes = Search.SearchRecipes(
                keyword: SearchKeyword,
                // tags: TagCriteria,
                time: TimeConstraint,
                rating: Rating,
                servings: Servings,
                favourite: Favorite,
                ownerUsername: OwnerUsername
            );

            SearchedRecipes = searchedRecipes;

            return SearchedRecipes;
        }
    }
}

using System;
using ReactiveUI;
using System.Reactive;
using System.Windows.Input;
using DataLayer;
using BusinessLayer;
using System.Diagnostics;
using RecipeInfo;
namespace MalfunctioningKitchen.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _contentViewModel;

        public ViewModelBase ContentViewModel
        {
            get => _contentViewModel;
            private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
        }
        public delegate void previous();
        public delegate void previousRecipe(Recipe recipe, previous prev);

        public ViewModelBase previousPage = new();

        public ICommand NavigateToSearchRecipeCommand { get; }
        public ICommand NavigateToUpdateProfileCommand { get; }
        public ICommand NavigateToRecipeCommand { get; }
        public ICommand NavigateToAddRecipeCommand { get; }
        public ICommand Edit { get; }
        public MainWindowViewModel()
        {
            _contentViewModel = new WelcomeViewModel();
            NavigateToUpdateProfileCommand = ReactiveCommand.Create(NavigateToUpdateProfile);
            NavigateToSearchRecipeCommand = ReactiveCommand.Create(NavigateToSearchRecipe);
            previous nav = NavigateToHomePage;
            NavigateToRecipeCommand = ReactiveCommand.Create<Recipe>(recipe => NavigateToRecipe(recipe, nav));
        }

        public void NavigateToWelcome()
        {
            ContentViewModel = new WelcomeViewModel();
        }

        public void NavigateToRegister()
        {
            RegisterViewModel viewModel = new();

            viewModel.Register.Subscribe(user =>
            {
                if (user != null)
                {
                    NavigateToLogin();
                }
            });

            ContentViewModel = viewModel;
        }

        public void NavigateToLogin()
        {
            LoginViewModel viewModel = new();

            viewModel.Login.Subscribe(user =>
            {
                if (user != null)
                {
                    NavigateToHomePage();
                }
            });

            ContentViewModel = viewModel;
        }

        public void NavigateToHomePage()
        {
            HomePageViewModel viewModel = new();
            viewModel.NavigateToSearchRecipeCommand.Subscribe(_ => NavigateToSearchRecipe());
            viewModel.NavigateToUpdateProfileCommand.Subscribe(_ => NavigateToUpdateProfile());
            viewModel.Logout.Subscribe(_ => NavigateToWelcome());
            previous nav = NavigateToHomePage;
            viewModel.ViewRecipeCommand.Subscribe(recipe => NavigateToRecipe(recipe, nav));
            viewModel.NavigateToAddRecipeCommand.Subscribe(_ => NavigateToAddRecipe(null,"Create"));
            ContentViewModel = viewModel;
        }



        public void NavigateToLoggedIn()
        {
            LoggedInViewModel viewModel = new();

            viewModel.Logout.Subscribe(_ => NavigateToWelcome());

            viewModel.NavigateToSearchRecipeCommand.Subscribe(_ => NavigateToSearchRecipe());
            viewModel.NavigateToUpdateProfileCommand.Subscribe(_ => NavigateToUpdateProfile());

            ContentViewModel = viewModel;
        }

        public void NavigateToSearchRecipe()
        {
            SearchRecipeViewModel viewModel = new();
            viewModel.Return.Subscribe(_ => NavigateToHomePage());
            previous nav = NavigateToPreviousSearchRecipe;
            viewModel.ViewRecipeCommand.Subscribe(recipe => NavigateToRecipe(recipe, nav));
            ContentViewModel = viewModel;
            previousPage = ContentViewModel;
        }

        public void NavigateToPreviousSearchRecipe(){
            SearchRecipeViewModel viewModel = (SearchRecipeViewModel)previousPage;
            viewModel.Return.Subscribe(_ => NavigateToHomePage());
            previous nav = NavigateToPreviousSearchRecipe;
            viewModel.ViewRecipeCommand.Subscribe(recipe => NavigateToRecipe(recipe, nav));
            ContentViewModel = viewModel;
        }

        public void NavigateToUpdateProfile()
        {
            var currentUser = AuthenticationManager.Instance.CurrentUser;
            UpdateProfileViewModel viewModel = new(currentUser);
            ContentViewModel = viewModel;
            viewModel.NavigateToSearchRecipeCommand.Subscribe(_ => NavigateToSearchRecipe());
            viewModel.Return.Subscribe(_ => NavigateToHomePage());
        }


        public void NavigateToRecipe(Recipe recipe, previous previous)
        {
            RecipeViewModel viewModel = new(recipe);
            
            viewModel.NavigateToHomePageCommand.Subscribe(_ => previous());
            viewModel.Logout.Subscribe(_ => NavigateToWelcome());
            viewModel.Edit.Subscribe(_ => NavigateToAddRecipe(recipe,"Edit"));
            viewModel.NavigateToAddReviewCommand.Subscribe(_ => NavigateToAddReview(recipe, previous));
            ContentViewModel = viewModel;
        }

        public void NavigateToAddReview(Recipe recipe, previous previous) 
        {
            AddReviewViewModel viewModel = new(recipe);
            viewModel.Back.Subscribe(_ => NavigateToRecipe(recipe,previous));
            ContentViewModel = viewModel;
        }
        
        public void NavigateToAddRecipe(Recipe recipe,string typeParentPage)
        {
            AddRecipeViewModel viewModel = new AddRecipeViewModel(recipe,typeParentPage);
            viewModel.NavigateToHomePageCommand.Subscribe(_ => NavigateToHomePage());
            viewModel.Logout.Subscribe(_ => NavigateToWelcome());
            viewModel.CreateRecipe.Subscribe(_ => NavigateToHomePage());    
            ContentViewModel = viewModel;
        }
    }
}

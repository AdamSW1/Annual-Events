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

        public ICommand NavigateToSearchRecipeCommand { get; }
        public ICommand NavigateToUpdateProfileCommand { get; }
        public ICommand NavigateToRecipeCommand { get; }
        public MainWindowViewModel()
        {
            _contentViewModel = new WelcomeViewModel();
            NavigateToUpdateProfileCommand = ReactiveCommand.Create(NavigateToUpdateProfile);
            NavigateToSearchRecipeCommand = ReactiveCommand.Create(NavigateToSearchRecipe);
            NavigateToRecipeCommand = ReactiveCommand.Create<Recipe>(recipe => NavigateToRecipe(recipe));
        }

        public void NavigateToWelcome()
        {
            ContentViewModel = new WelcomeViewModel();
        }

        public void NavigateToRegister()
        {
            RegisterViewModel viewModel = new RegisterViewModel();

            viewModel.Register.Subscribe(user =>
            {
                if (user != null)
                {
                    NavigateToWelcome();
                }
            });

            ContentViewModel = viewModel;
        }

        public void NavigateToLogin()
        {
            LoginViewModel viewModel = new LoginViewModel();

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
            HomePageViewModel viewModel = new HomePageViewModel();
            viewModel.NavigateToSearchRecipeCommand.Subscribe(_ => NavigateToSearchRecipe());
            viewModel.NavigateToUpdateProfileCommand.Subscribe(_ => NavigateToUpdateProfile());
            viewModel.Logout.Subscribe(_ => NavigateToWelcome());
            // viewModel.ViewRecipeCommand.Subscribe(recipe => Debug.Write(recipe.DisplayRecipeInfo()));
            viewModel.ViewRecipeCommand.Subscribe(recipe => NavigateToRecipe(recipe));
            ContentViewModel = viewModel;
        }

        

        public void NavigateToLoggedIn()
        {
            LoggedInViewModel viewModel = new LoggedInViewModel();

            viewModel.Logout.Subscribe(_ => NavigateToWelcome());

            viewModel.NavigateToSearchRecipeCommand.Subscribe(_ => NavigateToSearchRecipe());
            viewModel.NavigateToUpdateProfileCommand.Subscribe(_ => NavigateToUpdateProfile());

            ContentViewModel = viewModel;
        }

        public void NavigateToSearchRecipe()
        {
            SearchRecipeViewModel viewModel = new SearchRecipeViewModel();
            viewModel.Return.Subscribe(_ => NavigateToHomePage());
            ContentViewModel = viewModel;
        }


        public void NavigateToUpdateProfile()
        {
            var currentUser = AuthenticationManager.Instance.CurrentUser;
            UpdateProfileViewModel viewModel = new UpdateProfileViewModel(currentUser);
            ContentViewModel = viewModel;
            viewModel.NavigateToSearchRecipeCommand.Subscribe(_ => NavigateToSearchRecipe());
            viewModel.Return.Subscribe(_ => NavigateToHomePage());
            
        }

        public void NavigateToRecipe(Recipe recipe)
        {
            RecipeViewModel viewModel = new RecipeViewModel(recipe);
            viewModel.NavigateToHomePageCommand.Subscribe(_ => NavigateToHomePage());
            viewModel.Logout.Subscribe(_ => NavigateToWelcome());
            ContentViewModel = viewModel;
        }
        
    }
}

using System;
using ReactiveUI;
using System.Reactive;
using System.Windows.Input;

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

        public ICommand NavigateToNewPageCommand { get; }
        public MainWindowViewModel()
        {
            _contentViewModel = new WelcomeViewModel();
            NavigateToNewPageCommand = ReactiveCommand.Create(NavigateToNewPage);
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
                    NavigateToLoggedIn();
                }
            });

            ContentViewModel = viewModel;
        }

        public void NavigateToLoggedIn()
        {
            LoggedInViewModel viewModel = new LoggedInViewModel();

            viewModel.Logout.Subscribe(_ => NavigateToWelcome());

            viewModel.NavigateToNewPageCommand.Subscribe(_ => NavigateToNewPage());

            ContentViewModel = viewModel;
        }

        private void NavigateToNewPage()
        {
            ContentViewModel = new SearchRecipeViewModel();
        }
    }
}

using System.Reactive;
using DataLayer;
using BusinessLayer;
using ReactiveUI;

namespace MalfunctioningKitchen.ViewModels;

public class LoggedInViewModel : ViewModelBase
{
    public string Greeting { get; }

    public ReactiveCommand<Unit, Unit> Logout { get; }
    public ReactiveCommand<Unit, Unit> NavigateToSearchRecipeCommand { get; }

    public ReactiveCommand<Unit, Unit> NavigateToUpdateProfileCommand { get; }

    public LoggedInViewModel()
    {
        Logout = ReactiveCommand.Create(() =>
        {
            AuthenticationManager.Instance.Logout();
        });

        Greeting = $"Hello {AuthenticationManager.Instance.CurrentUser.Username}";

        NavigateToSearchRecipeCommand = ReactiveCommand.Create(() =>
        {

        });

        NavigateToUpdateProfileCommand = ReactiveCommand.Create
        (() =>
        {

        });
    }
}
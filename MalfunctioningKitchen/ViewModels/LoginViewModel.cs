using System;
using System.Reactive;


using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ReactiveUI;
using BusinessLayer;

namespace MalfunctioningKitchen.ViewModels;

public class LoginViewModel : ViewModelBase{

    private string? _username;
    public string? Username
    {
        get => _username;
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }

    private string? _password;
    public string? Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }

    private string? _errorMessage;
    public string? ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage,value);
    }

    public ReactiveCommand<Unit, Annual_Events_User?> Login {get;}

    public LoginViewModel(){
        IObservable<bool> areBothFilledIn = this.WhenAnyValue(
            LoginViewModel => LoginViewModel.Username,
            LoginViewModel => LoginViewModel.Password,
            (username, password) => 
                !(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        );

        Login = ReactiveCommand.Create(() =>
        {
            Annual_Events_User? loggedIn = null;

            if(AuthenticationManager.Instance.Login(Username!, Password!)){
                loggedIn = AuthenticationManager.Instance.CurrentUser;
            }

            if(loggedIn == null)
            {
                ErrorMessage = "Invalid username or password";
            }

            return loggedIn;

        }, areBothFilledIn);
    }
}
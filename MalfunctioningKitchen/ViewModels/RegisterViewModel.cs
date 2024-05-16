using System;
using System.Reactive;
using BusinessLayer;
using ReactiveUI;

namespace MalfunctioningKitchen.ViewModels;

public class RegisterViewModel : ViewModelBase
{
  /// <summary>
  /// This is the user we are in the process of creating, its prperties are also
  /// used to perform validation.
  /// </summary>
  private Annual_Events_User UserToRegister { get; } = new Annual_Events_User();

  public string? Name
  {
    get => UserToRegister.Username;
    set =>
      // if this throws an exception (i.e. value is invalid), the exception's
      // message is shown in the GUI.
      UserToRegister.Username = value;
  }

  private string? _password;
  public string? Password
  {
    get => _password;
    set
    {
      if (value == null)
      {
        throw new ArgumentNullException(nameof(Password));
      }
      else if (value.Length < 5
        || value.Length > 100)
      {
        throw new ArgumentException($"Must be at least " +
          $"5 and at most " +
          $"100 characters long",
          nameof(Password));
      }

      this.RaiseAndSetIfChanged(ref _password, value);
    }
  }

  private string? _confirmPassword;
  public string? ConfirmPassword
  {
    get => _confirmPassword;
    set
    {
      if (!string.Equals(value, Password))
      {
        throw new ArgumentException("Must match the first password",
          nameof(ConfirmPassword));
      }

      this.RaiseAndSetIfChanged(ref _confirmPassword, value);
    }
  }

  private string? _errorMessage;
  public string? ErrorMessage
  {
    get => _errorMessage;
    set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
  }

  public ReactiveCommand<Unit, Annual_Events_User?> Register { get; }

  public RegisterViewModel()
  {
    Register = ReactiveCommand.Create(() =>
    {
      try
      {
        AuthenticationManager.Instance.AddUser(UserToRegister);
        ErrorMessage = "";
      }
      catch (Exception exc)
      when (exc is ArgumentException || exc is NullReferenceException)
      {
        ErrorMessage = exc.Message;
        return null;
      }

      return UserToRegister;
    });
  }
}

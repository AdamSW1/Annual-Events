using System;
using System.Reactive;
using ReactiveUI;
using System.Windows;
using DataLayer;
using BusinessLayer;
namespace MalfunctioningKitchen.ViewModels
{
    public class UpdateProfileViewModel : ViewModelBase
    {
        private string _username = AuthenticationManager.Instance.CurrentUser.Username!;
        private string _password;
        private string _description = AuthenticationManager.Instance.CurrentUser.Description!;
        private int _age = AuthenticationManager.Instance.CurrentUser.Age;

        private Annual_Events_User _user;
        public Annual_Events_User User
        {
            get => _user;
            set => this.RaiseAndSetIfChanged(ref _user, value);
        }

        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public string Description
        {
            get => _description;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }

        public int Age
        {
            get => _age;
            set => this.RaiseAndSetIfChanged(ref _age, value);
        }

        private string _notificationMessage;
        public string NotificationMessage
        {
            get => _notificationMessage;
            set => this.RaiseAndSetIfChanged(ref _notificationMessage, value);
        }

        public ReactiveCommand<Unit, Unit> NavigateToSearchRecipeCommand { get; }
        public ReactiveCommand<Unit,Unit> Logout{get;}

        public UpdateProfileViewModel(Annual_Events_User user)
        {
            User = user;
            Username = User.Username;
            Description = User.Description;
            Age = user.Age;
            UpdateProfileCommand = ReactiveCommand.Create(() => UpdateProfile(user));

            NavigateToSearchRecipeCommand = ReactiveCommand.Create(() =>
            {
            });
            Logout = ReactiveCommand.Create(() =>
            {
                AuthenticationManager.Instance.Logout();
            });
        }

        public ReactiveCommand<Unit, Unit> UpdateProfileCommand { get; }

        private void UpdateProfile(Annual_Events_User user)
        {
            try
            {
                var profile = new Profile();
                User = user;
                profile.UpdateProfile(User, Username, Password, Description, Age);
                NotificationMessage = "Profile updated successfully!";
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Failed to update profile: {ex}";
            }
        }
    }
}

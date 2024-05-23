using System;
using System.Reactive;
using ReactiveUI;
using System.Windows;
using DataLayer;
using BusinessLayer;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Platform.Storage;
using System.IO;
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

        public Profile profile = new Profile();


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
        private static readonly Bitmap PLACEHOLDER =
            // This shows an example of loading an image from the assets directory.
            new(AssetLoader.Open(new Uri("avares://MalfunctioningKitchen/Assets/Default_pfp.jpg")));
        private Bitmap _imageDisplayed = PLACEHOLDER;
        public Bitmap ImageDisplayed
        {
            get => _imageDisplayed;
            set => this.RaiseAndSetIfChanged(ref _imageDisplayed, value);
        }

        public ReactiveCommand<Unit, Unit> NavigateToSearchRecipeCommand { get; }
        public ReactiveCommand<Unit,Unit> Return{get;}
        public ReactiveCommand<Unit, Unit> DeleteUserCommand { get; }
        public ReactiveCommand<Unit, Unit> UpdateProfileCommand { get; }
        public ReactiveCommand<Window, Task<byte[]>> SelectImage { get; }
        public ReactiveCommand<Unit, Unit> ClearImage { get; }


        public UpdateProfileViewModel(Annual_Events_User user)
        {
            User = user;
            Username = User.Username;
            Description = User.Description;
            Age = user.Age;
            UpdateProfileCommand = ReactiveCommand.Create(() => UpdateProfile(user));

            NavigateToSearchRecipeCommand = ReactiveCommand.Create(() => { });
            Return = ReactiveCommand.Create(() => { });
            DeleteUserCommand = ReactiveCommand.Create(() => DeleteUser());

            if (user.ProfilePicture != null && user.ProfilePicture.Length != 0 ){
                using var ms = new MemoryStream(user.ProfilePicture);
                ImageDisplayed = new Bitmap(ms);
            }

            SelectImage = ReactiveCommand.Create(async (Window window) =>
            {
                // https://docs.avaloniaui.net/docs/basics/user-interface/file-dialogs
                var files = await window.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
                {
                    Title = "Open Image File",
                    AllowMultiple = false,
                    // https://docs.avaloniaui.net/docs/concepts/services/storage-provider/file-picker-options
                    FileTypeFilter = new[] { FilePickerFileTypes.ImageAll }
                });

                // https://stackoverflow.com/a/221941
                using var memoryStream = new MemoryStream();

                if (files.Count >= 1)
                {
                    // Open reading stream from the first file.
                    await using var fileStream = await files[0].OpenReadAsync();
                    await fileStream.CopyToAsync(memoryStream);
                }
                // Reads all the content of file as an image.
                byte[] image = memoryStream.ToArray();

                // Here I could save the image into a user object:
                // myUser.ProfilePicture = image;
                profile.UpdatePFP(image);
                return image;
            });

            SelectImage.Subscribe(async (readingImageTask) =>
            {
                byte[] imageData = await readingImageTask;

                // For the project, there is no need to have this async lambda, simply
                // create a Bitmap using the byte array saved in DB and set the property
                // (`ImageDisplayed` in this case) that is bound to the Image in the view.
                ImageDisplayed = new(new MemoryStream(imageData));
            });
            ClearImage = ReactiveCommand.Create(() =>
            {
                ImageDisplayed = PLACEHOLDER;
                profile.UpdatePFP(new byte[0]);
            });
        }

        private void UpdateProfile(Annual_Events_User user)
        {
            try
            {
                User = user;
                profile.UpdateProfile(User, Username, Password, Description, Age);
                NotificationMessage = "Profile updated successfully!";
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Failed to update profile: {ex}";
            }
        }

        private void DeleteUser()
        {
            try {
                AnnualEventsUserServices.Instance.DeleteUser(AuthenticationManager.Instance.CurrentUser);
                AuthenticationManager.Instance.Logout();
            }
            catch (Exception ex)
            {
                NotificationMessage = $"Failed to delete your account: {ex}";
            }
        }
    }
}

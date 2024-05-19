using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MalfunctioningKitchen.ViewModels;

namespace MalfunctioningKitchen.Views
{
    public partial class UpdateProfileView : UserControl
    {
        public UpdateProfileView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

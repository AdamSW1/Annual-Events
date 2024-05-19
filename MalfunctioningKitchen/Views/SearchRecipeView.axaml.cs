using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MalfunctioningKitchen.ViewModels;

namespace MalfunctioningKitchen.Views
{
    public partial class SearchRecipeView : UserControl
    {
        public SearchRecipeView()
        {
            InitializeComponent();
            DataContext = new SearchRecipeViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

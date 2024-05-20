using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MalfunctioningKitchen.ViewModels;

namespace MalfunctioningKitchen.Views
{
    public partial class SearchRecipeView : UserControl
    {
        private SearchRecipeViewModel ViewModel {get;set;} = new();
        public SearchRecipeView()
        {
            DataContext = ViewModel;
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listSelectedItems = ((ListBox)sender).SelectedItems;
            ViewModel.SelectedTags = listSelectedItems.Cast<String>().ToList();
        }

    }
}

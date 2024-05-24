using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BusinessLayer;
using DataLayer;
using RecipeInfo;
using MalfunctioningKitchen.ViewModels;

namespace MalfunctioningKitchen.Views;

public partial class AddRecipeView : UserControl
{
    private AddRecipeViewModel ViewModel {get;set;} = new(null!,null!);
    public AddRecipeView()
    {
        InitializeComponent();
        DataContext = ViewModel;
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var listSelectedItems = ((ListBox)sender).SelectedItems;
        ViewModel.SelectedTags = listSelectedItems!.Cast<string>().ToList();
    }
}
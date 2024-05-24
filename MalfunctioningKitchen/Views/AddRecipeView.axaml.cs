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
<<<<<<< HEAD
    private AddRecipeViewModel ViewModel {get;set;} = new AddRecipeViewModel(new Recipe(), "TypeParentPage");
=======
    private AddRecipeViewModel ViewModel {get;set;} = new(null!,null!);
>>>>>>> 3c56fc70222f3d41a7e8d61f175a8447cef80261
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
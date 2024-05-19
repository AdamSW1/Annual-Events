using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MalfunctioningKitchen.ViewModels;

namespace MalfunctioningKitchen.Views;

public partial class HomePageView : UserControl
{
    public HomePageView()
    {
        InitializeComponent();
        DataContext = new HomePageViewModel();
    }
}
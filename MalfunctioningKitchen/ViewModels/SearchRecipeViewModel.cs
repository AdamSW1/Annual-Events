using System.Reactive;
using DataLayer;
using BusinessLayer;
using ReactiveUI;
namespace MalfunctioningKitchen.ViewModels;

public class SearchRecipeViewModel : ViewModelBase
{
    private string _title;

    // This is an example for now, add more properties if this works.
    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    public SearchRecipeViewModel()
    {
        Title = "Search Recipe";
    }
}


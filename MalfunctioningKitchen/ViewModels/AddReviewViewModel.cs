using System.Reactive;
using ReactiveUI;
using BusinessLayer;
using RecipeInfo;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using DataLayer;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System;
using System.Linq;

namespace MalfunctioningKitchen.ViewModels;

public class AddReviewViewModel : ViewModelBase
{
    private Recipe _recipe;

    public Recipe Recipe 
    {
        get => _recipe;
        set => this.RaiseAndSetIfChanged(ref _recipe, value);
    }   

    private int _score;
    public int Score
    {
        get => _score;
        set => this.RaiseAndSetIfChanged(ref _score, value);
    }

    private string _reviewerUsername;
    public string ReviewerUsername
    {
        get => _reviewerUsername;
        set => this.RaiseAndSetIfChanged(ref _reviewerUsername, value);
    }

    private string _reviewText;
    public string ReviewText 
    { 
        get => _reviewText;
        set => this.RaiseAndSetIfChanged(ref _reviewText, value);
    }

    private string? _errorMessage;
    public string? ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    public ReactiveCommand<Unit, Unit> Logout { get; }
    public ReactiveCommand<Unit, Unit> NavigateToHomePageCommand { get; }

    public AddReviewViewModel() 
    {
        Logout = ReactiveCommand.Create(() =>
        {
            AuthenticationManager.Instance.Logout();
        });

        NavigateToHomePageCommand = ReactiveCommand.Create(() => { });
    }

}
using System;
using System.Collections.Generic;
using System.Text;
using RecipeInfo;
namespace BusinessLayer;

public class Review
{

    public int ReviewId { get; set; }
    private int _score;
    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }
    private string _reviewerUsername;
    public string ReviewerUsername
    {
        get { return _reviewerUsername; }
        set { _reviewerUsername = value; }
    }

    private string _reviewText;
    public string ReviewText { 
        get{ return _reviewText; } 
        set{ _reviewText = value;}
    }

    public Review(string reviewerUsername, string reviewText, int score)
    {
        _score = score;
        _reviewerUsername = reviewerUsername;
        _reviewText = reviewText;
    }

    public override string ToString()
    {
        var str = new StringBuilder();
        str.Append($"- review by ");
        str.Append($"{ReviewerUsername}");
        str.Append($"{ReviewText}.");
        str.Append($"{Score}/5");
        return str.ToString();
    }
}
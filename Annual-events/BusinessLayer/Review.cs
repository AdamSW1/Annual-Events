using System;
using System.Collections.Generic;
using RecipeInfo;
namespace BusinessLayer;

public class Review
{
    public int Score{ get; }
    public string ReviewerUsername { get; }
    public string ReviewText { get; }

    public Review(string reviewerUsername, string reviewText, int score)
    {
        Score = score;
        ReviewerUsername = reviewerUsername;
        ReviewText = reviewText;
    }

    public override string ToString()
    {
        return $"\t- review by {ReviewerUsername}: {ReviewText}. {Score}/5";
    }
}
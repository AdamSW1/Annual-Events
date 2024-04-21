using System;
using System.Collections.Generic;
using RecipeInfo;
namespace BusinessLayer;

public class Review
{
    public string ReviewerUsername { get; }
    public string ReviewText { get; }

    public Review(string reviewerUsername, string reviewText)
    {
        ReviewerUsername = reviewerUsername;
        ReviewText = reviewText;
    }
}
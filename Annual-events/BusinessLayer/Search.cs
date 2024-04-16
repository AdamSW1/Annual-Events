using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using RecipeInfo;
namespace BusinessLayer;

class Search
{
    private List<Recipe> Recipes;
    public Utils Utils { get; } = new Utils();
    public Search(List<Recipe> recipes)
    {
        this.Recipes = recipes;
    }

    public List<Recipe> getRecipes()
    { //gets the recipes from the database
        throw new NotImplementedException();
    }
    // Search recipes by keyword
    public List<Recipe> SearchRecipesByKeyword(string keyword)
    {
        string reg = Regex.Escape(keyword);
        List<Recipe> searched = new();
        foreach (Recipe recipe in Recipes)
        {
            foreach (Match match in Regex.Matches(recipe.Description, $"(?i){keyword}"))
            {
                if (match.Success)
                {
                    searched.Add(recipe);
                }
            }
        }
        return searched;
    }

    // Search recipes by tags
    public List<Recipe> SearchRecipesByTags(List<string> tags)
    {
        var searchedTags = Utils.ValidateTags(tags);
        List<Recipe> searched = new();
        foreach (Recipe recipe in Recipes)
        {
            foreach (RecipeTags tag in searchedTags)
            {
                if (recipe.Tags.Contains(tag))
                {
                    searched.Add(recipe);
                }
            }
        }
        return searched;

    }

    // Search recipes by time constraint
    public List<Recipe> SearchRecipesByTimeConstraint()
    {
        throw new NotImplementedException();
    }
    // Search recipes by rating
    public List<Recipe> SearchRecipesByRating(int rating)
    {
        throw new NotImplementedException();
    }
    // Search recipes by servings constraint
    public List<Recipe> SearchRecipesByServings(int servings)
    {
        throw new NotImplementedException();
    }
    // Search recipes in favorites
    public List<Recipe> SearchRecipesInFavorites()
    {
        throw new NotImplementedException();
    }
    // Search recipes by owner username
    public List<Recipe> SearchRecipesByOwnerUsername(string ownerUsername)
    {
        throw new NotImplementedException();
    }
}
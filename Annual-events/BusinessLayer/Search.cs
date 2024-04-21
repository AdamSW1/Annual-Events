using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using RecipeInfo;
namespace BusinessLayer;

class Search
{
    private List<Recipe> _recipes;
    public List<Recipe> Recipes
    {
        get { return _recipes; }
        set { _recipes = value; }
    }
    public Utils utils = new Utils();
    public Search(List<Recipe> recipes)
    {
        _recipes = recipes;
    }

    public List<Recipe> getRecipes()
    { //gets the recipes from the database
        throw new NotImplementedException();
    }
    // Search recipes by keyword
    public List<Recipe> SearchRecipesByKeyword(string keyword)
    {
        string escaped = Regex.Escape(keyword);
        var reg = new Regex(escaped, RegexOptions.IgnoreCase);
        var searched = Recipes.Where(recipe => reg.IsMatch(recipe.Name) || reg.IsMatch(recipe.Description) || reg.IsMatch(recipe.Preparation));
        return searched.ToList();
    }
    // Search recipes by tags
    public List<Recipe> SearchRecipesByTags(List<string> tags)
    {
        List<RecipeTags> searchedTags = Utils.ValidateTags(tags);
        var searched = Recipes.Where(recipe => recipe.Tags.Intersect(searchedTags).Any());
        return searched.ToList();
    }
    // Search recipes by time constraint
    public List<Recipe> SearchRecipesByTimeConstraint(int time)
    {
        var searched = Recipes.Where(recipe => recipe.CookingTime >= time -3 && recipe.CookingTime <= time + 3);
        return searched.ToList();
    }
    // Search recipes by rating
    public List<Recipe> SearchRecipesByRating(int rating)
    {
        var searched = Recipes.Where(recipe => recipe.Ratings == rating);
        return searched.ToList();
    }
    // Search recipes by servings constraint
    public List<Recipe> SearchRecipesByServings(int servings)
    {
        var searched = Recipes.Where(recipe => recipe.Servings == servings);
        return searched.ToList();
    }
    // Search recipes in favorites
    public List<Recipe> SearchRecipesInFavorites(int favourite)
    {
        var searched = Recipes.Where(recipe => recipe.Favourite == favourite);
        return searched.ToList();
    }
    // Search recipes by owner username
    public List<Recipe> SearchRecipesByOwnerUsername(string ownerUsername)
    {
        var searched = Recipes.Where(recipe => recipe.Owner.Username == ownerUsername);
        return searched.ToList();
    }
}
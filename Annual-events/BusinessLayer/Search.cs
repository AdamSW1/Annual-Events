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
            foreach (Match match in Regex.Matches(recipe.Name, $"(?i){keyword}"))
            {
                if (match.Success)
                {
                    searched.Add(recipe);
                    break;
                }
            }
        }
        return searched;
    }

    // Search recipes by tags
    public List<Recipe> SearchRecipesByTags(List<string> tags)
    {
        List<RecipeTags> searchedTags = utils.ValidateTags(tags);
        List<Recipe> searched = new();
        foreach (Recipe recipe in Recipes)
        {
            foreach (RecipeTags tag in searchedTags)
            {
                if (recipe.Tags.Contains(tag))
                {
                    searched.Add(recipe);
                    break;
                }
            }
        }
        return searched;

    }

    // Search recipes by time constraint
    public List<Recipe> SearchRecipesByTimeConstraint(int time)
    {
        List<Recipe> searched = new();
        foreach (Recipe recipe in Recipes)
        {
            if (recipe.CookingTime >= time -3 && recipe.CookingTime <= time + 3)
            {
                searched.Add(recipe);
            }
        }
        return searched;
    }
    // Search recipes by rating
    public List<Recipe> SearchRecipesByRating(int rating)
    {
        List<Recipe> searched = new();
        foreach (Recipe recipe in Recipes)
        {
            if (recipe.Ratings == rating)
            {
                searched.Add(recipe);
            }
        }
        return searched;
    }
    // Search recipes by servings constraint
    public List<Recipe> SearchRecipesByServings(int servings)
    {
        List<Recipe> searched = new();
        foreach (Recipe recipe in Recipes)
        {
            if (recipe.Servings == servings)
            {
                searched.Add(recipe);
            }
        }
        return searched;
    }
    // Search recipes in favorites
    public List<Recipe> SearchRecipesInFavorites(int favourite)
    {
        List<Recipe> searched = new();
        foreach (Recipe recipe in Recipes)
        {
            if (recipe.Favourite == favourite)
            {
                searched.Add(recipe);
            }
        }
        return searched;
    }
    // Search recipes by owner username
    public List<Recipe> SearchRecipesByOwnerUsername(string ownerUsername)
    {
        List<Recipe> searched = new();
        foreach (Recipe recipe in Recipes)
        {
            if (recipe.Owner.Username == ownerUsername)
            {
                searched.Add(recipe);
            }
        }
        return searched;
    }
}
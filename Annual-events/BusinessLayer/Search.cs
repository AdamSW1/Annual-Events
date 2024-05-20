using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using RecipeInfo;
using DataLayer;
namespace BusinessLayer;

public class Search
{   
    public static List<Recipe> getRecipes()
    { 
        using (var dbContext = new AnnualEventsContext())
        {
            return dbContext.Recipe.ToList();
        }
    }
    // Search recipes by keyword
    public static List<Recipe> SearchRecipesByKeyword(string keyword,List<Recipe> Recipes)
    {
        string escaped = Regex.Escape(keyword);
        var reg = new Regex(escaped, RegexOptions.IgnoreCase);
        var searched = Recipes.Where(recipe => reg.IsMatch(recipe.Name) || reg.IsMatch(recipe.Description) || recipe.Preparation.Any( x=> reg.IsMatch(x.Step)));
        return searched.ToList();
    }
    // Search recipes by tags
    public static List<Recipe>? SearchRecipesByTags(List<RecipeTag> tags,List<Recipe> Recipes)
    {
        if(!Utils.ValidateTags(tags)){
            return null;
        }
        var searched = Recipes.Where(recipe => recipe.Tags.Intersect(tags).Any());
        return searched.ToList();
    }
    // Search recipes by time constraint
    public static List<Recipe> SearchRecipesByTimeConstraint(int time,List<Recipe> Recipes)
    {
        var searched = Recipes.Where(recipe => recipe.CookingTime >= time -3 && recipe.CookingTime <= time + 3);
        return searched.ToList();
    }
    // Search recipes by rating
    public static List<Recipe> SearchRecipesByRating(int rating,List<Recipe> Recipes)
    {
        var searched = Recipes.Where(recipe => recipe.AverageScore == rating);
        return searched.ToList();
    }
    // Search recipes by servings constraint
    public static List<Recipe> SearchRecipesByServings(int servings,List<Recipe> Recipes)
    {
        var searched = Recipes.Where(recipe => recipe.Servings == servings);
        return searched.ToList();
    }
    // Search recipes in favorites
    public static List<Recipe> SearchRecipesInFavorites(int favourite,List<Recipe> Recipes)
    {
        var searched = Recipes.Where(recipe => recipe.Favourite == favourite);
        return searched.ToList();
    }
    // Search recipes by owner username
    public static List<Recipe> SearchRecipesByOwnerUsername(string ownerUsername,List<Recipe> Recipes)
    {
        var searched = Recipes.Where(recipe => recipe.Owner.Username == ownerUsername);
        return searched.ToList();
    }
}
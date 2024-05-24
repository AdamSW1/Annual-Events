using System.Text.RegularExpressions;
using System.Collections.Generic;
using RecipeInfo;
using DataLayer;
using System.Linq;

namespace BusinessLayer;

public class Search
{
    public static List<Recipe> GetRecipes()
    {
        using (var dbContext = new AnnualEventsContext())
        {
            var recipes = RecipeServices.Instance.GetRecipes();
            Console.WriteLine($"Retrieved {recipes.Count} recipes from the database.");
            return recipes;
        }
    }

    // Search recipes by keyword
    public static List<Recipe> SearchRecipesByKeyword(string keyword, List<Recipe> recipes)
    {
        string escaped = Regex.Escape(keyword);
        var reg = new Regex(escaped, RegexOptions.IgnoreCase);
        var searched = recipes.Where(recipe => reg.IsMatch(recipe.Name) || reg.IsMatch(recipe.Description) || recipe.Preparation.Any(x => reg.IsMatch(x.Step))).ToList();
        Console.WriteLine($"Found {searched.Count} recipes by keyword: {keyword}");
        return searched;
    }

    // Search recipes by tags
    public static List<Recipe>? SearchRecipesByTags(List<RecipeTag> tags, List<Recipe> recipes)
    {
        if (!Utils.ValidateTags(tags))
        {
            return null;
        }

        var searched = recipes.Where(recipe => tags.Any(tag => recipe.Tags.Contains(tag))).ToList();
        Console.WriteLine($"Found {searched.Count} recipes by tags: {string.Join(", ", tags.Select(tag => tag.Tag))}");
        return searched;
    }

    public static List<Recipe> SearchRecipesByTimeConstraint(int time, List<Recipe> recipes)
    {
        var searched = recipes.Where(recipe => recipe.CookingTime >= time - 3 && recipe.CookingTime <= time + 3).ToList();
        Console.WriteLine($"Found {searched.Count} recipes by time constraint: {time}");
        return searched;
    }

    // Search recipes by rating
    public static List<Recipe> SearchRecipesByRating(int rating, List<Recipe> recipes)
    {
        var searched = recipes.Where(recipe => recipe.AverageScore == rating).ToList();
        Console.WriteLine($"Found {searched.Count} recipes by rating: {rating}");
        return searched;
    }

    // Search recipes by servings constraint
    public static List<Recipe> SearchRecipesByServings(int servings, List<Recipe> recipes)
    {
        var searched = recipes.Where(recipe => recipe.Servings == servings).ToList();
        Console.WriteLine($"Found {searched.Count} recipes by servings: {servings}");
        return searched;
    }

    // Search recipes in favorites
    public static List<Recipe> SearchRecipesInFavorites(int favourite, List<Recipe> recipes)
    {
        var searched = recipes.Where(recipe => recipe.Favourite == favourite).ToList();
        Console.WriteLine($"Found {searched.Count} recipes by favorite: {favourite}");
        return searched;
    }

    public static List<Recipe> SearchRecipesFavorited(List<Recipe> recipes)
    {
        List<Recipe> searched = AuthenticationManager.Instance.CurrentUser.FavRecipes.Intersect(recipes).ToList();
        return searched;
    }

    // Search recipes by owner username
    public static List<Recipe> SearchRecipesByOwnerUsername(string ownerUsername, List<Recipe> recipes)
    {
        var searched = recipes.Where(recipe => recipe.Owner.Username != null && recipe.Owner.Username.Equals(ownerUsername, StringComparison.OrdinalIgnoreCase));
        return searched.ToList();
    }

    // Search by Ingredients

    public static List<Recipe> SearchRecipeByIngredient(string ingredient, List<Recipe> recipes)
    {
        var searched = recipes
            .Where(recipe => recipe.RecipeIngredients
            .Any(ri => ri.Ingredient != null && ri.Ingredient.Name.Equals(ingredient, StringComparison.OrdinalIgnoreCase))).ToList();
        return searched.ToList();
    }

    // Example of chaining search queries
    public static List<Recipe>? SearchRecipes(string? keyword = null, List<RecipeTag>? tags = null, int? time = null, int? rating = null, int? servings = null, bool? favourite = null, string? ownerUsername = null, string? ingredient = null)
    {
        List<Recipe> searchedRecipes = GetRecipes();

        //checking favourites first as it eleminates the most amount of recipes
        if (searchedRecipes is not null && favourite is not null && favourite == true)
        {
            searchedRecipes = SearchRecipesFavorited(searchedRecipes);
        }

        //immediately return if no recipes are found in favourites
        if(searchedRecipes is not null && searchedRecipes.Count == 0)
        {
            return null;
        }

        if (searchedRecipes is not null && !string.IsNullOrEmpty(keyword))
        {
            searchedRecipes = SearchRecipesByKeyword(keyword, searchedRecipes);
        }

        if (searchedRecipes is not null && tags != null && tags.Any())
        {
            searchedRecipes = SearchRecipesByTags(tags, searchedRecipes)!;
        }

        if (searchedRecipes is not null && time.HasValue)
        {
            searchedRecipes = SearchRecipesByTimeConstraint(time.Value, searchedRecipes);
        }

        if (searchedRecipes is not null && rating.HasValue)
        {
            searchedRecipes = SearchRecipesByRating(rating.Value, searchedRecipes);
        }

        if (searchedRecipes is not null && servings.HasValue)
        {
            searchedRecipes = SearchRecipesByServings(servings.Value, searchedRecipes);
        }

        if (searchedRecipes is not null && !string.IsNullOrEmpty(ownerUsername))
        {
            searchedRecipes = SearchRecipesByOwnerUsername(ownerUsername, searchedRecipes);
        }

        if (searchedRecipes is not null && !string.IsNullOrEmpty(ingredient))
        {
            searchedRecipes = SearchRecipeByIngredient(ingredient, searchedRecipes);
        }

        return searchedRecipes;
    }
}


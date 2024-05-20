using System.Text.RegularExpressions;
using System.Collections.Generic;
using RecipeInfo;
using DataLayer;

namespace BusinessLayer
{
    public class Search
    {
        public static List<Recipe> GetRecipes()
        { 
            using (var dbContext = new AnnualEventsContext())
            {
                return dbContext.Recipe.ToList();
            }
        }

        // Search recipes by keyword
        public static List<Recipe> SearchRecipesByKeyword(string keyword, List<Recipe> recipes)
        {
            string escaped = Regex.Escape(keyword);
            var reg = new Regex(escaped, RegexOptions.IgnoreCase);
            var searched = recipes.Where(recipe => reg.IsMatch(recipe.Name) || reg.IsMatch(recipe.Description) || recipe.Preparation.Any(x => reg.IsMatch(x.Step)));
            return searched.ToList();
        }

        // Search recipes by tags
        public static List<Recipe>? SearchRecipesByTags(List<RecipeTag> tags, List<Recipe> recipes)
        {
            if (!Utils.ValidateTags(tags))
            {
                return null;
            }
            var searched = recipes.Where(recipe => recipe.Tags.Intersect(tags).Any());
            return searched.ToList();
        }

         public static List<Recipe> SearchRecipesByTimeConstraint(int time, List<Recipe> recipes)
        {
            var searched = recipes.Where(recipe => recipe.CookingTime >= time - 3 && recipe.CookingTime <= time + 3);
            return searched.ToList();
        }

        // Search recipes by rating
        public static List<Recipe> SearchRecipesByRating(int rating, List<Recipe> recipes)
        {
            var searched = recipes.Where(recipe => recipe.AverageScore == rating);
            return searched.ToList();
        }

        // Search recipes by servings constraint
        public static List<Recipe> SearchRecipesByServings(int servings, List<Recipe> recipes)
        {
            var searched = recipes.Where(recipe => recipe.Servings == servings);
            return searched.ToList();
        }

        // Search recipes in favorites
        public static List<Recipe> SearchRecipesInFavorites(int favourite, List<Recipe> recipes)
        {
            var searched = recipes.Where(recipe => recipe.Favourite == favourite);
            return searched.ToList();
        }

        // Search recipes by owner username
        public static List<Recipe> SearchRecipesByOwnerUsername(string ownerUsername, List<Recipe> recipes)
        {
            var searched = recipes.Where(recipe => recipe.Owner.Username == ownerUsername);
            return searched.ToList();
        }

        // Example of chaining search queries
        public List<Recipe> SearchRecipes(string keyword, List<Recipe> recipes, List<RecipeTag> tags, int time, int rating, int servings, int favourite, string ownerUsername)
        {
            List<Recipe> searchedRecipes = recipes;

            if (!string.IsNullOrEmpty(keyword))
            {
                searchedRecipes = SearchRecipesByKeyword(keyword, searchedRecipes);
            }

            if (tags != null && tags.Any())
            {
                searchedRecipes = SearchRecipesByTags(tags, searchedRecipes);
            }

            if (time > 0)
            {
                searchedRecipes = SearchRecipesByTimeConstraint(time, searchedRecipes);
            }

            if (rating > 0)
            {
                searchedRecipes = SearchRecipesByRating(rating, searchedRecipes);
            }

            if (servings > 0)
            {
                searchedRecipes = SearchRecipesByServings(servings, searchedRecipes);
            }

            if (favourite > 0)
            {
                searchedRecipes = SearchRecipesInFavorites(favourite, searchedRecipes);
            }

            if (!string.IsNullOrEmpty(ownerUsername))
            {
                searchedRecipes = SearchRecipesByOwnerUsername(ownerUsername, searchedRecipes);
            }

            return searchedRecipes;
        }

    }
}

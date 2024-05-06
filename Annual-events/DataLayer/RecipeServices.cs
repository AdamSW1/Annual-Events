using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using RecipeInfo;
using System.Security.Cryptography.X509Certificates;

namespace DataLayer;

public class RecipeServices
{
    private static RecipeServices? _instance;
    public static RecipeServices Instance{
        get { return _instance ??= _instance = new RecipeServices();}
    }

    public AnnualEventsContext DbContext { get; set; } = AnnualEventsContext.Instance;
    public RecipeServices(){}

    public Recipe? GetRecipe(string name)
    {
        var dbRecipe =  DbContext.Recipe
            .Where(r=>r.Name == name)
            .First();
        if(dbRecipe is null)
        {
            return null;
        }
        return dbRecipe;
    }

    public List<Recipe> GetRecipes()
    {
        return DbContext.Recipe!
            .ToList();
    }
    
    public List<Recipe> GetRecipesByOwner(Annual_Events_User owner)
    {
        return DbContext.Recipe!
            .Where(recipe => recipe.Owner.Username == owner.Username)
            .ToList();
    }

    public List<Recipe> GetRecipesByRating(int rating)
    {
        return DbContext.Recipe!
            .Where(recipe => recipe.AverageScore == rating)
            .ToList();
    }

    public List<Recipe> GetRecipesByServings(int servings)
    {
        return DbContext.Recipe!
            .Where(recipe => recipe.Servings == servings)
            .ToList();
    }

    public List<Recipe> GetRecipesByTimeConstraint(int time)
    {
        return DbContext.Recipe!
            .Where(recipe => recipe.CookingTime >= time -3 && recipe.CookingTime <= time + 3)
            .ToList();
    }

    public List<Recipe> GetRecipesInFavorites(int favourite)
    {
        return DbContext.Recipe!
            .Where(recipe => recipe.Favourite == favourite)
            .ToList();
    }
    public List<Recipe> GetRecipesFavByUser(Annual_Events_User user)
    {
        return DbContext.Recipe!
            .Where(recipe => recipe.FavouritedBy.Any(favUser => favUser.Username == user.Username))
            .ToList();
    }
    public void AddRecipe(Recipe recipe) 
    {
        if (recipe != null)
        {
            DbContext.Recipe!.Add(recipe);
            DbContext.SaveChanges();
        }
    }

    public void DeleteRecipe(Recipe recipe)
    {
        Recipe? DbRecipe = DbContext.Recipe
                            .Where(r=>r.RecipeID == recipe.RecipeID)
                            .FirstOrDefault();

        if(DbRecipe != null)
        {
            DbContext.Recipe!.RemoveRange(DbRecipe);
            DbContext.SaveChanges();
        }
    }

    public RecipeTag? GetRecipeTag(string recipeTag){
        Recipe? R = DbContext.Recipe.Where(recipe => recipe.Tags.Where(tag => tag.Tag == recipeTag).Any()).FirstOrDefault();
        if (R is null){
            return null;
        }
        return R.Tags.Where(tag => tag.Tag == recipeTag).FirstOrDefault();
    }

    public Ingredient? GetIngredient(string ingredientName){
        RecipeIngredient? RI = DbContext.RecipeIngredients.Where(x => x.Ingredient!.Name == ingredientName).FirstOrDefault();
        if (RI is null){
            return null;
        }
        return RI.Ingredient;
    }
}
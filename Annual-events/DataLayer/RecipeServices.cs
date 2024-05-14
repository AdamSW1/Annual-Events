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
            .Where(r=>r.Name.ToLower() == name.ToLower())
            .Include(r => r.Tags)
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .Include(r => r.Reviews)
            .Include(r => r.FavouritedBy)
            .Include(r => r.Preparation)
            .FirstOrDefault();
        if(dbRecipe is null)
        {
            return null;
        }
        return dbRecipe;
    }

    public List<Recipe> GetRecipes()
    {
        return DbContext.Recipe!
            .Include(r => r.Tags)
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .Include(r => r.Reviews)
            .Include(r => r.FavouritedBy)
            .Include(r => r.Preparation)
            .ToList();
    }

    public List<Recipe> GetRecipesCurrentUser(Annual_Events_User user)
    {
        return DbContext.Recipe!
            .Where(recipe => recipe.Owner.Username == user.Username)
            .Include(r => r.Tags)
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .Include(r => r.Reviews)
            .Include(r => r.FavouritedBy)
            .Include(r => r.Preparation)
            .ToList();
    }
    
    public List<Recipe> GetRecipesByOwner(Annual_Events_User owner)
    {
        return DbContext.Recipe!
            .Where(recipe => recipe.Owner.Username == owner.Username)
            .Include(r => r.Tags)
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .Include(r => r.Reviews)
            .Include(r => r.FavouritedBy)
            .Include(r => r.Preparation)
            .ToList();
    }

    public List<Recipe> GetRecipesByRating(int rating)
    {
        return DbContext.Recipe!
            .Where(recipe => recipe.AverageScore == rating)
            .Include(r => r.Tags)
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .Include(r => r.Reviews)
            .Include(r => r.FavouritedBy)
            .Include(r => r.Preparation)
            .ToList();
    }

    public List<Recipe> GetRecipesByServings(int servings)
    {
        return DbContext.Recipe!
            .Where(recipe => recipe.Servings == servings)
            .Include(r => r.Tags)
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .Include(r => r.Reviews)
            .Include(r => r.FavouritedBy)
            .Include(r => r.Preparation)
            .ToList();
    }

    public List<Recipe> GetRecipesByTimeConstraint(int time)
    {
        return DbContext.Recipe!
            .Where(recipe => recipe.CookingTime >= time -3 && recipe.CookingTime <= time + 3)
            .Include(r => r.Tags)
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .Include(r => r.Reviews)
            .Include(r => r.FavouritedBy)
            .Include(r => r.Preparation)
            .ToList();
    }

    public List<Recipe> GetRecipesInFavorites(int favourite)
    {
        return DbContext.Recipe!
            .Where(recipe => recipe.Favourite == favourite)
            .Include(r => r.Tags)
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .Include(r => r.Reviews)
            .Include(r => r.FavouritedBy)
            .Include(r => r.Preparation)
            .ToList();
    }
    public List<Recipe> GetRecipesFavByUser(Annual_Events_User user)
    {
        return DbContext.Recipe!
            .Where(recipe => recipe.FavouritedBy!.Any(favUser => favUser.Username == user.Username))
            .Include(r => r.Tags)
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .Include(r => r.Reviews)
            .Include(r => r.FavouritedBy)
            .Include(r => r.Preparation)
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
                            .Include(r => r.Tags)
                            .Include(r => r.RecipeIngredients)
                            .Include(r => r.Reviews)
                            .Include(r => r.FavouritedBy)
                            .Include(r => r.Preparation)
                            .FirstOrDefault();

        if(DbRecipe != null)
        {
            DbContext.Recipe!.Remove(DbRecipe);
            // DbContext.Recipe!.RemoveRange(DbRecipe);
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
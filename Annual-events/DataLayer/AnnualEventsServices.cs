using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using RecipeInfo;
using System.Security.Cryptography.X509Certificates;

namespace DataLayer;

public class AnnualEventsService
{
    private static AnnualEventsService? _instance;

    public static AnnualEventsService Instance{
        get { return _instance ??= _instance = new AnnualEventsService();}
    }

    public AnnualEventsContext DbContext = AnnualEventsContext.Instance;
    public AnnualEventsService(){}

    public Recipe GetRecipe(string name)
    {
        return DbContext.Recipe
        .Where(r=>r.Name == name)
        .First();
    }

    public List<Recipe> GetRecipes()
    {
        return DbContext.Recipe!
        .OrderBy(r=>r.Name)
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

    // public List<Recipe>

    public void AddRecipe(Recipe recipe)
    {
        if (recipe != null)
        {
            DbContext.Recipe!.Add(recipe);
            DbContext.SaveChanges();
        }
    }
    public void UpdateRecipe(Recipe recipe)
    {
        Recipe? DbRecipe = DbContext.Recipe
        .Where(r=>r.RecipeID == recipe.RecipeID)
        .FirstOrDefault();

        if(DbRecipe != null)
        {
            DbRecipe.RecipeID = recipe.RecipeID;
            DbRecipe.Name = recipe.Name;
            DbRecipe.Description = recipe.Description;
            DbRecipe.Preparation = recipe.Preparation;
            DbRecipe.Servings = recipe.Servings;
            DbRecipe.Ingredients = recipe.Ingredients;
            DbRecipe.Favourite = recipe.Favourite;
            DbRecipe.Owner = recipe.Owner;
            DbRecipe.Tags = recipe.Tags;
            DbRecipe.Reviews = recipe.Reviews;

            DbContext.Recipe!.Update(DbRecipe);
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

    public Preparation GetPreparation(int id){
        Preparation preparation = (Preparation)DbContext.Preparation
                                    .Where(prep => prep.PreparationID == id)
                                    .First();
        
        return preparation;
    }
    public void AddPreparation(Preparation preparation){
        DbContext.Preparation.Add(preparation);
        DbContext.SaveChanges();
    }

    public void RemovePreparation(Preparation preparation)
    {
        var query = from Preparation in DbContext.Preparation
        where preparation.PreparationID == preparation.PreparationID
        select preparation;

        foreach (var prep in query){
            DbContext.Preparation.Remove(prep);
        }
        DbContext.SaveChanges();

    }

    public Ingredient? GetIngredient(string ingredientName){
        RecipeIngredient? RI = DbContext.RecipeIngredients.Where(x => x.Ingredient.Name == ingredientName).FirstOrDefault();
        if (RI is null){
            return null;
        }
        return RI.Ingredient;
    }

    public RecipeTag? GetRecipeTag(string recipeTag){
        Recipe? R = DbContext.Recipe.Where(recipe => recipe.Tags.Where(tag => tag.Tag == recipeTag).Any()).FirstOrDefault();
        if (R is null){
            return null;
        }
        return R.Tags.Where(tag => tag.Tag == recipeTag).FirstOrDefault();
    }

}
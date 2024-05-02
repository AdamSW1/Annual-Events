using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using RecipeInfo;
namespace DataLayer;

public class AnnualEventsService
{
    private static AnnualEventsService? _instance;

    public static AnnualEventsService Instance{
        get { return _instance ??= _instance = new AnnualEventsService();}
    }
    public AnnualEventsContext DbContext = AnnualEventsContext.Instance;
    public AnnualEventsService(){}

    // Recipes
    public void AddRecipe(Recipe recipe)
    {
        DbContext.Recipe.Add(recipe);
        DbContext.SaveChanges();
    }

    public void UpdateRecipe(Recipe recipe)
    {
        var query = from Annual_Events_Recipe in DbContext.Recipe
                    where recipe.RecipeID == recipe.RecipeID
                    select recipe;

        foreach (var rec in query)
        {
            rec.Name = recipe.Name;
            rec.Description = recipe.Description;
            rec.CookingTime = recipe.CookingTime;
            rec.Preparation = recipe.Preparation;
            rec.Servings = recipe.Servings;
            rec.Ingredients = recipe.Ingredients;
            rec.Favourite = recipe.Favourite;
            rec.Owner = recipe.Owner;
            rec.Tags = recipe.Tags;
            rec.Reviews = recipe.Reviews;
        }

    }

    // Users
    public void DeleteUser(string authenticatedUsername)
    {
        var userToDelete = AnnualEventContext.Annual_Events_User
                                           .Include(u => u.Recipes)
                                           .Include(u => u.FavRecipes)
                                           .SingleOrDefault(u => u.Username == authenticatedUsername);
        if (userToDelete != null)
        {
            // Remove associated recipes
            AnnualEventContext.Recipe.RemoveRange(userToDelete.Recipes);

            // Remove associated favorite recipes
            AnnualEventContext.Recipe.RemoveRange(userToDelete.FavRecipes);

            // Remove the user from the database
            AnnualEventContext.Annual_Events_User.Remove(userToDelete);

            // Save changes to the database
            AnnualEventContext.SaveChanges();
        }
    }

}
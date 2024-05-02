using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using RecipeInfo;
namespace DataLayer;

public class AnnualEventsService
{
    private AnnualEventsContext _AnnualEventsContext;
    public AnnualEventsContext AnnualEventContext
    {
        get
        {
            return _AnnualEventsContext;
        }
        set
        {
            _AnnualEventsContext = value;
        }
    }
    public AnnualEventsService(AnnualEventsContext annualEventsContext)
    {
        _AnnualEventsContext = annualEventsContext;
    }

    // Recipes
    public void AddRecipe(Recipe recipe)
    {
        _AnnualEventsContext.Recipe.Add(recipe);
        _AnnualEventsContext.SaveChanges();
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
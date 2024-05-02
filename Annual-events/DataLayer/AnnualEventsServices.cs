using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using RecipeInfo;
using Microsoft.Win32;
namespace DataLayer;

public class AnnualEventsService
{
    private static AnnualEventsService? _instance;

    public static AnnualEventsService Instance
    {
        get { return _instance ??= _instance = new AnnualEventsService(); }
    }
    public AnnualEventsContext DbContext = AnnualEventsContext.Instance;
    public AnnualEventsService() { }

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

    // This method should also use DbContext
    public void ViewFavRecipesDb(string username)
    {
        string returnStr = "";
        // Use the existing DbContext instance instead of creating a new one
        var user = AnnualEventsContext.Annual_Events_User
           .Include(u => u.FavRecipes)
           .FirstOrDefault(u => u.Username == username);

        if (user != null && user.FavRecipes != null && user.FavRecipes.Any())
        {
            foreach (var recipe in user.FavRecipes)
            {
                returnStr += $"{recipe.Name}\n";
            }
        }
        else
        {
            returnStr += "No favorite recipes found.";
        }
        
        Console.WriteLine(returnStr); 
    }


    // Users

    // This should use DbContext instead, just a package issue
    public void AddUser(Annual_Events_User user)
    {
        AnnualEventsContext.Annual_Events_User.Add(user);
        DbContext.SaveChanges();
    }
    
    // this method should also work with DbContext: Will change
    public void DeleteUser(string authenticatedUsername)
    {
        var userToDelete = AnnualEventsContext.Annual_Events_User
                                           .Include(u => u.Recipes)
                                           .Include(u => u.FavRecipes)
                                           .SingleOrDefault(u => u.Username == authenticatedUsername);
        if (userToDelete != null)
        {
            // Remove associated recipes
            AnnualEventsContext.Recipe.RemoveRange(userToDelete.Recipes);

            // Remove associated favorite recipes
            AnnualEventsContext.Recipe.RemoveRange(userToDelete.FavRecipes);

            // Remove the user from the database
            AnnualEventsContext.Annual_Events_User.Remove(userToDelete);

            // Save changes to the database
            DbContext.SaveChanges();
        }
    }

}
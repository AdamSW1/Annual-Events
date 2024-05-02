using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using RecipeInfo;
namespace DataLayer;

public class AnnualEventsService
{
    public AnnualEventsContext DbContext = AnnualEventsContext.Instance;
    public AnnualEventsService()
    {
    }

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

}
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

}
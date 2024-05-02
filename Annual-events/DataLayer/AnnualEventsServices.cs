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

    public void AddRecipe(Recipe recipe)
    {
        _AnnualEventsContext.Recipe.Add(recipe);
        _AnnualEventsContext.SaveChanges();
    }

}
using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using RecipeInfo;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Specialized;

namespace DataLayer;

public class AnnualEventsService
{
    private static AnnualEventsService? _instance;

    public static AnnualEventsService Instance
    {
        get { return _instance ??= _instance = new AnnualEventsService(); }
    }

    public AnnualEventsContext DbContext{get;set;} = AnnualEventsContext.Instance;
    public AnnualEventsService(){}

    public void AddRecipe(Recipe recipe)
    {
        DbContext.Recipe.Add(recipe);
        DbContext.SaveChanges();
    }

    public Preparation GetPreparation(int id)
    {
        Preparation preparation = (Preparation)DbContext.Preparation
                                    .Where(prep => prep.PreparationID == id)
                                    .First();
        return preparation;
    }
    public void AddPreparation(Preparation preparation)
    {
        DbContext.Preparation.Add(preparation);
        DbContext.SaveChanges();
    }

    public void RemovePreparation(Preparation preparation)
    {
        var query = (from Preparation in DbContext.Preparation
        where preparation.PreparationID == preparation.PreparationID
        select preparation).FirstOrDefault();

        if(query != null){
            DbContext.Preparation.Remove(query);
            DbContext.SaveChanges();
        }
    }

    //Get all the reviews for a user
    public List<Review> GetReviewsForUser(Annual_Events_User user)
    {
        return DbContext.Review!
            .Where(review => review.ReviewerUsername == user.Username)
            .ToList();
    }   
    //Get all the reviews for a recipe
    public List<Review> GetReviewsForRecipe(Recipe recipe)
    {
        return DbContext.Review!
            .Where(review => review.Recipe.Name == recipe.Name)
            .ToList();
    }
}
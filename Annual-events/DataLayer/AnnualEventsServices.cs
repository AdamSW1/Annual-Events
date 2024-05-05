using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using RecipeInfo;
using System.Security.Cryptography.X509Certificates;

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

    public void GetReview(int id) 
    {
        Review review = (Review)DbContext.Review
                        .Where(rev => rev.ReviewId == id)
                        .First();
        return review;
    }
    public void AddReview(Review review) 
    {
        DbContext.Review.Add(review);
        DbContext.SaveChanges();
    }

    public void DeleteReview(Review review) 
    {
        DbContext.Review.Remove(review);
        DbContext.SaveChanges();
    }

}
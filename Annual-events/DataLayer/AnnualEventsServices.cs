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

    public AnnualEventsContext DbContext = AnnualEventsContext.Instance;
    public AnnualEventsService() { }

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
}
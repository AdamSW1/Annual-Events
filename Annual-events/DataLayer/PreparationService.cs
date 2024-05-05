using RecipeInfo;

namespace DataLayer;

public class PreparationService
{
    private static PreparationService? _instance;

    public static PreparationService Instance
    {
        get { return _instance ??= _instance = new PreparationService(); }
    }

    public AnnualEventsContext DbContext { get; set; } = AnnualEventsContext.Instance;
    public PreparationService(){}
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

        if (query != null)
        {
            DbContext.Preparation.Remove(query);
            DbContext.SaveChanges();
        }

    }
}
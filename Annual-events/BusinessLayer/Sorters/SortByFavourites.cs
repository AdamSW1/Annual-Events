using BusinessLayer;
using Comparators;
namespace sorters;

public class SortByFavourites : ISorter
{

    /// <summary>
    /// This method sorts a list of recipes by the number of favourites
    /// using the compareByFavourites comparer
    /// </summary>
    /// <param name="Recipes"></param>
    void ISorter.Sort(List<Recipe> Recipes)
    {
        Recipes.Sort(new CompareByFavourites());
    }
}
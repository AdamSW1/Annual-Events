using BusinessLayer;
using Comparators;
using RecipeInfo;
namespace sorters;

public class SortByTime : ISorter
{
    /// <summary>
    /// sorts a list of recipes by the time to prepare it
    /// using the compareByTime comparer
    /// </summary>
    /// <param name="Recipes"></param>
    void ISorter.Sort(List<Recipe> Recipes)
    {
        Recipes.Sort(new CompareByTime());
    }
}
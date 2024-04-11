using BusinessLayer;
using Comparators;
using RecipeInfo;
namespace sorters;

public class SortByRating : ISorter
{
    /// <summary>
    /// sorts a list of recipes by the number of ratings
    /// using the compareByRating comparer
    /// </summary>
    /// <param name="Recipes"></param>
    void ISorter.Sort(List<Recipe> Recipes)
    {
        Recipes.Sort(new CompareByRating());
    }
}
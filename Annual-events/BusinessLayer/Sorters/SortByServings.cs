using BusinessLayer;
using Comparators;
using RecipeInfo;
namespace sorters;

public class SortByServings : ISorter
{
    /// <summary>
    /// sorts a list of recipes by the number of servings
    /// using the compareByServing comparer
    /// </summary>
    /// <param name="Recipes"></param>
    void ISorter.Sort(List<Recipe> Recipes)
    {
        Recipes.Sort(new CompareByServings());
    }
}
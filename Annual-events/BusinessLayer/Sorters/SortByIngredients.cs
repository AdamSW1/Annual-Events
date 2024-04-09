using BusinessLayer;
using Comparators;
using RecipeInfo;
namespace sorters;

public class SortByIngredients : ISorter
{
    /// <summary>
    /// sorts a list of recipes by the number of Ingredients
    /// using the compareByIngredients comparer
    /// </summary>
    /// <param name="Recipes"></param>
    void ISorter.Sort(List<Recipe> Recipes)
    {
        Recipes.Sort(new CompareByIngredients());
    }
}
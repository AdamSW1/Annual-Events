using RecipeInfo;

namespace Comparators;

public class CompareByIngredients : IComparer<Recipe>
{

    /// <summary>
    /// compares recipes by their ingredients
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    int IComparer<Recipe>.Compare(Recipe? x, Recipe? y)
    {
        if(x == null || y == null){
            return 0;
        }
        if(x.RecipeIngredients.Count.CompareTo(y.RecipeIngredients.Count) != 0){
            return x.RecipeIngredients.Count.CompareTo(y.RecipeIngredients.Count);
        }

        return 0;
    }

}
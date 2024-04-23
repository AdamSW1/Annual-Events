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
        if(x.Ingredients.Count.CompareTo(y.Ingredients.Count) != 0){
            return x.Ingredients.Count.CompareTo(y.Ingredients.Count);
        }

        return 0;
    }

}
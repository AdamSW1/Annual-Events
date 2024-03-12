namespace Comparators;
using BusinessLayer;

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
        throw new NotImplementedException();
    }

}
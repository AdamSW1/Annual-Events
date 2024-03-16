using RecipeInfo;

namespace Comparators;

public class CompareByServings : IComparer<Recipe>
{
    /// <summary>
    /// compares recipes by their serving sizes
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    int IComparer<Recipe>.Compare(Recipe? x, Recipe? y)
    {
        throw new NotImplementedException();
    }
}
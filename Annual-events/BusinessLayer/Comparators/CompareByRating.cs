using BusinessLayer;

namespace Comparators;

public class CompareByRating : IComparer<Recipe>
{

    /// <summary>
    /// Compares recipes by their ratings
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    int IComparer<Recipe>.Compare(Recipe? x, Recipe? y)
    {
        throw new NotImplementedException();
    }

}
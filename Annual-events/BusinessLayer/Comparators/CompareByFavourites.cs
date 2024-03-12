namespace Comparators;
using BusinessLayer;

public class CompareByFavourites : IComparer<Recipe>
{
    /// <summary>
    /// compares recipes by how many favourites they have
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
using RecipeInfo;
namespace Comparators;

public class CompareByTime : IComparer<Recipe>
{

    /// <summary>
    /// compares recipes by the time to make them
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    int IComparer<Recipe>.Compare(Recipe? x, Recipe? y)
    {
        throw new NotImplementedException();
    }

}
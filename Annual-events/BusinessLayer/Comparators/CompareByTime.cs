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
        if (x == null || y == null){
            return 0;
        }

        if(x.CookingTime.CompareTo(y.CookingTime) != 0){
            return x.CookingTime.CompareTo(y.CookingTime);
        }

        return 0;
    }

}
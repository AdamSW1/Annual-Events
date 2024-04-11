using RecipeInfo;

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
        if(x == null || y == null){
            return 0;
        }
        if(x.Ratings.CompareTo(y.Ratings) != 0){
            return x.Ratings.CompareTo(y.Ratings);
        }
        return 0;
    }

}
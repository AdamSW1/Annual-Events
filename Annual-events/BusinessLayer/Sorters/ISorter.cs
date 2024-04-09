using BusinessLayer;
using RecipeInfo;
namespace sorters;

interface ISorter{
    abstract void Sort(List<Recipe> Recipes);
}


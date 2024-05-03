// RecipeManager.cs
using System.Linq;
using System.Collections.Generic;
using BusinessLayer;
using DataLayer;
namespace RecipeInfo;


public class RecipeManager
{
    public static void AddRecipe(Recipe  newRecipe)
    {
        // Create recipe

        // Add the recipe to the user's list
        newRecipe.Ingredients.ForEach(RI =>{
            if (RI.Ingredient is null){
                return;
            }
            Ingredient? ingr = AnnualEventsService.Instance.GetIngredient(RI.Ingredient.Name);
            if (ingr != null){
                RI.Ingredient = ingr;
            }
            return;
        });

        newRecipe.Tags.ToList().ForEach(tag =>{
            if(tag is null || tag.Tag is null){
                return;
            }
            RecipeTag? RT = AnnualEventsService.Instance.GetRecipeTag(tag.Tag);
            if (RT != null){
                newRecipe.Tags.Remove(tag);
                newRecipe.Tags.Add(RT);
                return;
            }
            return;
        });
        newRecipe.Owner.AddRecipe(newRecipe);
        AnnualEventsService.Instance.AddRecipe(newRecipe);
    }

    //Will delete from the DB in the future, maybe?
    public static void DeleteRecipe(Annual_Events_User user, Recipe recipeToDelete)
    {
        user.DeleteRecipe(recipeToDelete);
    }

    public static void AddToFavRecipe(Annual_Events_User user, Recipe recipeToAdd)
    {
        user.AddToFavRecipe(recipeToAdd);
    }
    public static void DeleteFavRecipe(Annual_Events_User user, Recipe recipeToDelete)
    {
        user.RemoveFromFavRecipe(recipeToDelete);
    }

    public static void UpdateRecipe(string newName, string newDescription, double newCookingTime, List<Preparation> newPreparation, int newServings, Recipe recipeToUpdate)
    {
            recipeToUpdate.Name = newName;
            recipeToUpdate.Description = newDescription;
            recipeToUpdate.CookingTime = newCookingTime;
            recipeToUpdate.Preparation = newPreparation;
            recipeToUpdate.Servings = newServings;
    }
}

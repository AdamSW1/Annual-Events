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

        //check if the ingredient already exists, replace it in the recipe object if it does
        newRecipe.RecipeIngredients.ForEach(RI =>{
            if (RI.Ingredient is null){
                return;
            }
            Ingredient? ingr = RecipeServices.Instance.GetIngredient(RI.Ingredient.Name);
            if (ingr != null){
                RI.Ingredient = ingr;
            }
            return;
        });

        //checks if the tags are already in the db and assigns or creates them
        newRecipe.Tags.ToList().ForEach(tag =>{
            if(tag is null || tag.Tag is null){
                return;
            }
            RecipeTag? RT = RecipeServices.Instance.GetRecipeTag(tag.Tag);
            if (RT != null){
                newRecipe.Tags.Remove(tag);
                newRecipe.Tags.Add(RT);
                return;
            }
            return;
        });
        // Add the recipe to the user's list
        newRecipe.Owner.AddRecipe(newRecipe);
        RecipeServices.Instance.AddRecipe(newRecipe);
    }

    //Call the service method to delete the recipe
    public static void DeleteRecipe(Annual_Events_User user, Recipe recipeToDelete)
    {
        RecipeServices.Instance.DeleteRecipe(recipeToDelete);
        AnnualEventsContext.Instance.SaveChanges();
    }
    
    public static void AddToFavRecipe(Annual_Events_User user, Recipe recipeToAdd)
    {

        user.AddToFavRecipe(recipeToAdd);
    }
    //Call the service method to delete the recipe
    public static void DeleteFavRecipe(Annual_Events_User user, Recipe recipeToDelete)
    {
        user.RemoveFromFavRecipe(recipeToDelete);
        RecipeServices.Instance.DbContext.SaveChanges();
    }
}

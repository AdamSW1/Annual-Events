using System.ComponentModel;

namespace BusinessLayer;


class Recipe
{
    private string _name;
    public string Name { get; set; }
    private string _description;
    public string Description { get; set; }
    private double _cookingTime;
    public double CookingTime { get; set; }
    private string _preparation;
    public string Preparation { get; set; }
    private int _servings;
    public int Servings { get; set; }
    private int _ratings;
    public int Ratings { get; set; }
    private string _ingredients;
    public string Ingredients { get; set; }
    public Recipe(string name, string description, double cookingTime, string preparation, int servings, int ratings, string ingredients)
    {
        Name = name;
        Description = description;
        CookingTime = cookingTime;
        Preparation = preparation;
        Servings = servings;
        Ratings = ratings;
        Ingredients = ingredients;
    }
    public void createRecipe()
    {
        // Create a new recipe
        throw new NotImplementedException();
    }
    public void updateRecipe()
    {
        // Update an existing recipe
        throw new NotImplementedException();
    }
    public void deleteRecipe()
    {
        // Delete an existing recipe
        throw new NotImplementedException();
    }
    public void rateRecipe()
    {
        // Rate a recipe
        throw new NotImplementedException();
    }
}
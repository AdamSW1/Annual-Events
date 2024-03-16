namespace RecipeInfo;
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
    private List<Ingredient> _ingredients;

    public List<Ingredient> Ingredients {get; set;}
    private int _favourite;
    public int Favourite { get; set; }
    public Recipe(string name, string description, double cookingTime, string preparation, int servings, int ratings, List<Ingredient> ingredients,int favourite)
    {
        _name = name;
        _description = description;
        _cookingTime = cookingTime;
        _preparation = preparation;
        _servings = servings;
        _ratings = ratings;
        _ingredients = ingredients;
        _favourite = favourite;
    }
    
    public void CreateRecipe()
    {
        // Create a new recipe
        throw new NotImplementedException();
    }
    public void UpdateRecipe()
    {
        // Update an existing recipe
        throw new NotImplementedException();
    }
    public void DeleteRecipe()
    {
        // Delete an existing recipe
        throw new NotImplementedException();
    }
    public void RateRecipe()
    {
        // Rate a recipe
        throw new NotImplementedException();
    }

    public void AddFavourite()
    {
        //adds a favourite to the favourite field
        throw new NotImplementedException();
    }
    public void RemoveFavourite()
    {
        //removes a favourite from the favourite field
        throw new NotImplementedException();
    }
}

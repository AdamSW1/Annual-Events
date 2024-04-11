using BusinessLayer;

namespace RecipeInfo;
class Recipe
{

    private User _owner;
    public User Owner {
        get { return _owner;}
        set { _owner = value;}
    }
    
    private string _name;
    public string Name {
        get{
            return _name;
        }
        set{
            _name = value;
        }
    }
    private string _description;
    public string Description {
        get{
            return _description;
        }
        set{
            _description = value;
        }
    }
    private double _cookingTime;
    public double CookingTime {
        get{
            return _cookingTime;
        }
        set{
            _cookingTime = value;
        }
    }
    private string _preparation;
    public string Preparation {
        get{
            return _preparation;
        }
        set{
            _preparation = value;
        }
    }
    private int _servings;
    public int Servings {
        get{
            return _servings;
        } 
        set{
            _servings = value;
        } 
    }
    private int _ratings;
    public int Ratings {
        get{
            return _ratings;
        } 
        set{
            _ratings = value;
        }
    }
    private List<Ingredient> _ingredients;

    public List<Ingredient> Ingredients {
        get{
            return _ingredients;
        } 
        set{
            _ingredients = value;
        }
        
        }
    private int _favourite;
    public int Favourite { 
        get{
            return _favourite;
        }
    }

    public Recipe(string name, string description, double cookingTime, string preparation, int servings, int ratings, List<Ingredient> ingredients,int favourite, User owner)
    {
        _name = name;
        _description = description;
        _cookingTime = cookingTime;
        _preparation = preparation;
        _servings = servings;
        _ratings = ratings;
        _ingredients = ingredients;
        _favourite = favourite;
        _owner = owner;
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

    internal void DisplayRecipeInfo()
    {
        Console.WriteLine($"Name: {_name}");
        Console.WriteLine($"Description: {_description}");
        Console.WriteLine($"Cooking Time: {_cookingTime} minutes");
        Console.WriteLine($"Preparation: {_preparation}");
        Console.WriteLine($"Servings: {_servings}");
        Console.WriteLine($"Ratings: {_ratings}");
        Console.WriteLine($"Favourite: {_favourite}");
    }
}

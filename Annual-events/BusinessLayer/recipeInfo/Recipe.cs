using BusinessLayer;
using Microsoft.VisualBasic;

namespace RecipeInfo;
class Recipe
{
    public Utils Utils = new Utils();
    private User _owner;
    public User Owner
    {
        get { return _owner; }
        set { _owner = value; }
    }

    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
    private string _description;
    public string Description
    {
        get
        {
            return _description;
        }
        set
        {
            _description = value;
        }
    }
    private double _cookingTime;
    public double CookingTime
    {
        get
        {
            return _cookingTime;
        }
        set
        {
            _cookingTime = value;
        }
    }
    private string _instruction;
    public string Instruction
    {
        get
        {
            return _instruction;
        }
        set
        {
            _instruction = value;
        }
    }
    private int _servings;
    public int Servings
    {
        get
        {
            return _servings;
        }
        set
        {
            _servings = value;
        }
    }
    private double _ratings;
    public double Ratings
    {
        get
        {
            return _ratings;
        }
        set
        {
            _ratings = value;
        }
    }
    private List<Ingredient> _ingredients;

    public List<Ingredient> Ingredients
    {
        get
        {
            return _ingredients;
        }
        set
        {
            _ingredients = value;
        }

    }
    private int _favourite;
    public int Favourite
    {
        get
        {
            return _favourite;
        }
    }

    private List<RecipeTags> _tags;
    public List<RecipeTags> Tags
    {
        get
        {
            return _tags;
        }
        set
        {
            _tags = value;
        }
    }

    private List<string> _review;
    public List<string> Review
    {
        get
        {
            return _review;
        }
        set
        {
            _review = value;
        }
    }

    public Recipe(
        string name, string description,
        double cookingTime, string instruction, int servings, 
        int ratings, List<Ingredient> ingredients,
        int favourite, User owner,List<string> tags, List<string> review
    )
    {
        _name = name;
        _description = description;
        _cookingTime = cookingTime;
        _instruction = instruction;
        _servings = servings;
        _ratings = ratings;
        _ingredients = ingredients;
        _favourite = favourite;
        _owner = owner;
        _tags = Utils.ValidateTags(tags);
        _review = review;
    }

    public void AddToDatabase()
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
        _favourite += 1;
        UpdateRecipe();
    }
    public void RemoveFavourite()
    {
        _favourite -= 1;
        UpdateRecipe();
    }

    internal void DisplayRecipeInfo()
    {
        Console.WriteLine($"Written by: {_owner.Username}");
        Console.WriteLine($"Name: {_name}");
        Console.WriteLine($"Description: {_description}");
        Console.WriteLine($"Cooking Time: {_cookingTime} minutes");
        Console.WriteLine("Ingredients:");
        foreach (var ingredient in Ingredients)
        {
            Console.WriteLine($"{ingredient}");
        }
        Console.WriteLine($"Preparation: {_instruction}");
        Console.WriteLine($"Servings: {_servings}");
        Console.WriteLine($"Ratings: {_ratings}");
        Console.WriteLine($"Favourites: {_favourite}");
        foreach (var review in Review)
        {
            Console.WriteLine($"Review: {review}");
        }
    }
}

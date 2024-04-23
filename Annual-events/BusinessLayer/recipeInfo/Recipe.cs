using BusinessLayer;
using Microsoft.VisualBasic;

namespace RecipeInfo;
public class Recipe
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

    // make a list of preparation objects
    private string _preparation;
    public string Preparation
    {
        get
        {
            return _preparation;
        }
        set
        {
            _preparation = value;
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
            if (value > 5  || value < 1)
            {
                throw new ArgumentException("Rating should be between 1 and 5.");
            }
            else
            {
                _ratings = value;
            }
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

    //make many to many
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

    private List<Review> _reviews = new(){};
    public List<Review> Reviews
    {
        get { 
                _reviews ??= new List<Review>();
                return _reviews; 
            }
        set { _reviews = value; }
    } 

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="cookingTime"></param>
    /// <param name="preparation"></param>
    /// <param name="servings"></param>
    /// <param name="ratings"></param>
    /// <param name="ingredients"></param>
    /// <param name="favourite"></param>
    /// <param name="owner"></param>
    /// <param name="tags"></param>
    public Recipe(
        string name, string description,
        double cookingTime, string preparation, int servings, 
        double ratings, List<Ingredient> ingredients,
        int favourite, User owner,List<string> tags, List<Review> reviews
    )
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
        _tags = Utils.ValidateTags(tags);
        _reviews = reviews;
    }

    // override object.Equals
    public override bool Equals(object obj)
    {
        
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        
        Recipe other = (Recipe)obj;
        bool namesEqual = _name.Equals(other._name);
        bool descriptionsEqual = _description.Equals(other._description);
        bool ownersEqual = (_owner == null && other._owner == null) || (_owner != null && _owner.Equals(other._owner));
        return namesEqual && descriptionsEqual && ownersEqual;
    }
    
    // override object.GetHashCode
    public override int GetHashCode()
    {
        return HashCode.Combine(_name, _description, _owner);
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

    internal string DisplayRecipeInfo()
    {
        string returnStr = "";

        returnStr += $"Written by: {_owner.Username}\n";
        returnStr += $"Name: {_name}\n";
        returnStr += $"Description: {_description}\n";
        returnStr += $"Cooking Time: {_cookingTime} minutes\n";
        returnStr += "Ingredients:\n";
        foreach (var ingredient in Ingredients)
        {
            returnStr += $"{ingredient}\n";
        }
        returnStr += $"Preparation: {_preparation}\n";
        returnStr += $"Servings: {_servings}\n";
        returnStr += $"Ratings: {_ratings}\n";
        returnStr += $"Favourites: {_favourite}\n";

        return returnStr;
    }

    public void AddReview(User reviewer, string reviewText)
    {
        Review review = new Review(reviewer.Username, reviewText);
        Reviews.Add(review);
    }
}

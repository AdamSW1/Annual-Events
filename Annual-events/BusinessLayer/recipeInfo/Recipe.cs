using System.ComponentModel.DataAnnotations.Schema;
using BusinessLayer;
using DataLayer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;

namespace RecipeInfo;
public class Recipe
{
    public AnnualEventsContext AnnualEventsContext {get;}
    public AnnualEventsService AnnualEventsService {get;}
    public Utils Utils = new();

    public int RecipeID {get;set;}

    //[ForeignKey("Owner")]
    public int? OwnerID {
        get{ return _owner.Annual_Events_UserId;}
        }
    private Annual_Events_User _owner;
    public Annual_Events_User Owner 
    {
        get{
            return _owner;
        }
        set{
            // if(value == null){
            //     throw new ArgumentException("Owner cant be null");
            // }
            _owner = value;
        }
    }

    //[ForeignKey("FavouritedBy")]
    // public int? FavouritedByID {get;set;}
    private List<Annual_Events_User>? _favouritedBy;
    public List<Annual_Events_User>? FavouritedBy
    {
        get{
            return _favouritedBy;
        }
        set{
            _favouritedBy = value;
        }
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
            if (!Utils.CheckName(value))
            {
                throw new ArgumentException("Invalid name");
            }
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
            if (!Utils.CheckLongString(value))
            {
                throw new ArgumentException("description cant be null");
            }
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
            if (!Utils.CheckDouble(value))
            {
                throw new ArgumentException("Cooking time cant be negative");
            }
            _cookingTime = value;
        }
    }

    // make a list of preparation objects
    private List<Preparation> _preparation;
    public List<Preparation> Preparation
    {
        
        get
        {
            return _preparation;
        }
        set
        {
            if (!Utils.CheckList(value))
            {
                throw new ArgumentException("Recipe must have a preparation");
            }
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
            if (!Utils.CheckInt(value))
            {
                throw new ArgumentException("Servings must be greater than 0");
            }
            _servings = value;
        }
    }

    public double AverageScore{
        get{
            double total = 0;
            double averageScore = 0;
            _reviews
                .ForEach(review =>{
                    total += review.Score;
                });
            averageScore = total / _reviews.Count;
            if (averageScore is double.NaN){
                return 0;
            }
            return averageScore;
        }
        set{}
        
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
            if(!Utils.CheckList(value)){
                throw new ArgumentException("Invalid list of ingredients");
            }
            _ingredients = value;
        }

    }

    //make many to many
    // user contAINS a list of RECIPEs And recipe has a list of users 
    private int _favourite;
    public int Favourite
    {
        get
        {
            return _favourite;
        }
        set
        {
            _favourite = value;
        }
    }

    private List<RecipeTag> _tags;
    public List<RecipeTag> Tags {
        get{
            return _tags;
        }
        set{
            _tags = value;
        }
    }

    private List<Review> _reviews;
    public List<Review> Reviews
    {
        get
        {
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
    /// <param name="reviews"></param>
    public Recipe(
        string name, string description,
        double cookingTime, List<Preparation> preparation, int servings,
        List<Ingredient> ingredients,
        int favourite, Annual_Events_User owner, List<RecipeTag> tags, List<Review> reviews
    )
    {
        _name = name;
        _description = description;
        _cookingTime = cookingTime;
        _preparation = preparation;
        _servings = servings;
        _ingredients = ingredients;
        _favourite = favourite;
        _owner = owner;
        _tags = tags;
        _reviews = reviews;
    }
    public Recipe(){}

    // override object.Equals
    public override bool Equals(Object? obj)
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

    public void AddToDatabase(Recipe recipe)
    {
        AnnualEventsService.AddRecipe(recipe);   
    }
    public void UpdateRecipe(Recipe)
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
        //UpdateRecipe();
    }
    public void RemoveFavourite()
    {
        _favourite -= 1;
        //UpdateRecipe();
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
        returnStr += $"Preparation:\n";
        _preparation.ForEach(prepStep => returnStr += $"\t{prepStep.StepNumber} - {prepStep.Step.Trim()}\n");
        returnStr += $"Servings: {_servings}\n";
        returnStr += $"Average Rating: {AverageScore}\n";
        returnStr += $"Favourites: {_favourite}\n";

        return returnStr;
    }

    public void AddReview(Annual_Events_User reviewer, string reviewText,int score)
    {
        Review review = new(reviewer.Username, reviewText,score);
        Reviews.Add(review);
    }
}

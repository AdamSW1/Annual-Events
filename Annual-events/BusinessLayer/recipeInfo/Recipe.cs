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
    public List <RecipeTags> Tags
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

    public Recipe(string name, string description, double cookingTime, string instruction, int servings, int ratings, List<Ingredient> ingredients,int favourite, User owner/*,List<string> tags*/)
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
        // _tags = ValidateTags(tags); //TODO create a method which adds and checks if the tags are valid
    }

    public List<RecipeTags> ValidateTags(List<string> tags)
    {
        //Make a list of all enums in recipeTags
        var enum_tags = Enum.GetValues(typeof(RecipeTags));
        List<string> string_enum_tags = enum_tags.Cast<string>().ToList();
        //Check if the input tags matches any of the available tags in the enums and adds it to a list
        List<string> available_tags = new List<string>();
        foreach(string tag in tags){
            if(string_enum_tags.Contains(tag)){
                available_tags.Add(tag);
            }
        }
        return available_tags.Cast<RecipeTags>().ToList();
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
        Console.WriteLine($"Name: {_name}");
        Console.WriteLine($"Description: {_description}");
        Console.WriteLine($"Cooking Time: {_cookingTime} minutes");
        Console.WriteLine("Ingredients:");
        foreach (var ingredient in Ingredients){
            Console.WriteLine($"{ingredient}");
        }
        Console.WriteLine($"\nPreparation: {_instruction}");
        Console.WriteLine($"Servings: {_servings}");
        Console.WriteLine($"Ratings: {_ratings}");
        Console.WriteLine($"Favourite: {_favourite}");
    }
}

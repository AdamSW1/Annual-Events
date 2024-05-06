namespace RecipeInfo;

using System.Text;
using API;
public class Ingredient{

    public int IngredientId { get; set; }
    public List<RecipeIngredient>? RecipeIngredients{ get; set; } = new(){};
    private string _name;
    public string Name {
        get{
            return _name;
        }
        set{
            _name=value;
        }
    }

    private double _price;

    //returns the plain price from the api
    // or the default specified in the constructor if an api price isnt found
    public double Price{
        get{
            Ingredient_json? ingredient = JsonParser.GetIngredient(_name);
            if (ingredient == null){
                return _price;
            }
            return double.Parse(ingredient.price?["usd"].plain ?? $"{_price}");
        }
        set{
            _price=value;
        }
    }

    //the formatted price for an ingredient,
    //if a formatted price isnt found return the plain price
    public string FormattedPrice{
        get{
            Ingredient_json? ingredient = JsonParser.GetIngredient(_name);
            if (ingredient == null){
                return $"_price";
            }
            return ingredient.price?["usd"].formatted ?? $"{Price}";
        }
    }

    public Ingredient(string name, double price){
        _name = name;
        _price = price;
    }
    public Ingredient(){}

    public override bool Equals(object? obj) 
    {
        return obj is Ingredient ing &&
            IngredientId == ing.IngredientId &&
            Name == ing.Name &&
            Price == ing.Price;
    }

    public override int GetHashCode() 
    {
        return HashCode.Combine(IngredientId, Name, Price);
    }

    public override string ToString()
    {
        var str = new StringBuilder();
        str.Append($"{Name}, {Price}");
        return str.ToString();
    }

}
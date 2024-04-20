namespace RecipeInfo;
using API;
public class Ingredient{
    
    private string _name;
    public string Name {
        get{
            return _name;
        }
        set{
            _name=value;
        }
    }

    private string _quantity;

    public string Quantity{
        get{
            return _quantity;
        }
        set{
            _quantity=value;
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

    public Ingredient(string name, string quantity, double price){
        _name = name;
        _quantity = quantity;
        _price = price;
    }

    public override string ToString()
    {
        return $"{Quantity} {Name}, {FormattedPrice}$";
    }

}
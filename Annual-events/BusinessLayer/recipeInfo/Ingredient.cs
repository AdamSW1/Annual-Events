namespace RecipeInfo;
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
    public double Price{
        get{
            return _price;
        }
        set{
            _price=value;
        }
    }

    public Ingredient(string name, string quantity, double price){
        _name = name;
        _quantity = quantity;
        _price = price;
    }

    public override string ToString()
    {
        return $"{Quantity} {Name}, {Price}$";
    }

}
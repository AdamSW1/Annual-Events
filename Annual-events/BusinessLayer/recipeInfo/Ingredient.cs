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

    private double _weight;

    public double Weight{
        get{
            return _weight;
        }
        set{
            _weight=value;
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

    public Ingredient(string name, double weight, double price){
        _name = name;
        _weight = weight;
        _price = price;
    }

}
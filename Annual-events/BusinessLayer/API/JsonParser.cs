using System.Text.Json;
using RecipeInfo;
namespace API;

public class JsonParser{
    private static JsonParser? _instance;

    public static JsonParser? Instance{
        get{
            _instance ??= new JsonParser();
            return _instance;
        }
    }

    private static List<Ingredient_json>? _ingredientList;

    public static List<Ingredient_json>? IngredientList {
        get{
            if(_ingredientList == null){
                Parse();
            }
            return _ingredientList;
        }
    }

    private JsonParser(){}
    
    /// <summary>
    /// Gets json data from the api and converts it
    /// to a list of Ingredient_json objects
    /// <see cref="Ingredient_json"/>
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    public static void Parse(){
        Uri uri= new Uri("http://10.172.19.128:8080/");
        string json = WebReader.Get(uri);
        json = json.Replace("serving size","servingSize");
        json = json.Replace("default","plain");
        json = json.ToLower();
        _ingredientList = JsonSerializer.Deserialize<List<Ingredient_json>>(json) ?? throw new ArgumentNullException("Failed to get json data");
    }
    public static Ingredient_json? GetIngredient(string name){
        name = name.ToLower();
        Ingredient_json? searched = null;
        if(IngredientList is null){
            throw new ArgumentNullException("Failed to get json data",nameof(IngredientList));
        }

        searched = IngredientList
            .FirstOrDefault(x =>
            { 
                if(x.name is not null){
                    return x.name.Contains(name);
                }
                return false;
            });

        return searched;
    }
}
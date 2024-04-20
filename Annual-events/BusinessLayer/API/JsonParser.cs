using System.Text.Json;
using RecipeInfo;
namespace API;

public class JsonParser{
    public static void Parse(){
        Uri uri= new Uri("http://10.172.19.128:8080/");
        
        string json = WebReader.Get(uri);
        json = json.Replace("serving size","servingSize");
        json = json.Replace("default","plain");
        List<Ingredient_json>? recipes = JsonSerializer.Deserialize<List<Ingredient_json>>(json);
        
    }
}
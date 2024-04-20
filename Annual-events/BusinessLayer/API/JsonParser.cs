using System.Text.Json;
namespace API;

public class JsonParser{
    public static void Parse(){
        Uri uri= new Uri("http://10.172.19.128:8080/");
        
        string json = WebReader.Get(uri);
        json = json.Replace("serving size","servingSize");
        json = json.Replace("default","plain");
        List<Recipe_json>? recipes = JsonSerializer.Deserialize<List<Recipe_json>>(json);
        
    }
}
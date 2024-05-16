namespace API;

/// <summary>
/// Objects to map the json from the api to
/// !Important! : field names MUST match the names in the api, case sensitive
/// uncomment fields to access them from the api
/// note: brand and category both cannot be accessed
/// 
/// Api json, must be using Dawson vm 
/// <see cref="http://10.172.19.128:8080/"/>
/// </summary>
public class Ingredient_json
{
    public string? name { get; set; }
    public Dictionary<string, USDPrice>? price { get; set; }
    public string? image { get; set; }

}


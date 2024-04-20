namespace API;

/// <summary>
/// Objects to map the json from the api to
/// !Important! : field names MUST match the names in the api, case sensitive
/// uncomment fields to access them from the api
/// note: brand and category both cannot be accessed
/// </summary>
public class Ingredient_json
{
    public string? name { get; set; }
    // public string? id { get; set; }
    // public string? country { get; set; }
    // public string? seasonal { get; set; }
    // public string? Category { get; set; }
    // public string? Brand {get; set;}
    // public string? weight { get; set;}
    // public bool hot{ get; set; }
    // public bool cold{ get; set; }
    // public bool frozen{ get; set; }
    // public string? description { get; set; }
    public Dictionary<string, USDPrice>? price { get; set; }
    public string? image { get; set; }
    // public Dictionary<string, string>? nutrition { get; set; }
    // public string? ingredients { get; set; }

}


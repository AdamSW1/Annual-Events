using System.Text;
using Microsoft.EntityFrameworkCore;
namespace DataLayer;

public class IngredientsContext
{
    public int IngredientId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Quantity { get; set; } = string.Empty;

    public double Price { get; set; }

    public override bool Equals(object? obj) 
    {
        return obj is IngredientsContext ing &&
            IngredientId == ing.IngredientId &&
            Name == ing.Name &&
            Quantity == ing.Quantity &&
            Price == ing.Price;
    }

    public override int GetHashCode() 
    {
        return HashCode.Combine(IngredientId, Name, Quantity, Price);
    }

    public override string ToString()
    {
    var str = new StringBuilder();

    str.Append($"Ingredient ( IngrdientId={IngredientId}, ");
    str.Append($"Name=\"{Name}\", ");
    str.Append($"Quantity=\"{Quantity}\", ");
    str.Append($"Price={Price.ToString()} )");

    return str.ToString();
    }

}
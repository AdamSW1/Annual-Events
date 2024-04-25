using RecipeInfo;

namespace RecipeInfo;

public class RecipeTag
{
    public int RecipeTagId { get; set; }
    public string? Tag { get; set; }
    
    //Override ToString
    public override string ToString()
    {
        return Tag ?? string.Empty;
    }
}

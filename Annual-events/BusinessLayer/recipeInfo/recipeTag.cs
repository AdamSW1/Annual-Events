using RecipeInfo;

namespace RecipeInfo;

public class RecipeTag
{
    public int RecipeTagId { get; set; }
    public string? Tag { get; set; }

    public RecipeTag(string tag){
        Tag = tag;
    }
    public RecipeTag(){}
    
    //Override ToString
    public override string ToString()
    {
        return Tag ?? string.Empty;
    }
}

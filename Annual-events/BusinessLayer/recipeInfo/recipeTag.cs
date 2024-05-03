using RecipeInfo;

namespace RecipeInfo;

public class RecipeTag
{
    public int RecipeTagId { get; set; }

    public List<Recipe>? RecipeWithTags { get; set; }
   
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

    // override object.Equals
    public override bool Equals(object? obj)
    {
        
        if (obj == null || GetType() != obj.GetType() || Tag ==null)
        {
            return false;
        }
        RecipeTag other = (RecipeTag)obj;

        return Tag.Equals(other.Tag);
    }
    
    // override object.GetHashCode
    public override int GetHashCode()
    {
        return Tag == null ? 0 : Tag.GetHashCode();
    }
}

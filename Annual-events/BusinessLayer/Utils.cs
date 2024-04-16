using BusinessLayer;
using RecipeInfo;
using Microsoft.EntityFrameworkCore;
namespace BusinessLayer;

class Utils
{
    public List<RecipeTags> ValidateTags(List<string> tags)
    {
        //Make a list of all enums in recipeTags
        var enum_tags = Enum.GetValues(typeof(RecipeTags));
        List<string> string_enum_tags = Array.ConvertAll((RecipeTags[])enum_tags, item => item.ToString()).ToList();
        //Check if the input tags matches any of the available tags in the enums and adds it to a list
        var available_tags = string_enum_tags.Where(tag => tags.Contains(tag)).ToList();
        return Array.ConvertAll(available_tags.ToArray(), item => (RecipeTags)Enum.Parse(typeof(RecipeTags), item)).ToList();
    }
}
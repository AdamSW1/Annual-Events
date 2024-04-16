using BusinessLayer;
using RecipeInfo;
namespace BusinessLayer;

class Utils
{
    public List<RecipeTags> ValidateTags(List<string> tags)
    {
        //Make a list of all enums in recipeTags
        var enum_tags = Enum.GetValues(typeof(RecipeTags));
        List<string> string_enum_tags = enum_tags.Cast<string>().ToList();
        //Check if the input tags matches any of the available tags in the enums and adds it to a list
        var available_tags = string_enum_tags.Where(tag => tags.Contains(tag));
        return available_tags.Cast<RecipeTags>().ToList();
    }

    public double CheckInput() 
    {   
        double value = 0;
        try 
        {
            value = double.Parse(Console.ReadLine());
        }
        catch(FormatException) 
        {
            Console.WriteLine("input needs to be a number");
        }
        return value;
    }

}
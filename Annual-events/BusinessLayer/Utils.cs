using System.Text.RegularExpressions;
using BusinessLayer;
using RecipeInfo;
namespace BusinessLayer;

public class Utils
{
    //Validates the inputed string tags with the available tags and returns a list of the available tags
    public static List<RecipeTags> ValidateTags(List<string> tags)
    {
        //Make a list of all enums in recipeTags
        var enum_tags = Enum.GetValues(typeof(RecipeTags));
        List<string> string_enum_tags = Array.ConvertAll((RecipeTags[])enum_tags, item => item.ToString()).ToList();
        //Check if the input tags matches any of the available tags in the enums and adds it to a list
        var available_tags = string_enum_tags.Where(tag => tags.Contains(tag)).ToList();
        return Array.ConvertAll(available_tags.ToArray(), item => (RecipeTags)Enum.Parse(typeof(RecipeTags), item)).ToList();
    }

    public static string? GetUserChoice(string prompt, string[] options)
    {
        Console.WriteLine(prompt);
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i + 1} | {options[i]}");
        }

        string choice = Console.ReadLine();

        if (int.TryParse(choice, out int choice1))
        {
            return options[choice1 - 1];
        }
        else if (options.Contains(choice))
        {
            return choice;
        }
        else if (string.IsNullOrWhiteSpace(choice))
        {
            return null;
        }
        return null;
    }

    public static bool CheckDouble(double value)
    {
        if (value < 0) { return false; }
        return true;

    }
    public static bool CheckName(string name)
    {

        if (name.Length > 30) { return false; }
        return true;
    }

    public static bool CheckName100Limit(string name)
    {
        if (name.Length > 100) { return false; }
        return true;
    }

    public static bool CheckInt(int number)
    {
        if (number < 0) { return false; }

        return true;
    }
    public static bool CheckRatings(double rating)
    {
        if (rating < 0 || rating > 5){ return false; }
        return true;
    }
}
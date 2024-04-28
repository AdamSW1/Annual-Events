using System.Text.RegularExpressions;
using BusinessLayer;
using RecipeInfo;
namespace BusinessLayer;

public class Utils
{
    //Validates the inputed string tags with the available tags and returns a list of the available tags
    public static bool ValidateTags(List<RecipeTag> tags)
    {
        //Make a list of all enums in recipeTags
        List<RecipeTags> list_all_tags = Enum.GetValues(typeof(RecipeTags)).Cast<RecipeTags>().ToList();
        
        //turn enums into RecipeTag object
        List<RecipeTag> tag_objs = new();

        list_all_tags.ForEach(tag => {
            tag_objs.Add(new RecipeTag(tag.ToString()));
        });

        //check if passed tags are in the list
        bool validTags = true;
        tags.ForEach(tag =>{
            if(!tag_objs.Contains(tag)){
                validTags = false;
            }
        });

        return validTags;
    }

    public static string? GetUserChoice(string prompt, string[] options)
    {
        Console.WriteLine(prompt);
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i + 1} | {options[i]}");
        }

        string choice = Console.ReadLine() ?? " ";

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

    public static bool CheckString(string val){
        if (string.IsNullOrEmpty(val) || string.IsNullOrWhiteSpace(val)) { return false; }
        return true;
    }
    public static bool CheckName(string name)
    {
        if (name.Length > 30 || string.IsNullOrWhiteSpace(name)) { return false; }
        return name.Any(ch => char.IsLetterOrDigit(ch) );
    }

    public static bool CheckLongString(string val)
    {
        if (val.Length > 2000) { return false; }
        return true;
    }

    public static bool CheckInt(int number)
    {
        if (number < 0) { return false; }

        return true;
    }
    public static bool CheckScore(double rating)
    {
        if (rating < 0 || rating > 5){ return false; }
        return true;
    }

    public static bool CheckList<T>(List<T> list){

        if (list == null || list.Count == 0){ return false; }
        return true;
    }

    public static bool CheckMultiStringInput(string input){
        if (input == null || !input.Contains(',')){
            return false;
        }
        return true;
    } 

    public static bool CheckRecipeInList(List<Recipe> recipes, string nameToFind){
        return recipes.Any(r =>
        {
            string min = r.Name.ToLower().Trim();
            return min == nameToFind.ToLower().Trim();
        });
    }
}
using System.Text.RegularExpressions;
using BusinessLayer;
using RecipeInfo;
namespace BusinessLayer;

public class Utils
{
    //Validates the inputed string tags with the available tags and returns a list of the available tags
    public List<RecipeTags> ValidateTags(List<string> tags)
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

    public double CheckDouble() 
    {   
        double value = 0;
        bool isDone = false;
        while (isDone != true) 
        {
            try 
            {
                value = double.Parse(Console.ReadLine() ?? "null");

                if (value < 0) 
                {
                    throw new ArgumentException();
                }
                isDone = true;
            }
            catch(FormatException) 
            {
                Console.WriteLine("Value needs to be a number!");
                Console.WriteLine();
            }
            catch(ArgumentException) 
            {
                Console.WriteLine("Value cannot be negative!");
                Console.WriteLine();
            }
        }
        return value;
    }
    public string CheckName() 
    {
        string name = "";
        bool isDone = false;
        string regexPatterns = @"[^a-zA-Z0-9\s]";
        while (isDone != true) 
        {
            try 
            {
                name = Console.ReadLine();

                if (name.Length > 30 || Regex.IsMatch(name, regexPatterns)) 
                {
                    throw new ArgumentException();
                }
                isDone = true;
            }
            catch(ArgumentException) 
            {
                Console.WriteLine("Invalid name. Cannot contain special characters and length of name must be 30 maximum!");
                Console.WriteLine();
            }
        }
        return name;
    }

    public string CheckName100Limit() 
    {
        string check100Limit = "";
        bool isDone = false;
        while (isDone != true) 
        {
            try 
            {
                check100Limit = Console.ReadLine();

                if (check100Limit.Length > 100) 
                {
                    throw new ArgumentException();
                }
                isDone = true;
            }
            catch(ArgumentException) 
            {
                Console.WriteLine("Invalid name. 100 character maximum!");
                Console.WriteLine();
            }
        }
        return check100Limit;
    }
    
    public int CheckServings() 
    {   
        int servings = 0;
        bool isDone = false;
        while (isDone != true) 
        {
            try 
            {
                servings = int.Parse(Console.ReadLine() ?? "null");

                if (servings < 0) 
                {
                    throw new ArgumentException();
                }
                isDone = true;
            }
            catch(FormatException) 
            {
                Console.WriteLine("Servings needs to be an integer!");
                Console.WriteLine();
            }
            catch(ArgumentException) 
            {
                Console.WriteLine("Servings cannot be negative!");
                Console.WriteLine();
            }
        }
        return servings;
    }
    public double CheckRatings() 
    {   
        double ratings = 0;
        bool isDone = false;
        while (isDone != true) 
        {
            try 
            {
                ratings= double.Parse(Console.ReadLine() ?? "null");

                if (ratings < 0 || ratings > 5) 
                {
                    throw new ArgumentException();
                }
                isDone = true;
            }
            catch(FormatException) 
            {
                Console.WriteLine("Ratings need to be an integer!");
                Console.WriteLine();
            }
            catch(ArgumentException) 
            {
                Console.WriteLine("Ratings has to be between 0 and 5!");
                Console.WriteLine();
            }
        }
        return ratings;
    }
}
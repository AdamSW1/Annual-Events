using RecipeInfo;
namespace BusinessLayer;

class User {

    private string username;
	private string password;
	private string description;
	private int age;
	private string? profile_picture; // (GUI)

    private List<Recipe> recipes = new List<Recipe>();

    public string Username { get { return username; } }
    public string Description { get { return description; } }
    public int Age { get { return age; } }

    // constructor 
    public User(string username, string password, string description, int age) {
        this.username = username;
        this.password = password;
        this.description = description;
        this.age = age;
        hashPassword(password);
    }

    public void DisplayUserInfo()
    {
        Console.WriteLine($"Username: {username}");
        Console.WriteLine($"Description: {description}");
        Console.WriteLine($"Age: {age}");
    }
	public void AddToFavRecipe(Recipe recipe) {
        recipes.Add(recipe);
    } // lets them add a recipe to an [] list
	public void AddRecipe(Recipe recipe) {
        recipe.Owner = this;
        recipes.Add(recipe);
    } // just adding a recipe to a list, other than Fav
	public void RemoveFromFavRecipe(Recipe recipe) {
        recipes.Remove(recipe);
    } // removes a recipe from the [] list

    public void DisplayRecipes() {
        Console.WriteLine($"Recipes for user: {username}\n");
        foreach (var recipe in recipes)
        {
            recipe.DisplayRecipeInfo();
        }
    }
	public void StepVerification() {} // optional but asks them 2 specific infos
	public void hashPassword(string password) {
        // algo here
    } // hashes their password
	public bool Authentication(string enteredUsername, string enteredPassword) {
        return username == enteredUsername && password == enteredPassword;
    } // verifies their passwords and usernames with database
	public void DeleteAccount() {
        
    } // should delete their account definitely
}
using RecipeInfo;
namespace BusinessLayer;

class User {

    string username;
	string password;
	string ?description;
	int ?age;
	string ?profile_picture; // (GUI)

    // constructor 
    Recipe ?recipe;
    public User(string username, string password, string description, int age) {
        this.username = username;
        this.password = password;
        this.description = description;
        this.age = age;
    }
    
    // Basic methods for now
    public void CreateUser() {
        Console.WriteLine("Enter username:");
        string username = Console.ReadLine();
        Console.WriteLine("Enter a password:");
        string password = Console.ReadLine();
        Console.WriteLine("Enter description:");
        string description = Console.ReadLine();
        Console.WriteLine("Enter age:");
        int age = Convert.ToInt32(Console.ReadLine());

        User newUser = new User(username, password, description, age);
        hashPassword(password);
    } // creates a user based off their info input 


	public void AddToFavRecipe() {} // lets them add a recipe to an [] list
	public void AddRecipe() {} // just adding a recipe to a list, other than Fav
	public void RemoveFromFavRecipe() {} // removes a recipe from the [] list
	public void StepVerification() {} // optional but asks them 2 specific infos
	public void hashPassword(string password) {
        // algo here
    } // hashes their password
	public void Authentication() {} // verifies their passwords and usernames
	public void DeleteAccount() {
        Authentication();
        // if(Authentication()) {
        //     // if true then procede
        // }
    } // should delete their account definitely
}
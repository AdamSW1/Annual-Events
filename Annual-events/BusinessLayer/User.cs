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
        hashPassword(password);
    }


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


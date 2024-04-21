using System;
using System.Collections.Generic;
using RecipeInfo;
namespace BusinessLayer
{
    class AuthenticationManager
    {
        private static AuthenticationManager? _instance;
        public static AuthenticationManager Instance{
            get{
                _instance ??= new AuthenticationManager();
                return _instance;
            }
        }
        private static List<User> users = new List<User>();
        private static User? _currentUser;

        private AuthenticationManager()
        {
            // Test data for now, since we dont have a database.
            users.Add(new User("user1", "password1", "Description 1", 25));
            users.Add(new User("user2", "password2", "Description 2", 30));

        }

        public static User CurrentUser
        {
            get
            {
                if (_currentUser != null)
                {
                    return _currentUser;
                }
                throw new NotImplementedException();
            }
        }


        public static void AddUser(User user)
        {
            users.Add(user);
        }

        public static bool Login(string username, string password)
        {
            foreach (var user in users)
            {
                if (user.Authentication(username, password))
                {
                    _currentUser = user;
                    return true;
                }
            }
            return false;
        }

        public static void Logout()
        {
            _currentUser = null;
        }

        public static List<Recipe> GetAllRecipesFromAllUsers()
        {
            List<Recipe> allRecipes = new List<Recipe>();
            foreach (var user in users)
            {
                allRecipes.AddRange(user.Recipes);
            }
            return allRecipes;
        }
    }
}

using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    class AuthenticationManager
    {
        private List<User> users = new List<User>();
        private User? _currentUser;

        public AuthenticationManager()
        {
            // Test data for now, since we dont have a database.
            users.Add(new User("user1", "password1", "Description 1", 25));
            users.Add(new User("user2", "password2", "Description 2", 30));

        }

        public User CurrentUser 
        { 
            get { 
                    if(_currentUser != null){
                        return _currentUser; 
                    }
                    throw new NotImplementedException();
                }
            }

        public bool Login(string username, string password)
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

        public (string, string) InitLogin()
        {
            Console.WriteLine("Login:");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            return (username, password);
        }

        public void Logout()
        {
            _currentUser = null;
        }
    }
}

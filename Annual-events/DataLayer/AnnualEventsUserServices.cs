using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using RecipeInfo;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DataLayer
{
    public class AnnualEventsUserServices
    {
        private static AnnualEventsUserServices? _instance;
        private readonly AnnualEventsContext _dbContext;
        public static AnnualEventsUserServices Instance
        {
            get { return _instance ??= _instance = new AnnualEventsUserServices(AnnualEventsContext.Instance); }
        }

        public AnnualEventsContext DbContext = AnnualEventsContext.Instance;

        public AnnualEventsUserServices(AnnualEventsContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Users
        public void AddUser(Annual_Events_User user)
        {
            user.Password = HashPassword(user.Password);
            _dbContext.Annual_Events_User.Add(user);
            _dbContext.SaveChanges();
        }

        public void DeleteUser(Annual_Events_User user)
        {
            var userRecipes = _dbContext.Recipe?.Where(r => r.Owner == user).ToList();

            if (userRecipes != null && userRecipes.Any())
            {
                _dbContext.Recipe.RemoveRange(userRecipes);
            }

            _dbContext.Annual_Events_User.Remove(user);
            _dbContext.SaveChanges();
        }

        public void AddFavRecipes(Recipe favRecipe)
        {
            _dbContext.Recipe.Add(favRecipe);
            _dbContext.SaveChanges();
        }

        public Annual_Events_User GetUserByUsername(string username)
        {
            // Retrieve the user by username
            var user = _dbContext.Annual_Events_User.FirstOrDefault(u => u.Username == username);

            return user;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Compute hash from the password
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert hashed bytes to string
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        // Method to verify user login
        public bool VerifyLogin(string username, string password)
        {
            // Retrieve the user by username
            var user = GetUserByUsername(username);

            // If user not found or password is null, return false
            if (user == null || password == null)
            {
                return false;
            }

            // Retrieve the stored hashed password from the database
            string storedHashedPassword = user.Password;

            // Compare the provided password with the stored hashed password
            return VerifyPassword(password, storedHashedPassword);
        }

        // Method to compare hashed password with provided password
        private bool VerifyPassword(string inputPassword, string storedHashedPassword)
        {
            using (var sha256 = SHA256.Create())
            {
                // Compute hash from the input password
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));

                // Convert hashed bytes to string
                string hashedInputPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                // Compare the hashed input password with the stored hashed password
                return hashedInputPassword == storedHashedPassword;
            }
        }

    }
}

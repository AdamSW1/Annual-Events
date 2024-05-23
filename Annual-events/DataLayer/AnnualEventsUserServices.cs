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
            var userToDelete = _dbContext.Annual_Events_User
                .Include(u => u.Recipes)
                    .ThenInclude(r => r.Preparation)
                .Include(u => u.Recipes)
                    .ThenInclude(r => r.RecipeIngredients)
                .Include(u => u.Recipes)
                    .ThenInclude(r => r.Tags)
                .Include(u => u.Recipes)
                    .ThenInclude(r => r.Reviews)
                .Include(u => u.FavRecipes)
                .FirstOrDefault();


            _dbContext.Annual_Events_User.Remove(user);
            _dbContext.SaveChanges();
        }

        public void AddFavRecipes(Recipe favRecipe)
        {
            var recipeToadd = _dbContext.Recipe
            .Include(r => r.Tags)
            .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
            .Include(r => r.Reviews)
            .Include(r => r.FavouritedBy)
            .Include(r => r.Preparation)
            .FirstOrDefault();

            if (recipeToadd == null)
            {
                return;
            }
            bool isFavorited = _dbContext.Annual_Events_User
            .Any(u => u.FavRecipes.Contains(favRecipe));

            if (isFavorited)
            {
                return;
            }
            favRecipe!.AddFavourite();
            RecipeManager.AddToFavRecipe(AuthenticationManager.Instance.CurrentUser, recipeToadd);
            _dbContext.SaveChanges();
        }
        public void RemoveFavRecipes(Recipe favRecipe)
        {
            var recipeToadd = _dbContext.Recipe
            .Include(r => r.Tags)
            .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
            .Include(r => r.Reviews)
            .Include(r => r.FavouritedBy)
            .Include(r => r.Preparation)
            .FirstOrDefault();

            if (recipeToadd == null)
            {
                return;
            }
            bool isFavorited = _dbContext.Annual_Events_User
            .Any(u => u.FavRecipes.Contains(favRecipe));

            if (!isFavorited)
            {
                return;
            }
            favRecipe.RemoveFavourite();
            RecipeManager.DeleteFavRecipe(AuthenticationManager.Instance.CurrentUser, recipeToadd);
            _dbContext.SaveChanges();
        }
        public Annual_Events_User GetUserByUsername(string username)
        {
            // Retrieve the user by username
            var user = _dbContext.Annual_Events_User.FirstOrDefault(u => u.Username == username);

            return user;
        }

        private static readonly RandomNumberGenerator rng = new RNGCryptoServiceProvider();

        public string HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[16];
            rng.GetBytes(salt);

            // Create an array to hold the salt and password
            byte[] combined = new byte[salt.Length + Encoding.UTF8.GetBytes(password).Length];
            Buffer.BlockCopy(salt, 0, combined, 0, salt.Length);
            Buffer.BlockCopy(Encoding.UTF8.GetBytes(password), 0, combined, salt.Length, Encoding.UTF8.GetBytes(password).Length);

            // Compute the hash
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(combined);

                // Combine the salt and hash
                byte[] hashWithSalt = new byte[salt.Length + hashedBytes.Length];
                Buffer.BlockCopy(salt, 0, hashWithSalt, 0, salt.Length);
                Buffer.BlockCopy(hashedBytes, 0, hashWithSalt, salt.Length, hashedBytes.Length);

                // Convert to base64 string for storage
                return Convert.ToBase64String(hashWithSalt);
            }
        }

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

        public bool VerifyPassword(string inputPassword, string storedHashedPassword)
        {
            // Convert the stored hashed password from base64
            byte[] hashWithSalt = Convert.FromBase64String(storedHashedPassword);

            // Extract the salt from the stored hash
            byte[] salt = new byte[16];
            Buffer.BlockCopy(hashWithSalt, 0, salt, 0, salt.Length);

            // Combine the input password with the extracted salt
            byte[] combined = new byte[salt.Length + Encoding.UTF8.GetBytes(inputPassword).Length];
            Buffer.BlockCopy(salt, 0, combined, 0, salt.Length);
            Buffer.BlockCopy(Encoding.UTF8.GetBytes(inputPassword), 0, combined, salt.Length, Encoding.UTF8.GetBytes(inputPassword).Length);

            // Compute the hash of the input password with the salt
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(combined);

                // Compare the hashes
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    if (hashedBytes[i] != hashWithSalt[salt.Length + i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }


    }
}

using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using RecipeInfo;
using System.Linq;

namespace DataLayer
{
    public class AnnualEventsUserService
    {
        private static AnnualEventsUserService? _instance;

        public static AnnualEventsUserService Instance
        {
            get { return _instance ??= _instance = new AnnualEventsUserService(); }
        }

        public AnnualEventsContext DbContext = AnnualEventsContext.Instance;

        public AnnualEventsUserService() {}

        // Users
        public void AddUser(Annual_Events_User user)
        {
            DbContext.Annual_Events_User.Add(user);
            DbContext.SaveChanges();
        }

        public void DeleteUser(Annual_Events_User user)
        {
            var userRecipes = DbContext.Recipe.Where(r => r.Owner == user).ToList();
            var userFavRecipes = DbContext.Recipe.Where(r => r.FavouritedBy.Contains(user)).ToList();

            DbContext.Recipe.RemoveRange(userRecipes);
            DbContext.Recipe.RemoveRange(userFavRecipes);

            DbContext.Annual_Events_User.Remove(user);
            DbContext.SaveChanges();
        }
    }
}

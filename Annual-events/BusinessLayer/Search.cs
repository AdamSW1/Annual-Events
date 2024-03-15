namespace BusinessLayer;

class Search {
    private List<Recipe> recipes;

    public Search(List<Recipe> recipes)
    {
        this.recipes = recipes;
    }

    // Search recipes by keyword
    public List<Recipe> SearchRecipesByKeyword(string keyword)
    {
        throw new NotImplementedException();
    }

    // Search recipes by tags
    public List<Recipe> SearchRecipesByTags(List<string> tags)
    {
        throw new NotImplementedException();
    }

    // Search recipes by time constraint
    public List<Recipe> SearchRecipesByTimeConstraint()
    {
        throw new NotImplementedException();
    }

    // Search recipes by rating
    public List<Recipe> SearchRecipesByRating(int rating)
    {
        throw new NotImplementedException();
    }

    // Search recipes by servings constraint
    public List<Recipe> SearchRecipesByServings(int servings)
    {
        throw new NotImplementedException();
    }

    // Search recipes in favorites
    public List<Recipe> SearchRecipesInFavorites()
    {
        throw new NotImplementedException();
    }

    // Search recipes by owner username
    public List<Recipe> SearchRecipesByOwnerUsername(string ownerUsername)
    {
        throw new NotImplementedException();
    }
}
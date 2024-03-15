namespace BusinessLayer;

class Search {
    private string _keywords;
    public string Keywords { get; set; }

    public Search(string keywords)
    {
        Keywords = keywords;
    }

    public void SortByIngredients()
    {
        // Sort recipes by ingredients
        throw new NotImplementedException();
    }
    public void SortByRatings()
    {
        // Sort recipes by ratings
        throw new NotImplementedException();
    }
    public  void SortByCookingTime()
    {
        // Sort recipes by cooking time
        throw new NotImplementedException();
    }
    public void SortByKeyword()
    {
        // Sort recipes by keyword
        throw new NotImplementedException();
    }
    public void SortByRecipeInFavorites()
    {
        // Sort recipes by favorites
        throw new NotImplementedException();
    }
    public void SortByServings()
    {
        // Sort recipes by servings
        throw new NotImplementedException();
    }
    public void SortByOwnership()
    {
        // Sort recipes by ownership
        throw new NotImplementedException();
    }
}
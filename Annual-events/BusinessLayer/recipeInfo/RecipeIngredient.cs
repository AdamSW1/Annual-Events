namespace RecipeInfo;
public class RecipeIngredient{

    public int RecipeIngredientId { get; set; }
    public Recipe Recipe{get; set;}
    public Ingredient Ingredient{get; set;}

    public string Quantity { get; set;}

    public RecipeIngredient(){}
}
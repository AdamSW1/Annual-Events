using BusinessLayer;
using DataLayer;
namespace RecipeInfo;

public class Preparation{
    public int PreparationID{ get; set; }
    private int _stepNumber;

    public List<Recipe>? Recipes{ get; set; }

    public int StepNumber { 
        get { return _stepNumber;}
        set { _stepNumber = value; }
    }

    private  string _step;
    public string Step { 
        get { return _step;}
        set { _step = value; }
    }

    public Preparation(int stepNumber, string step){
        _stepNumber = stepNumber;
        _step = step;
    }

    public void AddToDatabase(){
        AnnualEventsService.Instance.AddPreparation(this);
    }

    public void RemoveFromDatabase(){
        AnnualEventsService.Instance.RemovePreparation(this);
    }
}
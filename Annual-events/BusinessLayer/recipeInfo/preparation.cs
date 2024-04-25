using BusinessLayer;
namespace RecipeInfo;

public class Preparation{
    public int PreparationID{ get; set; }
    private readonly int _stepNumber;

    public int StepNumber { get { return _stepNumber;}}

    private readonly string _step;
    public string Step { get { return _step;}}

    public Preparation(int stepNumber, string step){
        _stepNumber = stepNumber;
        _step = step;
    }
}
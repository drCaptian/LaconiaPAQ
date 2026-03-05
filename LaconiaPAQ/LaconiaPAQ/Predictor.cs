namespace LaconiaPAQ;

public class Predictor
{
    private readonly StateMap stateMap;

    public Predictor()
    {
        stateMap = new StateMap(1 << 16);
    }

    public int Predict(int context)
    {
        return stateMap.P(context);
    }

    public void Update(int context, int y)
    {
        stateMap.Update(y);
    }
}
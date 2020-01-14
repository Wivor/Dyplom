using System.Collections.Generic;

public class ReplayData
{
    public SaveData saveData;
    public List<int[]> steps;

    public ReplayData(SaveData saveData)
    {
        this.saveData = saveData;
        steps = new List<int[]>();
    }

    public void AddStep(int characterID, int actionID, int hexID)
    {
        int[] arr = { characterID, actionID, hexID };
        steps.Add(arr);
    }
}

using System.Collections.Generic;

public class ReplayData
{
    public readonly MapData saveData;
    public readonly List<int[]> steps;

    public ReplayData(MapData saveData)
    {
        this.saveData = saveData;
        steps = new List<int[]>();
    }

    public void AddStep(int characterId, int actionId, int hexId)
    {
        int[] arr = { characterId, actionId, hexId };
        steps.Add(arr);
    }
}

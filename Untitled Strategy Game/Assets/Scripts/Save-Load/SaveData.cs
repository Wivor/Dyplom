using System.Collections.Generic;
using System.Linq;

public class SaveData
{
    public int gridHeight;
    public int gridWidth;
    public Dictionary<int, Statistics> characters;
    public List<int> obstacles;

    public SaveData(int gridHeight, int gridWidth)
    {
        this.gridHeight = gridHeight;
        this.gridWidth= gridWidth;

        characters = Storage.characters.ToDictionary(character => character.transform.parent.GetComponent<Hex>().id, character => character.Statistics);
    }
}

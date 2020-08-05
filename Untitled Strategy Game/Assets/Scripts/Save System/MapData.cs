using System.Collections.Generic;
using System.Linq;

public class MapData
{
    public readonly int gridHeight;
    public readonly int gridWidth;
    public readonly Dictionary<int, Statistics> characters;
    public readonly List<int> obstacles;

    public MapData(int gridHeight, int gridWidth)
    {
        this.gridHeight = gridHeight;
        this.gridWidth= gridWidth;

        characters = Storage.characters.ToDictionary(character => character.transform.parent.GetComponent<Hex>().id, character => character.Statistics);
        obstacles = Storage.obstacles.Select(obstacle => obstacle.parent.GetComponent<Hex>().id).ToList();
    }
}

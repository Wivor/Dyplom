﻿using System.Collections.Generic;
using System.Linq;

public class MapData
{
    public int gridHeight;
    public int gridWidth;
    public Dictionary<int, Statistics> characters;
    public List<int> obstacles;

    public MapData(int gridHeight, int gridWidth)
    {
        this.gridHeight = gridHeight;
        this.gridWidth= gridWidth;

        characters = Storage.characters.ToDictionary(character => character.transform.parent.GetComponent<Hex>().id, character => character.Statistics);
        obstacles = Storage.obstacles.Select(obstacle => { return obstacle.parent.GetComponent<Hex>().id; }).ToList();
    }
}

using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Collections.Generic;

public class SaveMapManager : MonoBehaviour
{
    public GameObject characterPrefab;
    public GameObject obstaclePrefab;

    Grid grid;
    JsonSerializer serializer;

    private void Start()
    {
        grid = FindObjectOfType<Grid>();
        serializer = new JsonSerializer();
    }

    public void SaveMap(string saveName)
    {
        MapData saveData = new MapData(grid.GridHeight, grid.GridWidth);

        serializer.Converters.Add(new JavaScriptDateTimeConverter());
        serializer.NullValueHandling = NullValueHandling.Ignore;

        using (StreamWriter sw = new StreamWriter("saves/" + saveName))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            serializer.Serialize(writer, saveData);
        }
    }

    public MapData GetSaveData()
    {
        return new MapData(grid.GridHeight, grid.GridWidth);
    }

    public void LoadMap(string saveName)
    {
        grid.ClearGrid();

        MapData saveData;

        using (StreamReader sr = new StreamReader("saves/" + saveName))
        using (JsonReader reader = new JsonTextReader(sr))
        {
            saveData = serializer.Deserialize<MapData>(reader);
        }

        FindObjectOfType<Grid>().GenerateGrid(saveData.gridWidth, saveData.gridHeight);

        foreach (KeyValuePair<int, Statistics> entry in saveData.characters)
        {
            LoadCharacter(entry.Key, entry.Value);
        }

        foreach (int hexID in saveData.obstacles)
        {
            LoadObstacle(hexID);
        }
    }

    private void LoadObstacle(int hexID)
    {
        GameObject obstacle = Instantiate(obstaclePrefab);
        Hex hex = Storage.GetHexByID(hexID);
        obstacle.transform.position = hex.transform.position + new Vector3(0, 2, 0);
        obstacle.transform.parent = hex.transform;
        Storage.obstacles.Add(obstacle.transform);
    }

    private void LoadCharacter(int hexID, Statistics stats)
    {
        GameObject character = Instantiate(characterPrefab);
        Hex hex = Storage.GetHexByID(hexID);
        character.transform.position = hex.transform.position + new Vector3(0, 2, 0);
        character.transform.parent = hex.transform;
        character.GetComponent<Character>().Statistics = stats;
        FindObjectOfType<GameManager>().AddNewCharacter(character.GetComponent<Character>());

        if (stats.Team == "Team A")
            character.GetComponent<Renderer>().material = hex.TeamAmat;
        else
            character.GetComponent<Renderer>().material = hex.TeamBmat;
    }
}

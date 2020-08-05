using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Linq;

public class SaveMapManager : MonoBehaviour
{
    public GameObject characterPrefab;
    public GameObject obstaclePrefab;

    private Grid _grid;
    private JsonSerializer _serializer;

    private void Start()
    {
        _grid = FindObjectOfType<Grid>();
        _serializer = new JsonSerializer();
    }

    public void SaveMap(string saveName)
    {
        MapData saveData = new MapData(_grid.GridHeight, _grid.GridWidth);

        _serializer.Converters.Add(new JavaScriptDateTimeConverter());
        _serializer.NullValueHandling = NullValueHandling.Ignore;

        using (StreamWriter sw = new StreamWriter("saves/" + saveName))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            _serializer.Serialize(writer, saveData);
        }
    }

    public MapData GetSaveData()
    {
        return new MapData(_grid.GridHeight, _grid.GridWidth);
    }

    public void LoadMap(string saveName)
    {
        using (StreamReader sr = new StreamReader("saves/" + saveName))
        using (JsonReader reader = new JsonTextReader(sr))
        {
            LoadMap(_serializer.Deserialize<MapData>(reader));
        }
    }

    public void LoadMap(MapData mapData)
    {
        _grid.ClearGrid();
        FindObjectOfType<Grid>().GenerateGrid(mapData.gridWidth, mapData.gridHeight);

        foreach (var entry in mapData.characters)
        {
            LoadCharacter(entry.Key, entry.Value);
        }

        foreach (var hexId in mapData.obstacles)
        {
            LoadObstacle(hexId);
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

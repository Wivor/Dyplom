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

    private void LoadObstacle(int hexId)
    {
        Hex hex = Storage.GetHexById(hexId);
        GameObject obstacle = Instantiate(obstaclePrefab, hex.transform, true);
        obstacle.transform.position = hex.transform.position + new Vector3(0, 2, 0);
        Storage.obstacles.Add(obstacle.transform);
    }

    private void LoadCharacter(int hexId, Statistics stats)
    {
        Hex hex = Storage.GetHexById(hexId);
        GameObject character = Instantiate(characterPrefab, hex.transform, true);
        character.transform.position = hex.transform.position + new Vector3(0, 2, 0);
        character.GetComponent<Character>().statistics = stats;
        FindObjectOfType<GameManager>().AddNewCharacter(character.GetComponent<Character>());

        if (stats.Team == "Team A")
            character.GetComponent<Renderer>().material = hex.teamAmat;
        else
            character.GetComponent<Renderer>().material = hex.teamBmat;
    }
}

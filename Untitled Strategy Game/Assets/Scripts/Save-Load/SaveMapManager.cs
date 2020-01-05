using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Collections.Generic;

public class SaveMapManager : MonoBehaviour
{
    public GameObject characterPrefab;

    Grid grid;
    JsonSerializer serializer;

    private void Start()
    {
        grid = FindObjectOfType<Grid>();
        serializer = new JsonSerializer();
    }

    public void SaveMap()
    {
        SaveData saveData = new SaveData(grid.GridHeight, grid.GridWidth);

        serializer.Converters.Add(new JavaScriptDateTimeConverter());
        serializer.NullValueHandling = NullValueHandling.Ignore;

        using (StreamWriter sw = new StreamWriter("save.json"))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            serializer.Serialize(writer, saveData);
        }
    }

    public void LoadMap()
    {
        SaveData saveData;

        using (StreamReader sr = new StreamReader("save.json"))
        using (JsonReader reader = new JsonTextReader(sr))
        {
            saveData = serializer.Deserialize<SaveData>(reader);
        }

        FindObjectOfType<Grid>().GenerateGrid(saveData.gridWidth, saveData.gridHeight);

        foreach (KeyValuePair<int, Statistics> entry in saveData.characters)
        {
            LoadCharacter(entry.Key, entry.Value);
        }
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

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    public GameObject characterPrefab;
    public GameObject obstaclePrefab;

    int i = 0;
    Grid grid;
    ReplayData replayData;
    JsonSerializer serializer;
    Timer timer;

    private void Start()
    {
        grid = FindObjectOfType<Grid>();
        serializer = new JsonSerializer();
    }

    private void Update()
    {
        if (timer != null)
            timer.Update();
    }

    public void CreateReplayData(MapData mapData)
    {
        replayData = new ReplayData(mapData);
    }

    public void AddStep(int characterID, int actionID, int hexID)
    {
        replayData.AddStep(characterID, actionID, hexID);
    }

    public void SaveReplay(string replayName)
    {
        serializer.Converters.Add(new JavaScriptDateTimeConverter());
        serializer.NullValueHandling = NullValueHandling.Ignore;

        using (StreamWriter sw = new StreamWriter("replays/" + replayName))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            serializer.Serialize(writer, replayData);
        }
    }
    
    public void LoadReplay(string replayName)
    {
        grid.ClearGrid();

        using (StreamReader sr = new StreamReader("replays/" + replayName))
        using (JsonReader reader = new JsonTextReader(sr))
        {
            replayData = serializer.Deserialize<ReplayData>(reader);
        }

        FindObjectOfType<Grid>().GenerateGrid(replayData.saveData.gridWidth, replayData.saveData.gridHeight);

        foreach (KeyValuePair<int, Statistics> entry in replayData.saveData.characters)
        {
            LoadCharacter(entry.Key, entry.Value);
        }

        foreach (int hexID in replayData.saveData.obstacles)
        {
            LoadObstacle(hexID);
        }

        PlayReplay();
    }

    public void PlayReplay()
    {
        timer = new Timer(Time.deltaTime, 1, DoNextAction);
    }

    public void DoNextAction()
    {
        if (i < replayData.steps.Count)
        {
            GetComponent<GameManager>().TriggerActionInReplay(replayData.steps[i][0], replayData.steps[i][1], replayData.steps[i][2]);
            i++;
        }
        else
        {
            timer = null;
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

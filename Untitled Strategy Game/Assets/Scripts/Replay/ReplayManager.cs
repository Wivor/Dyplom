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
    private Grid _grid;
    private string _replayName;
    private ReplayData _replayData;
    private JsonSerializer _serializer;
    private Timer _timer;

    private void Start()
    {
        _grid = FindObjectOfType<Grid>();
        _serializer = new JsonSerializer();
    }

    private void Update()
    {
        if (_timer != null)
            _timer.Update();
    }

    public void CreateReplayData(string replayName, MapData mapData)
    {
        _replayName = replayName;
        _replayData = new ReplayData(mapData);
    }

    public void AddStep(int characterID, int actionID, int hexID)
    {
        _replayData?.AddStep(characterID, actionID, hexID);
    }

    public void SaveReplay()
    {
        if (_replayData == null)
            return;
        
        _serializer.Converters.Add(new JavaScriptDateTimeConverter());
        _serializer.NullValueHandling = NullValueHandling.Ignore;

        using (StreamWriter sw = new StreamWriter("replays/" + _replayName))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            _serializer.Serialize(writer, _replayData);
        }
    }
    
    public void LoadReplay(string replayName)
    {
        _grid.ClearGrid();

        using (StreamReader sr = new StreamReader("replays/" + replayName))
        using (JsonReader reader = new JsonTextReader(sr))
        {
            _replayData = _serializer.Deserialize<ReplayData>(reader);
        }

        FindObjectOfType<Grid>().GenerateGrid(_replayData.saveData.gridWidth, _replayData.saveData.gridHeight);

        foreach (KeyValuePair<int, Statistics> entry in _replayData.saveData.characters)
        {
            LoadCharacter(entry.Key, entry.Value);
        }

        foreach (int hexID in _replayData.saveData.obstacles)
        {
            LoadObstacle(hexID);
        }

        PlayReplay();
    }

    public void PlayReplay()
    {
        _timer = new Timer(Time.deltaTime, 1, DoNextAction);
    }

    public void DoNextAction()
    {
        if (i < _replayData.steps.Count)
        {
            GetComponent<GameManager>().TriggerAction(_replayData.steps[i][0], _replayData.steps[i][1], _replayData.steps[i][2]);
            i++;
        }
        else
        {
            _timer = null;
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

    public void ClearReplay()
    {
        _replayData = null;
    }
}

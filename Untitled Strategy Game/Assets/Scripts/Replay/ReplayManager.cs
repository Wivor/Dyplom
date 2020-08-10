using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    public GameObject characterPrefab;
    public GameObject obstaclePrefab;

    private int _i = 0;
    private Grid _grid;
    private string _replayName;
    private ReplayData _replayData;
    private JsonSerializer _serializer;
    private GameManager _gameManager;
    private Timer _timer;

    private void Start()
    {
        _gameManager = GetComponent<GameManager>();
        _grid = FindObjectOfType<Grid>();
        _serializer = new JsonSerializer();
    }

    private void Update()
    {
        _timer?.Update();
    }

    public void CreateReplayData(string replayName, MapData mapData)
    {
        _replayName = replayName;
        _replayData = new ReplayData(mapData);
    }

    public void AddStep(int characterId, int actionId, int hexId)
    {
        _replayData?.AddStep(characterId, actionId, hexId);
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

        foreach (int hexId in _replayData.saveData.obstacles)
        {
            LoadObstacle(hexId);
        }

        PlayReplay();
    }

    private void PlayReplay()
    {
        _timer = new Timer(Time.deltaTime, 1, DoNextAction);
    }

    private void DoNextAction()
    { 
        if (_i < _replayData.steps.Count)
        {
            _gameManager.TriggerAction(_replayData.steps[_i][0], _replayData.steps[_i][1], _replayData.steps[_i][2]);
            _i++;
        }
        else
        {
            _timer = null;
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
            character.GetComponent<Renderer>().material = hex.teamAMat;
        else
            character.GetComponent<Renderer>().material = hex.teamBMat;
    }

    public void ClearReplay()
    {
        _replayData = null;
    }
}

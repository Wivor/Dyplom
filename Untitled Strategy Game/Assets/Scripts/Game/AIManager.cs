using System;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public bool aiEnabled;

    private int _aiTurnNumber;
    private int _aiGamesNumber = 2;
    private int _aiCurrentGameNumber;
    private MapData _mapData;
    
    public int AiTurnNumber { get => _aiTurnNumber; set => _aiTurnNumber = value; }
    public int AiGamesNumber { get => _aiGamesNumber; set => _aiGamesNumber = value; }

    /*
     * Used at the start of the game if AI is enabled.
     * Saves MapData and starts LearningLoop.
     */
    public void StartGame()
    {
        _mapData = FindObjectOfType<SaveMapManager>().GetSaveData();
        LearningLoop();
    }

    /*
     * Replays game @_aiGamesNumber times.
     * For every game loads map, creates new replay object, moves characters based on their policy and returns to editor at the end.
     *
     * #TODO issues with loading map in loop. Characters are not loading properly? 
     */
    private void LearningLoop()
    {
        for (_aiCurrentGameNumber = 0; _aiCurrentGameNumber <= _aiGamesNumber; _aiCurrentGameNumber++)
        {
            if (_aiCurrentGameNumber != 0)
                FindObjectOfType<SaveMapManager>().LoadMap(_mapData);
            FindObjectOfType<ReplayManager>().CreateReplayData(DateTime.Now.ToString("dd-MM-yyyy_hhmmss_") + "no" + _aiCurrentGameNumber, _mapData);
            Storage.characters[FindObjectOfType<GameManager>().queue].GetComponent<Agent>().TakeAction();
            
            FindObjectOfType<GameManager>().StartEditor();
        }
    }
}

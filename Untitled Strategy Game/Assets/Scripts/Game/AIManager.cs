using System;
using System.Collections;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public bool aiEnabled;

    private int _aiNumberOfTurns;
    private int _aiNumberOfGames;
    private int _aiCurrentGameNumber;
    private int _step;
    private MapData _mapData;
    private GameManager _gameManager;
    private SaveMapManager _saveMapManager;
    private ReplayManager _replayManager;

    public int AiNumberOfTurns { get => _aiNumberOfTurns; set => _aiNumberOfTurns = value; }
    public int AiNumberOfGames { get => _aiNumberOfGames; set => _aiNumberOfGames = value; }

    private void Start()
    {
        _replayManager = FindObjectOfType<ReplayManager>();
        _saveMapManager = FindObjectOfType<SaveMapManager>();
        _gameManager = GetComponent<GameManager>();
    }

    /*
     * Checks if game was started with AI enabled. If yes starts game loop and disables checking.
     */
    private void Update()
    {
        if (!aiEnabled || _gameManager.gameState != State.GAME) return;
        enabled = false;
        StartGame();
    }

    /*
     * Used at the start of the game if AI is enabled.
     * Saves information about map to @_mapData for map loading after every game. Then starts LearningLoop.
     */
    private void StartGame()
    {
        _mapData = FindObjectOfType<SaveMapManager>().GetSaveData();
        _step = 1;
        StartCoroutine( LearningLoop());
    }

    /*
     * Replays game @_aiGamesNumber times.
     * For every game goes through following steps:
     * 0: Loads new map from @_mapData and then saits until map is fully loaded. Skipped at first
     *     loop as map is already loaded at loop start.
     * 1: Creates new object for replay of next game. Then starts the game and waits for it to load.
     * 2: Makes AI move based on its policy until game hits @_aiTurnNumber turn. @_aiTurnNumber is set by user in UI.
     * 3: Returns to editor and waits until it's loaded. Then increases @_aiCurrentGameNumber.
     * Steps are repeated until @_aiCurrentGameNumber hits @_aiGamesNumber. #TODO @_aiGamesNumber is set in editor.
     * At the end start checking for new loop request.
     */
    private IEnumerator LearningLoop()
    {
        while (_aiCurrentGameNumber != _aiNumberOfGames)
        {
            switch (_step)
            {
                case 0:
                    _saveMapManager.LoadMap(_mapData);
                    yield return new WaitUntil(() => Storage.characters.TrueForAll(character => character.isReady));
                    _step++;
                    break;
            
                case 1:
                    _replayManager.CreateReplayData(DateTime.Now.ToString("dd-MM-yyyy_hhmmss_") + "no" + _aiCurrentGameNumber, _mapData);
                    _gameManager.StartGame();
                    yield return new WaitUntil(() => _gameManager.gameState == State.GAME);
                    _step++;
                    break;
            
                case 2:
                    while (_gameManager.turn != _aiNumberOfTurns)
                        Storage.characters[_gameManager.queue].GetComponent<Agent>().TakeAction();
                    _step++;
                    break;
                
                case 3:
                    _gameManager.StartEditor();
                    yield return new WaitUntil(() => _gameManager.gameState == State.EDITOR);
                    _aiCurrentGameNumber++;
                    _step = 0;
                    break;
            }
        }
        enabled = true;
    }
}

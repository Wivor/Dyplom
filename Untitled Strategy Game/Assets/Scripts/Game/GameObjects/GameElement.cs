using UnityEngine;

/* 
 * Abstract class for elements that can be placed on the map such as characters or environment.
 */

public abstract class GameElement : MonoBehaviour
{
    public bool isReady = false;
    
    private GameManager _gameManager;
    private EditorStatistics _editorStatistics;
    private GameStatistics _gameStatistics;
    private MapEditor _mapEditor;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _editorStatistics = FindObjectOfType<EditorStatistics>();
        _gameStatistics = FindObjectOfType<GameStatistics>();
        _mapEditor = FindObjectOfType<MapEditor>();
        Init();
        isReady = true;
    }

    protected abstract void Init();
}

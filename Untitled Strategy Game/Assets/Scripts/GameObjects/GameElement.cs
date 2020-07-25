using UnityEngine;

/* 
 * Abstract class for elements that can be placed on the map such as characters or environment.
 */

public abstract class GameElement : MonoBehaviour
{
    protected GameManager GameManager;
    protected EditorStatistics EditorStatistics;
    protected GameStatistics GameStatistics;
    protected MapEditorManager MapEditorManager;

    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        EditorStatistics = FindObjectOfType<EditorStatistics>();
        GameStatistics = FindObjectOfType<GameStatistics>();
        MapEditorManager = FindObjectOfType<MapEditorManager>();
        Init();
    }

    protected abstract void Init();
}

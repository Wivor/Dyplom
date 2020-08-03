using UnityEngine;

/* 
 * Abstract class for elements that can be placed on the map such as characters or environment.
 */

public abstract class GameElement : MonoBehaviour
{
    protected GameManager GameManager;
    protected EditorStatistics EditorStatistics;
    protected GameStatistics GameStatistics;
    protected MapEditor mapEditor;

    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        EditorStatistics = FindObjectOfType<EditorStatistics>();
        GameStatistics = FindObjectOfType<GameStatistics>();
        mapEditor = FindObjectOfType<MapEditor>();
        Init();
    }

    protected abstract void Init();
}

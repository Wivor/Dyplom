using UnityEngine;

public abstract class GameElement : MonoBehaviour
{
    protected GameManager GameManager;
    protected EditorStatistics EditorStatistics;
    protected GameStatistics GameStatistics;
    protected MapEditorManager MapEditorManager;

    public string Name = "Blocc";
    public int CurrentHealth = 100;
    public int MaxHealth = 100;

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

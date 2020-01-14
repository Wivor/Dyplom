using UnityEngine;

public class Hex : MonoBehaviour
{
    public Material TeamAmat;
    public Material TeamBmat;

    public int id;
    public GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameObject.AddComponent<ClickableInEditor>();
    }

    // no longer relevant
    public bool IsOccupied()
    {
        if (transform.childCount == 0)
            return false;
        return true;
    }

    void OnEnable()
    {
        EventManager.OnGameStart += OnGameStart;
        EventManager.OnReplayStart += OnEditorStart;
    }

    void OnDisable()
    {
        EventManager.OnGameStart -= OnGameStart;
        EventManager.OnReplayStart -= OnEditorStart;
    }

    public void OnGameStart()
    {
        gameObject.AddComponent<ClickableInGame>();
        Destroy(GetComponent<ClickableInEditor>());
    }

    public void OnEditorStart()
    {
        gameObject.AddComponent<ClickableInReplay>();
        Destroy(GetComponent<ClickableInEditor>());
    }
}

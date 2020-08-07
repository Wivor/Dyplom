using UnityEngine;
using UnityEngine.Serialization;

/*
 * Class for every hexagonal tile on map.
 */

public class Hex : MonoBehaviour
{
    public Material teamAmat;
    public Material teamBmat;

    public int id;
    public GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameObject.AddComponent<ClickableInEditor>();
    }

    /*
     * Checks if hex is occupied by another character or obstacle.
     * 
     * @return bool     true if hex is occupied and false if not
     */

    public bool IsOccupied()
    {
        if (transform.GetComponentInChildren<Character>() != null || transform.GetComponentInChildren<Obstacle>() != null)
            return true;
        return false;
    }

    /*
     * All below adds clickable functionality depending on game state.
     */

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

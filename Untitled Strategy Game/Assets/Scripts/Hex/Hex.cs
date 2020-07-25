using UnityEngine;

/*
 * Class for every hexagonal tile on map.
 */

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

        /*
         * Or is it?
         * 
         * Checks if hex is occupied. Might cause conflicts with range indicators.
         * 
         * #TODO examin this
         */

    public bool IsOccupied()
    {
        if (transform.childCount == 0)
            return false;
        return true;
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

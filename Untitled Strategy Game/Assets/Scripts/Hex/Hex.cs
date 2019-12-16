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
        EventManager.OnGameStart += ChangeGameState;
    }

    void OnDisable()
    {
        EventManager.OnGameStart -= ChangeGameState;
    }

    public void ChangeGameState()
    {
        gameObject.AddComponent<ClickableInGame>();
        Destroy(GetComponent<ClickableInEditor>());
    }
}

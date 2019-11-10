using UnityEngine;

public class HexGame : MonoBehaviour
{
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        EventManager.OnGameStart += Enable;
    }

    public void OnMouseDown()
    {
        if (!isOccupied())
        {
            gameManager.currentTurnCharacters[0].transform.SetParent(transform, false);
            gameManager.EndTurn();
        }
    }

    public bool isOccupied()
    {
        if (transform.childCount == 0)
            return false;
        return true;
    }

    void OnDisable()
    {
        EventManager.OnGameStart -= Enable;
    }

    public void Enable()
    {
        enabled = true;
    }
}

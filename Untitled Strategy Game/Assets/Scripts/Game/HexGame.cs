﻿using UnityEngine;

public class HexGame : MonoBehaviour
{
    public int id;
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        EventManager.OnGameStart += Enable;
    }

    public void OnMouseDown()
    {
        if(enabled)
        {
            gameManager.TriggerAction(gameManager.currentTurnCharacters[0].id, gameManager.selectedActionID, id);
            //currentTurnCharacters[0].transform.SetParent(transform, false);
        }
    }

    public bool IsOccupied()
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

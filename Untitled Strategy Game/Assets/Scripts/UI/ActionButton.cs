﻿using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public Action Action;

    void Start()
    {
        GetComponentInChildren<Text>().text = Action.actionName.ToString();
        GetComponent<Image>().sprite = Action.artwork;
    }

    public void OnClick()
    {
        FindObjectOfType<GameManager>().OnActionPress(Action);
        Character character = Storage.characters[FindObjectOfType<GameManager>().Queue];
        Action.OnSelect(character, character.transform.parent.GetComponent<Hex>());
    }
}
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public Action action;

    void Start()
    {
        GetComponentInChildren<Text>().text = action.actionName.ToString();
        GetComponent<Image>().sprite = action.artwork;
    }

    public void OnClick()
    {
        FindObjectOfType<GameManager>().OnActionPress(action);
        Character character = Storage.characters[FindObjectOfType<GameManager>().queue];
        action.OnSelect(character, character.transform.parent.GetComponent<Hex>());
    }
}

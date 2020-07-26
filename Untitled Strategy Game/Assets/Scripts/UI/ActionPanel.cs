
using UnityEngine;

public class ActionPanel : MonoBehaviour
{
    public Transform ButtonPrefab;

    public void SetActions(Character character)
    {
        ClearPanel();
        AddActionstoPanel(character);
    }

    private void ClearPanel()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    private void AddActionstoPanel(Character character)
    {
        foreach (Action action in character.actions)
        {
            Transform button = Instantiate(ButtonPrefab);
            button.SetParent(transform);
            button.GetComponent<ActionButton>().Action = action;
        }
    }
}

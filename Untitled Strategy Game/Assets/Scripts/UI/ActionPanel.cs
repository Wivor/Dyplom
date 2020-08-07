
using UnityEngine;
using UnityEngine.Serialization;

public class ActionPanel : MonoBehaviour
{
    public Transform buttonPrefab;

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
            Transform button = Instantiate(buttonPrefab, transform, true);
            button.GetComponent<ActionButton>().action = action;
        }
    }
}

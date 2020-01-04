using UnityEngine;

public class ClickableInGame : MonoBehaviour
{
    public void OnMouseDown()
    {
        if (GetComponent<Hex>() != null)
        {
            Hex hex = GetComponent<Hex>();
            hex.gameManager.TriggerAction(hex.gameManager.currentTurnCharacters[0].id, hex.gameManager.selectedAction.id, hex.id);
        }
        else if (GetComponent<Character>() != null)
        {
            Character character = GetComponent<Character>();
            FindObjectOfType<GameStatistics>().SetCharacter(character);
        }
    }
}

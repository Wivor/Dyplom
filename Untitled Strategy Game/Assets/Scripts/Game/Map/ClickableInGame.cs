using UnityEngine;

public class ClickableInGame : MonoBehaviour
{

    /*
     * If state is in game and hex is pressed, selected action is executed.
     * 
     * If character is pressed its statistics are displayed in right corner bar.
     */

    public void OnMouseDown()
    {
        if (GetComponent<Hex>() != null)
        {
            Hex hex = GetComponent<Hex>();
            hex.gameManager.TriggerAction(Storage.characters[hex.gameManager.queue].id, hex.gameManager.selectedAction.id, hex.id);
        }
        else if (GetComponent<Character>() != null)
        {
            Character character = GetComponent<Character>();
            FindObjectOfType<GameStatistics>().SetCharacter(character);
        }
    }
}

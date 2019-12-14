using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableInGame : MonoBehaviour
{
    Hex hex;
    bool isHex = true;

    private void Start()
    {
        if (GetComponent<Hex>())
        {
            hex = GetComponent<Hex>();
        }
        else
        {
            isHex = false;
        }
    }

    public void OnMouseDown()
    {
        if (!isHex)
        {
            hex = transform.parent.GetComponent<Hex>();
        }
        else
        {
            hex.gameManager.TriggerAction(hex.gameManager.currentTurnCharacters[0].id, hex.gameManager.selectedAction.id, hex.id);
        }
    }
}

using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/*
 * Adds proper numbers on every hex at it creation.
 */

public class HexText : MonoBehaviour
{
    public Text idText;
    public Text positionText;

    private void Start()
    {
        idText.text = GetComponent<Hex>().id.ToString();
        positionText.text = GetComponent<Node>().position.ToString();
    }
}

using UnityEngine;
using UnityEngine.UI;

/*
 * Adds proper numbers on every hex at it creation.
 */

public class HexText : MonoBehaviour
{
    public Text IDText;
    public Text PositionText;

    void Start()
    {
        IDText.text = GetComponent<Hex>().id.ToString();
        PositionText.text = GetComponent<Node>().position.ToString();
    }
}

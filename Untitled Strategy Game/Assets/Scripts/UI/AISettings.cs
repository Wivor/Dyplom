using UnityEngine;
using UnityEngine.UI;

public class AISettings : MonoBehaviour
{
    public Toggle ToggleA;
    public Toggle ToggleB;
    public InputField TurnInput;

    void Start()
    {
        TurnInput.text = "2";
    }

    public void OnToggleA()
    {
        if (ToggleA.isOn)
            FindObjectOfType<GameManager>().AIenabled = true;
        else
            FindObjectOfType<GameManager>().AIenabled = false;
    }

    public void TurnChange()
    {
        FindObjectOfType<GameManager>().AITurnNumber = int.Parse(TurnInput.text);
    }
}

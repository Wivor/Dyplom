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
        FindObjectOfType<AIManager>().aiEnabled = ToggleA.isOn;
    }

    public void TurnChange()
    {
        FindObjectOfType<AIManager>().AiTurnNumber = int.Parse(TurnInput.text);
    }
}

using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AISettings : MonoBehaviour
{
    public Toggle toggleA;
    public Toggle toggleB;
    public InputField turnInput;

    void Start()
    {
        turnInput.text = "2";
    }

    public void OnToggleA()
    {
        FindObjectOfType<AIManager>().aiEnabled = toggleA.isOn;
    }

    public void TurnChange()
    {
        FindObjectOfType<AIManager>().AiTurnNumber = int.Parse(turnInput.text);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class AISettings : MonoBehaviour
{
    public Toggle toggleA;
    public Toggle toggleB;
    public InputField turnInput;
    public InputField gamesInput;

    void Start()
    {
        turnInput.text = "5";
        gamesInput.text = "10";
    }

    public void OnToggleA()
    {
        FindObjectOfType<AIManager>().aiEnabled = toggleA.isOn;
    }

    public void TurnChange()
    {
        FindObjectOfType<AIManager>().AiNumberOfTurns = int.Parse(turnInput.text);
    }

    public void GameChange()
    {
        FindObjectOfType<AIManager>().AiNumberOfGames = int.Parse(gamesInput.text);
    }
}

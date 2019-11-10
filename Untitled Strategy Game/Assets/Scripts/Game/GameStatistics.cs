using UnityEngine;
using UnityEngine.UI;

public class GameStatistics : MonoBehaviour
{
    public Text NameText;
    public Text HeathText;
    public Text AttackText;
    public Text InitiativeText;
    public Text RangeText;
    public Text MovementText;

    public void SetCharacter(Character character)
    {
        NameText.text = character.Name.ToString();
        HeathText.text = character.CurrentHealth.ToString() + " / " + character.MaxHealth.ToString();
        AttackText.text = character.Attack.ToString();
        InitiativeText.text = character.Initiative.ToString();
        RangeText.text = character.Range.ToString();
        MovementText.text = character.Movement.ToString();
    }

    public void SetObstacle(Obstacle obstacle)
    {
        NameText.text = obstacle.Name.ToString();
        HeathText.text = obstacle.CurrentHealth.ToString() + " / " + obstacle.MaxHealth.ToString();
        AttackText.text = "-";
        InitiativeText.text = "-";
        RangeText.text = "-";
        MovementText.text = "-";
    }
}

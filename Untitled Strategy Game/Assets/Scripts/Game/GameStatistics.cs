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
    public Text ActionPointsText;

    public void SetCharacter(Character character)
    {
        NameText.text = character.Statistics.Name.ToString();
        HeathText.text = character.Statistics.CurrentHealth.ToString() + " / " + character.Statistics.MaxHealth.ToString();
        AttackText.text = character.Statistics.Attack.ToString();
        InitiativeText.text = character.Statistics.Initiative.ToString();
        RangeText.text = character.Statistics.Range.ToString();
        MovementText.text = character.Statistics.Movement.ToString();
        ActionPointsText.text = character.Statistics.CurrentActionPoints.ToString();
    }

    public void SetObstacle(Obstacle obstacle)
    {
        NameText.text = "-";
        HeathText.text = "-";
        AttackText.text = "-";
        InitiativeText.text = "-";
        RangeText.text = "-";
        MovementText.text = "-";
        ActionPointsText.text = "-";
    }
}

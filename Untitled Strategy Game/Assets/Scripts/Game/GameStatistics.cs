using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameStatistics : MonoBehaviour
{
    public Text nameText;
    public Text heathText;
    public Text attackText;
    public Text initiativeText;
    public Text rangeText;
    public Text movementText;
    public Text actionPointsText;

    public void SetCharacter(Character character)
    {
        nameText.text = character.statistics.Name.ToString();
        heathText.text = character.statistics.CurrentHealth.ToString() + " / " + character.statistics.MaxHealth.ToString();
        attackText.text = character.statistics.Attack.ToString();
        initiativeText.text = character.statistics.Initiative.ToString();
        rangeText.text = character.statistics.Range.ToString();
        movementText.text = character.statistics.Movement.ToString();
        actionPointsText.text = character.statistics.CurrentActionPoints.ToString();
    }

    public void SetObstacle(Obstacle obstacle)
    {
        nameText.text = "-";
        heathText.text = "-";
        attackText.text = "-";
        initiativeText.text = "-";
        rangeText.text = "-";
        movementText.text = "-";
        actionPointsText.text = "-";
    }
}

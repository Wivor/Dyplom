using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EditorStatistics : MonoBehaviour
{
    public InputField nameText;
    public InputField heathText;
    public InputField attackText;
    public InputField initiativeText;
    public InputField rangeText;
    public InputField movementText;

    public void SetCharacter(Character character)
    {
        nameText.text = character.statistics.Name.ToString();
        heathText.text = character.statistics.MaxHealth.ToString();
        attackText.interactable = true;
        attackText.text = character.statistics.Attack.ToString();
        initiativeText.interactable = true;
        initiativeText.text = character.statistics.Initiative.ToString();
        rangeText.interactable = true;
        rangeText.text = character.statistics.Range.ToString();
        movementText.interactable = true;
        movementText.text = character.statistics.Movement.ToString();
    }

    public void SetObstacle(Obstacle obstacle)
    {
        nameText.text = "-";
        heathText.text = "-";
        attackText.interactable = false;
        attackText.text = "-";
        initiativeText.interactable = false;
        initiativeText.text = "-";
        rangeText.interactable = false;
        rangeText.text = "-";
        movementText.interactable = false;
        movementText.text = "-";
    }

    public void UpdateCharacterName()
    {
        FindObjectOfType<MapEditor>().selectedCharacter.statistics.Name = nameText.text;
    }

    public void UpdateCharacterHealth()
    {
        FindObjectOfType<MapEditor>().selectedCharacter.statistics.MaxHealth = int.Parse(heathText.text);
    }

    public void UpdateCharacterAttack()
    {
        GameElement element = FindObjectOfType<MapEditor>().selectedCharacter;
        if (element is Character)
            (element as Character).statistics.Attack = int.Parse(attackText.text);
    }

    public void UpdateCharacterInitiative()
    {
        GameElement element = FindObjectOfType<MapEditor>().selectedCharacter;
        if (element is Character)
            (element as Character).statistics.Initiative = int.Parse(initiativeText.text);
    }

    public void UpdateCharacterRange()
    {
        GameElement element = FindObjectOfType<MapEditor>().selectedCharacter;
        if (element is Character)
            (element as Character).statistics.Range = int.Parse(rangeText.text);
    }

    public void UpdateCharacterMovement()
    {
        GameElement element = FindObjectOfType<MapEditor>().selectedCharacter;
        if (element is Character)
            (element as Character).statistics.Movement = int.Parse(movementText.text);
    }
}

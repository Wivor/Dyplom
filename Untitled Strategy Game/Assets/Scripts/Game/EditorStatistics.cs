using UnityEngine;
using UnityEngine.UI;

public class EditorStatistics : MonoBehaviour
{
    public InputField NameText;
    public InputField HeathText;
    public InputField AttackText;
    public InputField InitiativeText;
    public InputField RangeText;
    public InputField MovementText;

    public void SetCharacter(Character character)
    {
        NameText.text = character.Statistics.Name.ToString();
        HeathText.text = character.Statistics.MaxHealth.ToString();
        AttackText.interactable = true;
        AttackText.text = character.Statistics.Attack.ToString();
        InitiativeText.interactable = true;
        InitiativeText.text = character.Statistics.Initiative.ToString();
        RangeText.interactable = true;
        RangeText.text = character.Statistics.Range.ToString();
        MovementText.interactable = true;
        MovementText.text = character.Statistics.Movement.ToString();
    }

    public void SetObstacle(Obstacle obstacle)
    {
        NameText.text = "-";
        HeathText.text = "-";
        AttackText.interactable = false;
        AttackText.text = "-";
        InitiativeText.interactable = false;
        InitiativeText.text = "-";
        RangeText.interactable = false;
        RangeText.text = "-";
        MovementText.interactable = false;
        MovementText.text = "-";
    }

    public void UpdateCharacterName()
    {
        FindObjectOfType<MapEditor>().SelectedCharacter.Statistics.Name = NameText.text;
    }

    public void UpdateCharacterHealth()
    {
        FindObjectOfType<MapEditor>().SelectedCharacter.Statistics.MaxHealth = int.Parse(HeathText.text);
    }

    public void UpdateCharacterAttack()
    {
        GameElement element = FindObjectOfType<MapEditor>().SelectedCharacter;
        if (element is Character)
            (element as Character).Statistics.Attack = int.Parse(AttackText.text);
    }

    public void UpdateCharacterInitiative()
    {
        GameElement element = FindObjectOfType<MapEditor>().SelectedCharacter;
        if (element is Character)
            (element as Character).Statistics.Initiative = int.Parse(InitiativeText.text);
    }

    public void UpdateCharacterRange()
    {
        GameElement element = FindObjectOfType<MapEditor>().SelectedCharacter;
        if (element is Character)
            (element as Character).Statistics.Range = int.Parse(RangeText.text);
    }

    public void UpdateCharacterMovement()
    {
        GameElement element = FindObjectOfType<MapEditor>().SelectedCharacter;
        if (element is Character)
            (element as Character).Statistics.Movement = int.Parse(MovementText.text);
    }
}

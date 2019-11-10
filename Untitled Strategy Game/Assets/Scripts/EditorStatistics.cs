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

    public void UpdateCharacterName()
    {
        FindObjectOfType<MapEditorManager>().SelectedCharacter.Name = NameText.text;
    }

    public void UpdateCharacterHealth()
    {
        FindObjectOfType<MapEditorManager>().SelectedCharacter.MaxHealth = int.Parse(HeathText.text);
    }

    public void UpdateCharacterAttack()
    {
        GameElement element = FindObjectOfType<MapEditorManager>().SelectedCharacter;
        if (element is Character)
            (element as Character).Attack = int.Parse(AttackText.text);
    }

    public void UpdateCharacterInitiative()
    {
        GameElement element = FindObjectOfType<MapEditorManager>().SelectedCharacter;
        if (element is Character)
            (element as Character).Initiative = int.Parse(InitiativeText.text);
    }

    public void UpdateCharacterRange()
    {
        GameElement element = FindObjectOfType<MapEditorManager>().SelectedCharacter;
        if (element is Character)
            (element as Character).Range = int.Parse(RangeText.text);
    }

    public void UpdateCharacterMovement()
    {
        GameElement element = FindObjectOfType<MapEditorManager>().SelectedCharacter;
        if (element is Character)
            (element as Character).Movement = int.Parse(MovementText.text);
    }
}

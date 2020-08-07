using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Pass")]
public class Pass : Action
{
    public override void Initialize(Character character)
    {

    }

    public override void OnSelect(Character character, Hex hex)
    {
        FindObjectOfType<GameManager>().TriggerAction(character.id, id, 0);
    }

    public override bool Use(Character character, Hex hex)
    {
        Debug.Log("ID " + character.id + " passed the turn.");
        character.statistics.CurrentActionPoints = 0;
        return true;
    }
}

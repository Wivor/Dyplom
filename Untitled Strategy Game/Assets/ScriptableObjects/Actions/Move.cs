using UnityEngine;

[CreateAssetMenu (menuName = "Actions/Move")]
public class MoveScript : Action
{
    public override void Initialize(Character character)
    {
        range = character.Movement;
    }

    public override void Use(Character character, GameObject hex)
    {
        character.transform.SetParent(hex.transform, false);
    }
}

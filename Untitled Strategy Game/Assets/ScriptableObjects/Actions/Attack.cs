using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Attack")]
public class Attack : Action
{
    public override void Initialize(Character character)
    {
        range = character.Range;
    }

    public override void Use(Character character, GameObject hex)
    {
        character.transform.SetParent(hex.transform, false);
    }
}

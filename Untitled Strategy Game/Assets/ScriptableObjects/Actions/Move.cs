using UnityEngine;

[CreateAssetMenu (menuName = "Actions/Move")]
public class Move : Action
{
    public override void Initialize(Character character)
    {
        range = character.Movement;
    }

    public override bool Use(Character character, HexGame hex)
    {
        if (!hex.isOccupied())
        {
            character.transform.SetParent(hex.transform, false);
            return true;
        }
        return false;
    }
}

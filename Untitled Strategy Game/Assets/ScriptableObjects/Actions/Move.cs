using UnityEngine;

[CreateAssetMenu (menuName = "Actions/Move")]
public class Move : Action
{
    public override void Initialize(Character character)
    {
        range = character.Movement;
    }

    public override void OnSelect(Character character, HexGame hex)
    {
        
    }

    public override bool Use(Character character, HexGame hex)
    {
        if (!hex.IsOccupied())
        {
            character.transform.SetParent(hex.transform, false);
            return true;
        }
        return false;
    }
}

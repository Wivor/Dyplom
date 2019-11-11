using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Attack")]
public class Attack : Action
{
    public override void Initialize(Character character)
    {
        range = character.Range;
    }

    public override bool Use(Character character, HexGame hex)
    {
        hex.GetComponentInChildren<Character>().CurrentHealth -= character.Attack;
        return true;
    }
}

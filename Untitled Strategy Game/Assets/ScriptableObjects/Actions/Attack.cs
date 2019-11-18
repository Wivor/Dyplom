using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Attack")]
[System.Serializable]
public class Attack : Action
{
    public Material inRangeMaterial;

    public override void Initialize(Character character)
    {
        range = character.Range;
    }

    public override void OnSelect(Character character, HexGame hex)
    {
        RangeFinder rangeFinder = new RangeFinder();
        Debug.Log("Range: " + range);
        rangeFinder.FindNodesInRange(hex.GetComponent<Node>(), range);
        foreach(Node node in rangeFinder.inRange)
        {
            node.GetComponent<Renderer>().material = inRangeMaterial;
        }
    }

    public override bool Use(Character character, HexGame hex)
    {
        hex.GetComponentInChildren<Character>().CurrentHealth -= character.Attack;
        return true;
    }
}

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
        Debug.Log("Range: " + range);
        foreach(Node node in RangeFinder.FindNodesInRange(hex.GetComponent<Node>(), range))
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

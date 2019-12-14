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
        inRange = MoveFinder.FindNodesInRange(hex.GetComponent<Node>(), range);
        foreach (Node node in inRange)
        {
            Transform indicator = Instantiate(hexPrefab) as Transform;
            indicator.position = node.transform.position;
            indicator.parent = node.transform;
            indicator.GetComponent<Renderer>().material = material;
        }
    }

    public override bool Use(Character character, HexGame hex)
    {
        if (hex.GetComponent<Node>().distance != 0)
        {
            character.transform.SetParent(hex.transform, false);
            return true;
        }
        return false;
    }
}

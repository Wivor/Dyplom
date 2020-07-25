using UnityEngine;

[CreateAssetMenu (menuName = "Actions/Move")]
public class Move : Action
{
    public override void Initialize(Character character)
    {
        
    }

    /*
     * Saves to inRange set of nodes that are in range of this action. Then adds to its hexes indicators for users as child objects with different color.
     */

    public override void OnSelect(Character character, Hex hex)
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

    /*
     * If clicked hex is in range moves character on it.
     * 
     * @character       character to move
     * @hex             hex to move on
     * 
     * @return bool     returns true if operation was successful or false if not
     */

    public override bool Use(Character character, Hex hex)
    {
        if (hex.GetComponent<Node>().distance != 0)
        {
            character.transform.SetParent(hex.transform, false);
            return true;
        }
        return false;
    }
}

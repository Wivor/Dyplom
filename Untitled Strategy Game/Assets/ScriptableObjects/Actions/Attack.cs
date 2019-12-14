using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Attack")]
[System.Serializable]
public class Attack : Action
{
    public override void Initialize(Character character)
    {
        range = character.Range;
    }

    public override void OnSelect(Character character, Hex hex)
    {
        inRange = RangeFinder.FindNodesInRange(hex.GetComponent<Node>(), range);
        foreach (Node node in inRange)
        {
            Transform indicator = Instantiate(hexPrefab) as Transform;
            indicator.position = node.transform.position;
            indicator.parent = node.transform;
            indicator.GetComponent<Renderer>().material = material;
        }
    }

    public override bool Use(Character character, Hex hex)
    {
        hex.GetComponentInChildren<Character>().DealDamage(character.Attack);
        return true;
    }
}

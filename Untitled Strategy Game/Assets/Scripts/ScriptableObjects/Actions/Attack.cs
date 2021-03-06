﻿using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Attack")]
[System.Serializable]
public class Attack : Action
{
    public override void Initialize(Character character)
    {
        range = character.statistics.Range;
    }

    /*
     * Saves to inRange set of nodes that are in range of this action. Then adds to its hexes indicators for users as child objects with different color.
     */

    public override void OnSelect(Character character, Hex hex)
    {
        inRange = RangeFinder.FindNodesInRange(hex.GetComponent<Node>(), range);
        foreach (Node node in inRange)
        {
            Transform indicator = Instantiate(hexPrefab, node.transform, true) as Transform;
            indicator.position = node.transform.position;
            indicator.GetComponent<Renderer>().material = material;
        }
    }

    /*
     * Attacks character on clicked hex if there is any.
     * 
     * @character       attacking character
     * @hex             clicked hex
     * 
     * @return bool     returns true if operation was successful or false if not
     */

    public override bool Use(Character character, Hex hex)
    {
        if (hex.GetComponentInChildren<Character>() != null)
        {
            Debug.Log("ID " + character.id + " attacked: " + hex.GetComponent<Node>().position.ToString());
            hex.GetComponentInChildren<Character>().DealDamage(character.statistics.Attack);
            return true;
        }
        Debug.LogWarning("-> FAILURE! ID " + character.id + " attack: " + hex.GetComponent<Node>().position.ToString());
        return false;
    }
}

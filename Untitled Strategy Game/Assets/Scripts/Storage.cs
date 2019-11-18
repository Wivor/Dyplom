using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public List<Action> actions;
    public List<Character> characters;
    public List<HexGame> hexes;

    public Action GetActionByName(string name)
    {
        return actions.Find(action => action.actionName.Contains(name));
    }

    public Character GetCharacterByID(int id)
    {
        return characters.Find(character => character.id == id);
    }

    public Action GetActionByID(int id)
    {
        return actions.Find(action => action.id == id);
    }

    public HexGame GetHexByID(int id)
    {
        return hexes.Find(hex => hex.id == id);
    }

    public HexGame GetHexByPosition(Node.Position position)
    {
        return hexes.Find(hex => hex.GetComponent<Node>().position.Equals(position));
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public List<Action> actions;
    public static List<Character> characters = new List<Character>();
    public static List<Transform> obstacles = new List<Transform>();
    public static List<Hex> hexes = new List<Hex>();

    public Action GetActionByName(string name)
    {
        return actions.Find(action => action.actionName.Contains(name));
    }

    public static Character GetCharacterById(int id)
    {
        return characters.Find(character => character.id == id);
    }

    public Action GetActionById(int id)
    {
        return actions.Find(action => action.id == id);
    }

    public static Hex GetHexById(int id)
    {
        return hexes.Find(hex => hex.id == id);
    }

    public static Hex GetHexByPosition(Node.Position position)
    {
        return hexes.Find(hex => hex.GetComponent<Node>().position.Equals(position));
    }

    public static void ClearStorage()
    {
        characters.Clear();
        obstacles.Clear();
        hexes.Clear();
    }
}

using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    public Transform hexPrefab;
    public Material material;

    public int id;
    public string actionName;
    public int cooldown = 0;
    public int range = 1;
    public int cost = 1;
    public Sprite artwork;

    protected HashSet<Node> inRange;

    public abstract void Initialize(Character character);
    public abstract void OnSelect(Character character, Hex hex);
    public abstract bool Use(Character character, Hex hex);

    public void OnDeselect()
    {
        if (inRange != null)
        {
            foreach (Node node in inRange)
            {
                node.distance = 0;
                foreach (Transform child in node.transform)
                {
                    if (child.tag == "RangeIndicator")
                        Destroy(child.gameObject);
                }
            }
            inRange.Clear();
        }
    }
}

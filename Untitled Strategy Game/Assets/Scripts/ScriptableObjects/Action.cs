using System.Collections.Generic;
using UnityEngine;

/*
 * ScriptableObject for actions that character can take during the game.
 */

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

    /*
    * Method used to initialize it's statistics.
    * 
    * @character       owner of the action
    * 
    * #TODO action statistics are probably not unique to character - to check
    */

    public abstract void Initialize(Character character);

    /*
    * Method called when action is being selected.
    * 
    * @character       current moving character
    * @hex             position of current moving character (#TODO could be taken in method from character)
    */

    public abstract void OnSelect(Character character, Hex hex);

    /*
     * Method called when action is being used. Executes action efects and returns if action could be used.
     * 
     * @character       executor of the action
     * @hex             target of the action
     * 
     * @return bool     true if action was possible to use and false if not
     */

    public abstract bool Use(Character character, Hex hex);

    /*
     * Method called when action is deselected during game. Removes range indicators from the map.
     */

    public void OnDeselect()
    {
        if (inRange != null)
        {
            foreach (Node node in inRange)
            {
                node.distance = 0;
                foreach (Transform child in node.transform)
                {
                    if (child.CompareTag("RangeIndicator"))
                        Destroy(child.gameObject);
                }
            }
            inRange.Clear();
        }
    }
}

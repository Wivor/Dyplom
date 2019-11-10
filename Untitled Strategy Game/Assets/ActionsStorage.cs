using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsStorage : MonoBehaviour
{
    public List<Action> actions;

    public Action GetActionByName(string name)
    {
        return actions.Find(action => action.actionName.Contains(name));
    }
}

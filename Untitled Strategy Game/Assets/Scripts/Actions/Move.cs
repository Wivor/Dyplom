using UnityEngine;

public class Move : Action
{
    public Move()
    {
        actionName = "Move";
        id = 0;
        cooldown = 0;

        artwork = Resources.Load<Sprite>("Images/cube");
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}

using UnityEngine;

public class Attack : Action
{
    public Attack()
    {
        actionName = "Attack";
        id = 0;
        cooldown = 0;

        artwork = Resources.Load<Sprite>("Images/cube");
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}

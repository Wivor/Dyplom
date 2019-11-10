using UnityEngine;

public abstract class Action
{
    public string actionName;
    public int id;
    public int cooldown;
    public int cooldownLeft = 0;

    public Sprite artwork;

    public abstract void Use();
}

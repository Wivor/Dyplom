using UnityEngine;

public abstract class Action : ScriptableObject
{
    public int id;
    public string actionName;
    public int cooldown = 0;
    public int range = 1;
    public Sprite artwork;

    public abstract void Initialize(Character character);
    public abstract bool Use(Character character, HexGame hex);
}

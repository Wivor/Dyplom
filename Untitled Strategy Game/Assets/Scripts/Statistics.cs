using System.Collections.Generic;

public class Statistics
{
    public string Name;
    public int CurrentHealth;
    public int MaxHealth;
    public int Attack;
    public int Initiative;
    public int Range;
    public int Movement;

    public Statistics()
    {
        Name = "";
        CurrentHealth = 100;
        MaxHealth = 100;
        Attack = 10;
        Initiative = 10;
        Range = 1;
        Movement = 3;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Character : GameElement
{
    public int TeamID;
    public int Queue;

    public List<Action> actions = new List<Action>();

    public Statistics Statistics = new Statistics();

    public int id;

    protected override void Init()
    {
        if (TeamID == 0)
        {
            GetComponent<Agent>().SetType(1);
        }
        else
        {
            GetComponent<Agent>().SetType(2);
        }

        Action move = FindObjectOfType<Storage>().GetActionByName("Move");
        actions.Add(move);

        Action attack = FindObjectOfType<Storage>().GetActionByName("Attack");
        actions.Add(attack);

        Action pass = FindObjectOfType<Storage>().GetActionByName("Pass");
        actions.Add(pass);
    }

    public void InitializeActions()
    {
        foreach (Action action in actions)
        {
            action.Initialize(this);
        }
    }

    public Action GetActionByID(int id)
    {
        return actions.Find(action => action.id == id);
    }

    public void DealDamage(int dmg)
    {
        Statistics.CurrentHealth -= dmg;
        if (Statistics.CurrentHealth <= 0)
        {
            FindObjectOfType<GameManager>().DestroyCharacter(this);
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        EventManager.OnGameStart += OnGameStart;
        EventManager.OnReplayStart += OnReplayStart;
    }

    public void OnGameStart()
    {
        gameObject.AddComponent<ClickableInGame>();
    }

    public void OnReplayStart()
    {
        gameObject.AddComponent<ClickableInReplay>();
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Character : GameElement
{
    public int Queue;

    public List<Action> actions = new List<Action>();

    public Statistics Statistics = new Statistics();

    public int id;
    public string Owner;

    protected override void Init()
    {
        Action move = FindObjectOfType<Storage>().GetActionByName("Move");
        actions.Add(move);

        Action attack = FindObjectOfType<Storage>().GetActionByName("Attack");
        actions.Add(attack);
    }

    public void InitializeActions()
    {
        foreach (Action action in actions)
        {
            action.Initialize(this);
        }
    }

    public Action getActionByID(int id)
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
        EventManager.OnGameStart += ChangeGameState;
    }

    public void ChangeGameState()
    {
        gameObject.AddComponent<ClickableInGame>();
    }
}

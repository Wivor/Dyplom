using System.Collections.Generic;
using UnityEngine.Serialization;

/*
 * Class for moveable characters for players.
 */

public class Character : GameElement
{
    public int teamId;
    public int queue;

    public List<Action> actions = new List<Action>();

    public Statistics statistics = new Statistics();

    public int id;

    /* 
     * Method Init is called at the creation of the character.
     * Agent type is asigned to Agent component.
     * Basic actions are added to actions list. 
     */

    protected override void Init()
    {
        GetComponent<Agent>().SetType(teamId == 0 ? 1 : 2);

        Action move = FindObjectOfType<Storage>().GetActionByName("Move");
        actions.Add(move);

        Action attack = FindObjectOfType<Storage>().GetActionByName("Attack");
        actions.Add(attack);

        Action pass = FindObjectOfType<Storage>().GetActionByName("Pass");
        actions.Add(pass);
    }

    /*
     * Calls Initialize method for every action character has right before start of the game or replay.
     */

    public void InitializeActions()
    {
        foreach (Action action in actions)
        {
            action.Initialize(this);
        }
    }
    
    /*
     * Used to get one of actions in actions list.
     * 
     * @id int          id of the wanted action
     * 
     * @return Action   Action with the given id
     */

    public Action GetActionById(int id)
    {
        return actions.Find(action => action.id == id);
    }

    /*
     * Substracts given number from CurrentHealth in Statistics. Used when character is taking damage.
     * When CurrentHeath drops to 0 or below character object is destroyed.
     * 
     * @dmg int     damage taken
     */

    public void DealDamage(int dmg)
    {
        statistics.CurrentHealth -= dmg;
        if (statistics.CurrentHealth <= 0)
        {
            FindObjectOfType<GameManager>().DestroyCharacter(this);
            Destroy(gameObject);
        }
    }

    /*
     * Adds events that are called on start of the game or replay.
     */

    private void OnEnable()
    {
        EventManager.OnGameStart += OnGameStart;
        EventManager.OnReplayStart += OnReplayStart;
    }

    private void OnGameStart()
    {
        gameObject.AddComponent<ClickableInGame>();
    }

    private void OnReplayStart()
    {
        gameObject.AddComponent<ClickableInReplay>();
    }
}

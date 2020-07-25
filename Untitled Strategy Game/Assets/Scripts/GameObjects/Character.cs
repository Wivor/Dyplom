using System.Collections.Generic;

/*
 * Class for moveable characters for players.
 */

public class Character : GameElement
{
    public int TeamID;
    public int Queue;

    public List<Action> actions = new List<Action>();

    public Statistics Statistics = new Statistics();

    public int id;

    /* 
     * Method Init is called at the creation of the character.
     * Agent type is asigned to Agent component.
     * Basic actions are added to actions list. 
     */

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

    public Action GetActionByID(int id)
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
        Statistics.CurrentHealth -= dmg;
        if (Statistics.CurrentHealth <= 0)
        {
            FindObjectOfType<GameManager>().DestroyCharacter(this);
            Destroy(gameObject);
        }
    }

    /*
     * Adds events that are called on start of the game or replay.
     */

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

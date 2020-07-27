using UnityEngine;

public class GameManager : MonoBehaviour
{
    Storage storage;

    public Canvas EditorCanvas;
    public Canvas GameCanvas;
    public Canvas ReplayCanvas;

    public int CharacterID = 0;
    public Action selectedAction;

    public bool AIenabled = false;
    public int turn = 1;

    QueueManager QueueManager;

    bool ReplayPlaying = false;

    /*
     * Enables editor canvas and sets turn to 1.
     * 
     * #TODO InitializeActions doesn't really initialize anything here
     */

    void Start()
    {
        storage = FindObjectOfType<Storage>();
        QueueManager = GetComponent<QueueManager>();
        QueueManager.Initialize();
        
        GameCanvas.enabled = false;
        EditorCanvas.enabled = true;

        FindObjectOfType<TurnChanger>().TurnText.text = "Turn 1";

        InitializeActions();
    }

    /*
     * Called when game starts.
     * Hides editor canvas and enables game canvas, calls EventManager which makes characters clickable,
     * randomizes initiative for characters, sorts the queue by the result, creates replay object,
     * sets character on top bar in move order, sets actions on bottom bar for the first moving character.
     */

    public void StartGame()
    {
        EditorCanvas.enabled = false;
        GameCanvas.enabled = true;
        GetComponent<ReplayManager>().CreateReplayData(GetComponent<SaveMapManager>().GetSaveData());
        
        QueueManager.StartGame();

        FindObjectOfType<TopCharacterPanel>().OnStart(Storage.characters);
        FindObjectOfType<ActionPanel>().SetActions(Storage.characters[0]);
        EventManager.GameStartTrigger();
    }

    /*
     * Called when replay starts.
     * Hides editor canvas and enables game canvas, loads replay file, calls EventManager which makes characters clickable,
     * initializes characters actions, sorts characters by initiative, sets character on top bar in move order, disables bottom bar.
     * 
     * #TODO InitializeActions?
     */

    public void StartReplay()
    {
        EditorCanvas.enabled = false;
        GameCanvas.enabled = true;

        ReplayPlaying = true;
        AIenabled = false;

        GetComponent<ReplayManager>().LoadReplay("test");
        EventManager.ReplayStartTrigger();

        QueueManager.StartReplay();
        
        FindObjectOfType<TopCharacterPanel>().OnStart(Storage.characters);
        FindObjectOfType<ActionPanel>().enabled = false;
    }

    /*
     * Removes character from storage and destroys its object.
     * 
     * @character character to destroy
     */

    public void DestroyCharacter(Character character)
    {
        Storage.characters.Remove(character);
        FindObjectOfType<TopCharacterPanel>().RemoveCharacter(character);
    }

    /*
     * Called when action is being used.
     * Takes objects of character, action and hex from storage basing on ids given. Executes action and if action was successfuly used substracts character action points.
     * If character is out of action point its turn is ended.
     * 
     * @characterID     id of character executing action
     * @actionID        id of action being used
     * @hexID           id of targeted hex
     */

    public void TriggerAction(int characterID, int actionID, int hexID)
    {
        Character character = Storage.GetCharacterByID(characterID);
        Action action = FindObjectOfType<Storage>().GetActionByID(actionID);
        Hex hex = Storage.GetHexByID(hexID);

        if (character.GetActionByID(actionID).Use(character, hex))
        {
            if (!ReplayPlaying)
            {
                GetComponent<ReplayManager>().AddStep(characterID, actionID, hexID);
            }
            character.Statistics.CurrentActionPoints -= action.cost;
            character.GetActionByID(actionID).OnDeselect();

            if (character.Statistics.CurrentActionPoints == 0)
                QueueManager.EndTurn();
            else if (AIenabled)
                Storage.characters[QueueManager.Queue].GetComponent<Agent>().TakeAction();
        }
    }

    /*
     * Called when action is pressed on UI.
     * 
     * @action      action being pressed. Button on UI holds refference to it
     */

    public void OnActionPress(Action action)
    {
        if(selectedAction != null)
            selectedAction.OnDeselect();
        selectedAction = action;
    }

    /*
     * Called on creation of new character.
     * Adds new character to storage.
     * 
     * @character       character to add
     */

    public void AddNewCharacter(Character character)
    {
        character.id = CharacterID;
        character.Statistics.Name = CharacterID.ToString();
        Storage.characters.Add(character);
        CharacterID++;
    }

    /*
     * Sets statistics of action based on character statistics.
     */

    private static void InitializeActions()
    {
        foreach (Character character in Storage.characters)
        {
            foreach (Action action in character.actions)
            {
                action.Initialize(character);
            }
        }
    }

    public int getQueue()
    {
        return QueueManager.Queue;
    }
}

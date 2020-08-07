using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Canvas editorCanvas;
    public Canvas gameCanvas;
    public GameObject actionsPanel;
    public Canvas replayCanvas;

    public int queue;
    public int characterId = 0;
    public Action selectedAction;

    private int _turn = 1;
    private bool _replayPlaying = false;

    /*
     * Enables editor canvas and sets turn to 1.
     * 
     * #TODO InitializeActions doesn't really initialize anything here
     */

    void Start()
    {
        gameCanvas.enabled = false;
        editorCanvas.enabled = true;

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
        editorCanvas.enabled = false;
        gameCanvas.enabled = true;

        EventManager.GameStartTrigger();

        Storage.characters.ForEach(character => 
        {
            character.statistics.Initiative += Random.Range(-10, 10);
            character.InitializeActions();
        });

        SortCharactersByInitiative(ref Storage.characters);
        AddQueueNumberTo(Storage.characters);

        FindObjectOfType<TopCharacterPanel>().OnStart(Storage.characters);
        FindObjectOfType<ActionPanel>().SetActions(Storage.characters[0]);
        
        if (GetComponent<AIManager>().aiEnabled)
            GetComponent<AIManager>().StartGame();
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
        editorCanvas.enabled = false;
        gameCanvas.enabled = true;

        _replayPlaying = true;

        Storage.characters.ForEach(character =>
        {
            character.InitializeActions();
        });

        SortCharactersByInitiative(ref Storage.characters);
        AddQueueNumberTo(Storage.characters);
        
        actionsPanel.SetActive(false);
    }

    /*
     * Returns to Editor. Disables game canvas and enables editor one. Clears Storage and map. Resets game values.
     */

    public void StartEditor()
    {
        editorCanvas.enabled = true;
        gameCanvas.enabled = false;
        actionsPanel.SetActive(true);

        _replayPlaying = false;
        characterId = 0;
        queue = 0;
        _turn = 1;

        Storage.ClearStorage();
        FindObjectOfType<Grid>().ClearGrid();
        EventManager.ClearEvents();
        FindObjectOfType<ReplayUI>().OnEditorStart();
    }

    /*
     * Called at the end of character turn.
     * Adds number to queue, saves move to replay file, deselects action taken, if character was last resets queue,
     * sets bottom bar for next character, moves characters in top bar.
     */

    public void EndTurn()
    {
        queue++;
        if (!_replayPlaying)
            GetComponent<ReplayManager>().SaveReplay();

        if (selectedAction != null)
        {
            selectedAction.OnDeselect();
        }
        
        if (queue == Storage.characters.Count)
        {
            queue = 0;
            _turn++;
            FindObjectOfType<TurnChanger>().TurnText.text = "Turn " + _turn;

            Storage.characters.ForEach(character => character.statistics.CurrentActionPoints = character.statistics.ActionPoints);
        }
        if (!_replayPlaying)
            FindObjectOfType<ActionPanel>().SetActions(Storage.characters[queue]);
        FindObjectOfType<TopCharacterPanel>().UpdateTopBar();

        //infinite queue
        if (GetComponent<AIManager>().aiEnabled && !_replayPlaying)
        {
            if(_turn <= GetComponent<AIManager>().AiTurnNumber)
            {
                Storage.characters[queue].GetComponent<Agent>().TakeAction();
            }
        }
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

    public void TriggerAction(int characterId, int actionID, int hexID)
    {
        Character character = Storage.GetCharacterById(characterId);
        Action action = FindObjectOfType<Storage>().GetActionById(actionID);
        Hex hex = Storage.GetHexById(hexID);

        if (character.GetActionById(actionID).Use(character, hex))
        {
            if (!_replayPlaying)
            {
                GetComponent<ReplayManager>().AddStep(characterId, actionID, hexID);
            }
            character.statistics.CurrentActionPoints -= action.cost;
            character.GetActionById(actionID).OnDeselect();

            if (character.statistics.CurrentActionPoints == 0)
                EndTurn();
            else if (GetComponent<AIManager>().aiEnabled && !_replayPlaying)
                Storage.characters[queue].GetComponent<Agent>().TakeAction();
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
        character.id = characterId;
        character.statistics.Name = characterId.ToString();
        Storage.characters.Add(character);
        characterId++;
    }

    /*
     * Adds move order number to characters.
     */
    
    private void AddQueueNumberTo(List<Character> characters)
    {
        int i = 0;
        foreach (Character character in characters)
        {
            character.queue = i;
            i++;
        }
    }

    /*
     * Sorts character list by their initiative and then reverses it.
     * 
     * @characters      list of characters to sort.
     * 
     * #TODO could just use descending sort to be honest
     */

    private void SortCharactersByInitiative(ref List<Character> characters)
    {
        characters = characters.OrderBy(character => character.statistics.Initiative).ToList();
        characters.Reverse();
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
}

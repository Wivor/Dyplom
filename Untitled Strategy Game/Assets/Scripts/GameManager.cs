using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    Storage storage;

    public List<Character> currentTurnCharacters = new List<Character>();
    List<Character> nextTurnCharacters = new List<Character>();

    public Canvas EditorCanvas;
    public Canvas GameCanvas;
    public Canvas ReplayCanvas;

    public int Queue;
    public int CharacterID;
    public Action selectedAction;

    int turn;

    void Start()
    {
        storage = FindObjectOfType<Storage>();

        CharacterID = 0;
        turn = 1;
        GameCanvas.enabled = false;
        EditorCanvas.enabled = true;

        FindObjectOfType<TurnChanger>().TurnText.text = "Turn 1";

        InitializeActions();
    }

    public void StartGame()
    {
        EditorCanvas.enabled = false;
        GameCanvas.enabled = true;

        EventManager.GameStartTrigger();

        Storage.characters.ForEach(character => 
        {
            character.Statistics.Initiative += Random.Range(-10, 10);
            character.InitializeActions();
        });

        currentTurnCharacters.AddRange(Storage.characters);

        SortCharactersByInitiative(ref currentTurnCharacters);
        AddQueueTo(currentTurnCharacters);

        GetComponent<ReplayManager>().CreateReplayData(GetComponent<SaveMapManager>().GetSaveData());
        FindObjectOfType<TopCharacterPanel>().OnStart(currentTurnCharacters);
        FindObjectOfType<ActionPanel>().SetActions(currentTurnCharacters[0]);
    }

    public void StartReplay()
    {
        EditorCanvas.enabled = false;
        GameCanvas.enabled = true;

        GetComponent<ReplayManager>().LoadReplay("test");
        EventManager.ReplayStartTrigger();

        Storage.characters.ForEach(character =>
        {
            character.InitializeActions();
        });

        currentTurnCharacters.AddRange(Storage.characters);

        SortCharactersByInitiative(ref currentTurnCharacters);
        AddQueueTo(currentTurnCharacters);
        
        FindObjectOfType<TopCharacterPanel>().OnStart(currentTurnCharacters);
        FindObjectOfType<ActionPanel>().enabled = false;
    }

    public void EndTurn()
    {
        GetComponent<ReplayManager>().SaveReplay("test");
        if (selectedAction != null)
        {
            selectedAction.OnDeselect();
        }
        FindObjectOfType<ActionPanel>().SetActions(currentTurnCharacters[0]);

        currentTurnCharacters.Remove(currentTurnCharacters.First());
        
        nextTurnCharacters = Storage.characters;
        SortCharactersByInitiative(ref nextTurnCharacters);
        FindObjectOfType<TopCharacterPanel>().SetTopBar(currentTurnCharacters, nextTurnCharacters);
        
        if (currentTurnCharacters.Count == 0)
        {
            currentTurnCharacters = nextTurnCharacters;
            AddQueueTo(currentTurnCharacters);
            FindObjectOfType<TopCharacterPanel>().OnStart(currentTurnCharacters);
            
            turn++;
            FindObjectOfType<TurnChanger>().TurnText.text = "Turn " + turn;
        }
    }

    public void DestroyCharacter(Character character)
    {
        Storage.characters.Remove(character);
        FindObjectOfType<GameManager>().currentTurnCharacters.Remove(character);
        FindObjectOfType<GameManager>().nextTurnCharacters.Remove(character);
    }

    public void TriggerAction(int characterID, int actionID, int hexID)
    {
        Character character = Storage.GetCharacterByID(characterID);
        Action action = FindObjectOfType<Storage>().GetActionByID(actionID);
        Hex hex = Storage.GetHexByID(hexID);

        if (character.GetActionByID(actionID).Use(character, hex))
        {
            GetComponent<ReplayManager>().AddStep(characterID, actionID, hexID);
            EndTurn();
        }
    }

    public void TriggerActionInReplay(int characterID, int actionID, int hexID)
    {
        Character character = Storage.GetCharacterByID(characterID);
        Action action = FindObjectOfType<Storage>().GetActionByID(actionID);
        Hex hex = Storage.GetHexByID(hexID);

        character.GetActionByID(actionID).OnSelect(character, character.transform.parent.GetComponent<Hex>());
        if (character.GetActionByID(actionID).Use(character, hex))
        {
            character.GetActionByID(actionID).OnDeselect();
            EndTurn();
        }
    }

    public void OnActionPress(Action action)
    {
        if(selectedAction != null)
            selectedAction.OnDeselect();
        selectedAction = action;
    }

    public void AddNewCharacter(Character character)
    {
        character.id = CharacterID;
        character.Statistics.Name = CharacterID.ToString();
        Storage.characters.Add(character);
        CharacterID++;
    }
    
    private void AddQueueTo(List<Character> characters)
    {
        int i = 0;
        foreach (Character character in characters)
        {
            character.Queue = i;
            i++;
        }
    }

    private void SortCharactersByInitiative(ref List<Character> characters)
    {
        characters = characters.OrderBy(character => character.Statistics.Initiative).ToList();
        characters.Reverse();
    }

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

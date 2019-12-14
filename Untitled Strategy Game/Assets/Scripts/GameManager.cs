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

    public int Queue;
    public int CharacterID;
    public Action selectedAction;
    public GameState GameState;

    int turn;

    void Start()
    {
        storage = FindObjectOfType<Storage>();

        CharacterID = 0;
        turn = 1;
        GameState = GameState.Editor;
        GameCanvas.enabled = false;
        EditorCanvas.enabled = true;

        FindObjectOfType<TurnChanger>().TurnText.text = "Turn 1";

        InitializeActions();
    }

    public void StartGame()
    {
        GameState = GameState.Game;
        EditorCanvas.enabled = false;
        GameCanvas.enabled = true;

        EventManager.EventTrigger();

        storage.characters.ForEach(character => 
        {
            character.Initiative += Random.Range(-10, 10);
            character.InitializeActions();
        });

        currentTurnCharacters.AddRange(storage.characters);

        SortCharactersByInitiative(ref currentTurnCharacters);
        AddQueueTo(currentTurnCharacters);

        FindObjectOfType<TopCharacterPanel>().OnStart(currentTurnCharacters);
        FindObjectOfType<ActionPanel>().SetActions(currentTurnCharacters[0]);
    }

    public void EndTurn()
    {
        selectedAction.OnDeselect();
        FindObjectOfType<ActionPanel>().SetActions(currentTurnCharacters[0]);

        currentTurnCharacters.Remove(currentTurnCharacters.First());
        
        nextTurnCharacters = storage.characters;
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
        FindObjectOfType<Storage>().characters.Remove(character);
        FindObjectOfType<GameManager>().currentTurnCharacters.Remove(character);
        FindObjectOfType<GameManager>().nextTurnCharacters.Remove(character);
    }

    public void TriggerAction(int characterID, int actionID, int hexID)
    {
        Character character = storage.GetCharacterByID(characterID);
        Action action = storage.GetActionByID(actionID);
        Hex hex = storage.GetHexByID(hexID);

        if(character.getActionByID(actionID).Use(character, hex))
            EndTurn();
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
        character.Name = CharacterID.ToString();
        storage.characters.Add(character);
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
        characters = characters.OrderBy(character => character.Initiative).ToList();
        characters.Reverse();
    }

    private static void InitializeActions()
    {
        foreach (Character character in FindObjectOfType<Storage>().characters)
        {
            foreach (Action action in character.actions)
            {
                action.Initialize(character);
            }
        }
    }
}

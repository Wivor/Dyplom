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
    public int selectedActionID;
    public int CharacterID;
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
    }

    public void StartGame()
    {
        GameState = GameState.Game;
        EditorCanvas.enabled = false;
        GameCanvas.enabled = true;

        EventManager.EventTrigger();

        storage.characters.ForEach(character => character.Initiative += Random.Range(-10, 10));

        currentTurnCharacters.AddRange(storage.characters);

        SortCharactersByInitiative(ref currentTurnCharacters);
        AddQueueTo(currentTurnCharacters);

        FindObjectOfType<TopCharacterPanel>().OnStart(currentTurnCharacters);
        FindObjectOfType<ActionPanel>().SetActions(currentTurnCharacters[0]);
    }

    public void EndTurn()
    {
        FindObjectOfType<ActionPanel>().SetActions(currentTurnCharacters[0]);

        currentTurnCharacters.Remove(currentTurnCharacters.First());
        
        nextTurnCharacters = storage.characters;
        SortCharactersByInitiative(ref nextTurnCharacters);
        FindObjectOfType<TopCharacterPanel>().SetTopBar(currentTurnCharacters, nextTurnCharacters);

        Queue++;
        if (Queue == storage.characters.Count)
        {
            currentTurnCharacters = nextTurnCharacters;
            AddQueueTo(currentTurnCharacters);
            FindObjectOfType<TopCharacterPanel>().OnStart(currentTurnCharacters);

            Queue = 0;
            turn++;
            FindObjectOfType<TurnChanger>().TurnText.text = "Turn " + turn;
        }
    }

    public void TriggerAction(int characterID, int actionID, int hexID)
    {
        Character character = storage.GetCharacterByID(characterID);
        Action action = storage.GetActionByID(actionID);
        HexGame hex = storage.GetHexByID(hexID);

        if(character.getActionByID(actionID).Use(character, hex))
            EndTurn();
    }

    public void OnActionPress(Action action)
    {
        selectedActionID = action.id;
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
}

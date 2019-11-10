using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public List<Character> Characters = new List<Character>();

    public List<Character> currentTurnCharacters = new List<Character>();
    List<Character> nextTurnCharacters = new List<Character>();

    public Canvas EditorCanvas;
    public Canvas GameCanvas;

    public int Queue;
    public int CharacterID;
    public GameState GameState;

    int turn;

    void Start()
    {
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

        Characters.ForEach(character => character.Initiative += Random.Range(-10, 10));

        currentTurnCharacters.AddRange(Characters);

        SortCharactersByInitiative(ref currentTurnCharacters);
        SortCharactersByInitiative(ref Characters);
        AddQueueTo(currentTurnCharacters);

        FindObjectOfType<TopCharacterPanel>().OnStart(currentTurnCharacters);
        FindObjectOfType<ActionPanel>().SetActions(currentTurnCharacters[0]);
    }

    public void EndTurn()
    {
        FindObjectOfType<ActionPanel>().SetActions(currentTurnCharacters[0]);

        currentTurnCharacters.Remove(currentTurnCharacters.First());
        
        nextTurnCharacters = Characters;
        SortCharactersByInitiative(ref nextTurnCharacters);
        SortCharactersByInitiative(ref Characters);
        FindObjectOfType<TopCharacterPanel>().SetTopBar(currentTurnCharacters, nextTurnCharacters);

        Queue++;
        if (Queue == Characters.Count)
        {
            currentTurnCharacters = nextTurnCharacters;
            AddQueueTo(currentTurnCharacters);
            FindObjectOfType<TopCharacterPanel>().OnStart(currentTurnCharacters);

            Queue = 0;
            turn++;
            FindObjectOfType<TurnChanger>().TurnText.text = "Turn " + turn;
        }
    }

    public void OnActionPress(Action action)
    {
        Debug.Log("Pressed");
        Debug.Log(action.actionName);
        Debug.Log(currentTurnCharacters[Queue].Name);
    }

    public void AddNewCharacter(Character character)
    {
        character.ID = CharacterID;
        character.Name = CharacterID.ToString();
        Characters.Add(character);
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

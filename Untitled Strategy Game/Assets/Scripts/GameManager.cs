using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    Storage storage;

    public Canvas EditorCanvas;
    public Canvas GameCanvas;
    public Canvas ReplayCanvas;

    public int Queue;
    public int CharacterID = 0;
    public Action selectedAction;

    int turn = 1;

    void Start()
    {
        storage = FindObjectOfType<Storage>();
        
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

        SortCharactersByInitiative(ref Storage.characters);
        AddQueueTo(Storage.characters);

        GetComponent<ReplayManager>().CreateReplayData(GetComponent<SaveMapManager>().GetSaveData());
        FindObjectOfType<TopCharacterPanel>().OnStart(Storage.characters);
        FindObjectOfType<ActionPanel>().SetActions(Storage.characters[0]);

        Storage.characters[Queue].GetComponent<Agent>().TakeAction();
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

        SortCharactersByInitiative(ref Storage.characters);
        AddQueueTo(Storage.characters);
        
        FindObjectOfType<TopCharacterPanel>().OnStart(Storage.characters);
        FindObjectOfType<ActionPanel>().enabled = false;
    }

    public void EndTurn()
    {
        Queue++;
        GetComponent<ReplayManager>().SaveReplay("test");

        if (selectedAction != null)
        {
            selectedAction.OnDeselect();
        }
        
        if (Queue == Storage.characters.Count)
        {
            Queue = 0;
            turn++;
            FindObjectOfType<TurnChanger>().TurnText.text = "Turn " + turn;
        }

        FindObjectOfType<ActionPanel>().SetActions(Storage.characters[Queue]);
        FindObjectOfType<TopCharacterPanel>().UpdateTopBar();
        //Storage.characters[Queue].GetComponent<Agent>().TakeAction();
    }

    public void DestroyCharacter(Character character)
    {
        Storage.characters.Remove(character);
        FindObjectOfType<TopCharacterPanel>().RemoveCharacter(character);
    }

    public void TriggerActionForPlayer(int characterID, int actionID, int hexID)
    {
        Character character = Storage.GetCharacterByID(characterID);
        Action action = FindObjectOfType<Storage>().GetActionByID(actionID);
        Hex hex = Storage.GetHexByID(hexID);

        if (character.GetActionByID(actionID).Use(character, hex))
        {
            GetComponent<ReplayManager>().AddStep(characterID, actionID, hexID);
            character.Statistics.CurrentActionPoints -= action.cost;
            character.GetActionByID(actionID).OnDeselect();

            if (character.Statistics.CurrentActionPoints == 0)
                EndTurn();
        }
    }

    public void TriggerAction(int characterID, int actionID, int hexID)
    {
        Character character = Storage.GetCharacterByID(characterID);
        Action action = FindObjectOfType<Storage>().GetActionByID(actionID);
        Hex hex = Storage.GetHexByID(hexID);

        character.GetActionByID(actionID).OnSelect(character, character.transform.parent.GetComponent<Hex>());
        if (character.GetActionByID(actionID).Use(character, hex))
        {
            character.Statistics.CurrentActionPoints -= action.cost;
            character.GetActionByID(actionID).OnDeselect();

            if (character.Statistics.CurrentActionPoints == 0)
                EndTurn();
            else
                Storage.characters[Queue].GetComponent<Agent>().TakeAction();
        }
        else
        {
            character.GetActionByID(actionID).OnDeselect();
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

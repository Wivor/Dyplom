using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    public int Queue;
    GameManager GameManager;

    public void Initialize()
    {
        GameManager = GetComponent<GameManager>();
    }

    public void StartGame()
    {
        EventManager.GameStartTrigger();

        Storage.characters.ForEach(character =>
        {
            character.Statistics.Initiative += Random.Range(-10, 10);
            character.InitializeActions();
        });

        SortCharactersByInitiative(ref Storage.characters);
        AddQueueNumberTo(Storage.characters);

        // infinite queue
        if (GameManager.AIenabled)
        {
            Storage.characters[Queue].GetComponent<Agent>().TakeAction();
        }
    }

    public void StartReplay()
    {
        Storage.characters.ForEach(character =>
        {
            character.InitializeActions();
        });

        SortCharactersByInitiative(ref Storage.characters);
        AddQueueNumberTo(Storage.characters);
    }

    /*
     * Called at the end of character turn.
     * Adds number to queue, saves move to replay file, deselects action taken, if character was last resets queue,
     * sets bottom bar for next character, moves characters in top bar.
     */

    public void EndTurn()
    {
        Queue++;
        GetComponent<ReplayManager>().SaveReplay("test");

        if (GameManager.selectedAction != null)
        {
            GameManager.selectedAction.OnDeselect();
        }

        if (Queue == Storage.characters.Count)
        {
            Queue = 0;
            GameManager.turn++;
            FindObjectOfType<TurnChanger>().TurnText.text = "Turn " + GameManager.turn;
        }

        FindObjectOfType<ActionPanel>().SetActions(Storage.characters[Queue]);
        FindObjectOfType<TopCharacterPanel>().UpdateTopBar();

        //infinite queue
        if (GameManager.AIenabled)
        {
            if (GameManager.turn <= 2)
            {
                Storage.characters[Queue].GetComponent<Agent>().TakeAction();
            }
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
        characters = characters.OrderBy(character => character.Statistics.Initiative).ToList();
        characters.Reverse();
    }

    /*
     * Adds move order number to characters.
     */

    private void AddQueueNumberTo(List<Character> characters)
    {
        int i = 0;
        foreach (Character character in characters)
        {
            character.Queue = i;
            i++;
        }
    }
}

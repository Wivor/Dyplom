using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TopCharacterPanel : MonoBehaviour
{
    public Transform ButtonPrefab;
    public Transform Separator;
    
    public List<Character> currentTurnCharacters = new List<Character>();

    public void OnStart(List<Character> startCharacters)
    {
        currentTurnCharacters = new List<Character>(Storage.characters);
        ClearBar();
        AddListOfCharacters(startCharacters);
        AddSeparator();
        AddListOfCharacters(startCharacters);
    }

    public void UpdateTopBar()
    {
        if (currentTurnCharacters.Count == 1)
        {
            currentTurnCharacters.Remove(currentTurnCharacters.First());
            currentTurnCharacters = new List<Character>(Storage.characters);
        }
        else
            currentTurnCharacters.Remove(currentTurnCharacters.First());

        UpdateBar();
    }

    private void UpdateBar()
    {
        ClearBar();
        AddListOfCharacters(currentTurnCharacters);
        AddSeparator();
        AddListOfCharacters(Storage.characters);
    }

    public void RemoveCharacter(Character character)
    {
        if (currentTurnCharacters.Contains(character))
        {
            currentTurnCharacters.Remove(character);
            UpdateBar();
        }
    }

    private void AddListOfCharacters(List<Character> startCharacters)
    {
        foreach (Character character in startCharacters)
        {
            Transform button = Instantiate(ButtonPrefab);
            button.SetParent(transform);
            button.GetComponent<TopBarButton>().Character = character;
        }
    }

    private void AddSeparator()
    {
        Transform button = Instantiate(Separator);
        button.SetParent(transform);
    }

    private void ClearBar()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }
}

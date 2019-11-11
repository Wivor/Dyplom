using System.Collections.Generic;
using UnityEngine;

public class TopCharacterPanel : MonoBehaviour
{
    public Transform ButtonPrefab;
    public Transform Separator;

    public void OnStart(List<Character> startCharacters)
    {
        ClearBar();
        AddListOfCharacters(startCharacters);
        AddSeparator();
        AddListOfCharacters(startCharacters);
    }

    public void SetTopBar(List<Character> currentTurnCharacters, List<Character> nextTurnCharacters)
    {
        ClearBar();
        AddListOfCharacters(currentTurnCharacters);
        AddSeparator();
        AddListOfCharacters(nextTurnCharacters);
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

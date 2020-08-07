using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TopBarButton : MonoBehaviour
{
    public Character character;

    void Start()
    {
        GetComponentInChildren<Text>().text = character.statistics.Name.ToString();
    }

    public void OnClick()
    {
        FindObjectOfType<GameStatistics>().SetCharacter(character);
    }
}

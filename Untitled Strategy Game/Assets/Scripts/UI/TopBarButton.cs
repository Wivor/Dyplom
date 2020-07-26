using UnityEngine;
using UnityEngine.UI;

public class TopBarButton : MonoBehaviour
{
    public Character Character;

    void Start()
    {
        GetComponentInChildren<Text>().text = Character.Statistics.Name.ToString();
    }

    public void OnClick()
    {
        FindObjectOfType<GameStatistics>().SetCharacter(Character);
    }
}

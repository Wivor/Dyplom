using UnityEngine;
using UnityEngine.UI;

public class TopBarButton : MonoBehaviour
{
    public Character Character;

    void Start()
    {
        GetComponentInChildren<Text>().text = Character.Name.ToString();
    }

    public void OnClick()
    {
        FindObjectOfType<GameStatistics>().SetCharacter(Character);
    }
}

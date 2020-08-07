using UnityEngine;

public class ClickableInReplay : MonoBehaviour
{
    public void OnMouseDown()
    {
        if (GetComponent<Character>() == null) return;
        Character character = GetComponent<Character>();
        FindObjectOfType<GameStatistics>().SetCharacter(character);
    }
}

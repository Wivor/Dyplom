using UnityEngine;

public class ClickableInReplay : MonoBehaviour
{
    public void OnMouseDown()
    {
        if (GetComponent<Character>() != null)
        {
            Character character = GetComponent<Character>();
            FindObjectOfType<GameStatistics>().SetCharacter(character);
        }
    }
}

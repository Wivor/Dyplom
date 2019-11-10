using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnGameStart;

    public static void EventTrigger()
    {
        OnGameStart();
    }
}

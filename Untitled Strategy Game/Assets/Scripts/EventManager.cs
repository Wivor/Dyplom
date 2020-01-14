using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnGameStart;
    public static event ClickAction OnReplayStart;

    public static void GameStartTrigger()
    {
        OnGameStart();
    }

    public static void ReplayStartTrigger()
    {
        OnReplayStart();
    }
}

public class Timer
{
    float deltaTime;

    float targetTime;
    float duration;

    public delegate void OnTimerEnd();
    public OnTimerEnd onEnd;

    public bool Repeat { get; set; }

    public Timer(float deltaTime, float duration, OnTimerEnd action)
    {
        this.deltaTime = deltaTime;
        this.duration = duration;
        targetTime = duration;
        onEnd = action;
        Repeat = true;
    }

    public void Update()
    {
        targetTime -= deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }
    }

    void timerEnded()
    {
        onEnd.Invoke();
        if (Repeat)
            targetTime = duration;
    }
}

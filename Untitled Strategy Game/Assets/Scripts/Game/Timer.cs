public class Timer
{
    private float _targetTime;
    private readonly float _deltaTime;
    private readonly float _duration;
    private readonly OnTimerEnd _onEnd;
    private readonly bool _repeat;

    public delegate void OnTimerEnd();

    public Timer(float deltaTime, float duration, OnTimerEnd action)
    {
        this._deltaTime = deltaTime;
        this._duration = duration;
        _targetTime = duration;
        _onEnd = action;
        _repeat = true;
    }

    public void Update()
    {
        _targetTime -= _deltaTime;

        if (_targetTime <= 0.0f)
        {
            TimerEnded();
        }
    }

    private void TimerEnded()
    {
        _onEnd.Invoke();
        if (_repeat)
            _targetTime = _duration;
    }
}

using UnityEngine;

public static class TimeControl
{
    public static bool IsTimeStopped => _isTimeStopped;
    private static bool _isTimeStopped;

    public static void SetTimeStop(bool value)
    {
        _isTimeStopped = value;
    }
}

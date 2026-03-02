using System;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private Action OnPlayerDetected;
    private Action OnPlayerLeft;

    public void PlayerInSight(Collider2D col)
    {
        OnPlayerDetected?.Invoke();
    }
    public void PlayerLeftRange(Collider2D col)
    {
        OnPlayerLeft?.Invoke();
    }
}

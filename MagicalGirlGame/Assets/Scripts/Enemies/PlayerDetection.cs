using System;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public Action OnPlayerDetected;
    public Action OnPlayerLeftAttackrange;
    public Action OnPlayerLeft;

    public void PlayerInSight(Collider2D col)
    {
        OnPlayerDetected?.Invoke();
    }
    public void PlayerLeftRange(Collider2D col)
    {
        OnPlayerLeft?.Invoke();
    }
    public void PlayerLeftAttackrange (Collider2D col)
    {
        OnPlayerLeftAttackrange?.Invoke();
    }
}

using System;
using UnityEngine;
using UnityEngine.Events;

public class PushableComponent2D : MonoBehaviour,IPushable
{
    public UnityEvent OnStartLongPush;
    public UnityEvent OnStopLongPush;
    [SerializeField] Rigidbody2D _rb2D;
    public void Push(PushInfo pushInfo)
    {
        _rb2D.AddForce((transform.position - pushInfo.pushPosition).normalized * pushInfo.pushForce,ForceMode2D.Impulse);
    }
    
    public void LongPush(PushInfo pushInfo, ForceMode2D pushMode)
    {
        _rb2D.AddForce((transform.position - pushInfo.pushPosition).normalized * pushInfo.pushForce, pushMode);
        
    }
    public void EndLongPush()
    {
        OnStopLongPush?.Invoke();
    }

    public void StartLongPush()
    {
        OnStartLongPush?.Invoke();
    }
}

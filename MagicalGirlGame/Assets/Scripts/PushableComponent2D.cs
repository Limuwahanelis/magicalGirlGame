using System;
using UnityEngine;
using UnityEngine.Events;

public class PushableComponent2D : MonoBehaviour,IPushable
{
    public UnityEvent OnStartLongPush;
    public UnityEvent OnStopLongPush;
    public UnityEvent OnStartLongPushMove;
    public UnityEvent OnStopLongPushMove;
    [SerializeField] Rigidbody2D _rb2D;
    public void Push(PushInfo pushInfo)
    {
         _rb2D.AddForce((transform.position - pushInfo.pushPosition).normalized * pushInfo.pushForce,ForceMode2D.Impulse);
    }
    
    public void LongPush(PushInfo pushInfo, ForceMode2D pushMode)
    {
        if(pushInfo.pushDir != Vector2.zero) _rb2D.AddForce(pushInfo.pushDir * pushInfo.pushForce, pushMode);
        else _rb2D.AddForce((transform.position - pushInfo.pushPosition).normalized * pushInfo.pushForce, pushMode);
        
    }
    public void EndLongPush()
    {
        OnStopLongPush?.Invoke();
    }

    public void StartLongPush()
    {
        OnStartLongPush?.Invoke();
    }

    public void LongPushMove(PushInfo pushInfo)
    {
        if (pushInfo.pushDir != Vector2.zero) _rb2D.MovePosition(_rb2D.position+ pushInfo.pushDir * pushInfo.pushForce*Time.fixedDeltaTime);
        else _rb2D.AddForce((transform.position - pushInfo.pushPosition).normalized * pushInfo.pushForce * Time.fixedDeltaTime);
    }

    public void StartLongPushMove()
    {
        OnStartLongPushMove?.Invoke();
    }

    public void EndLongPushMove()
    {
        OnStopLongPushMove?.Invoke(); ;
    }
}

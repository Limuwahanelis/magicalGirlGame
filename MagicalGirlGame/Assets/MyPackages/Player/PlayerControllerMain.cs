using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public abstract class PlayerControllerMain : MonoBehaviour
{
    [Header("Debug"), SerializeField] protected bool _printState;
    public virtual bool IsAlive { get; }
    public PlayerStateMain CurrentPlayerState => _currentPlayerState;
    public virtual GameObject MainBody { get; }
    protected PlayerStateMain _currentPlayerState;
    [SerializeField, HideInInspector] protected string _initialStateType;
    void Start()
    {

        Initalize();
    }
    protected abstract void Initalize();
    void Update()
    {
        _currentPlayerState.Update();
    }
    private void FixedUpdate()
    {
        _currentPlayerState.FixedUpdate();
    }
    public void ChangeState(PlayerState newState)
    {
        if (_printState) Logger.Log(newState.GetType());
        _currentPlayerState.InterruptState();
        _currentPlayerState = newState;
    }

    public void PushPlayer(PushInfo pushInfo)
    {
        _currentPlayerState.Push(pushInfo);
    }
    public Coroutine WaitAndExecuteFunction(float timeToWait, Action function)
    {
        return StartCoroutine(HelperClass.DelayedFunction(timeToWait, function));
    }
    public Coroutine WaitFrameAndExecuteFunction(Action function)
    {
        return StartCoroutine(HelperClass.WaitFrame(function));
    }

    private void OnDestroy()
    {

    }
}

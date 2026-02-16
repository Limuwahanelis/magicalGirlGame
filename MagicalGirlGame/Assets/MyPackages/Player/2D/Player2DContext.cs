using System;
using UnityEngine;

public class Player2DContext
{
    public Action<Player2DState> ChangePlayerState;
    public Func<float, Action, Coroutine> WaitAndPerformFunction;
    public Func<Action, Coroutine> WaitFrameAndPerformFunction;
    public MonoBehaviour coroutineHolder;
    public AnimationManager animationManager;
    public PlayerMovement2D playerMovement;
    public AudioEventPlayer audioEventPlayer;
    public PlayerChecks2D checks;

    public PushInfo pushInfo;

    //public PlayerCombat combat;
    //public PlayerCollisions collisions;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DIdleState : Player2DState
{
    public static Type StateType { get => typeof(Player2DIdleState); }
    public Player2DIdleState(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        PerformInputCommand();
        if (!_context.checks.IsOnGround) ChangeState(Player2DInAirState.StateType);
    }

    public override void SetUpState(Player2DContext context)
    {
        base.SetUpState(context);
    }
    public override void Move(Vector2 direction)
    {
        if (direction.x == 0) return;
        ChangeState(Player2DBasicMoveState.StateType);
    }
    public override void Jump()
    {
        ChangeState(Player2DJumpingState.StateType);
    }
    public override void InterruptState()
    {
     
    }
}
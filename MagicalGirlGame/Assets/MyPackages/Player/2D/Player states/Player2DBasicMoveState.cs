using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DBasicMoveState : Player2DState
{
    public static Type StateType { get => typeof(Player2DBasicMoveState); }
    public Player2DBasicMoveState(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        PerformInputCommand();
    }
    public override void Move(Vector2 direction)
    {

        if (direction.x == 0)
        {
            ChangeState(Player2DIdleState.StateType);
        }
        if(_context.checks.IsOnGround) _context.playerMovement.Move(direction, _context.checks.GroundHit.point);

    }
    public override void SetUpState(Player2DContext context)
    {
        base.SetUpState(context);
    }
    public override void Jump()
    {
        ChangeState(Player2DJumpingState.StateType);
    }
    public override void InterruptState()
    {
     
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DJumpingState : Player2DState
{
    public static Type StateType { get => typeof(Player2DJumpingState); }
    private Coroutine _jumpCor;
    public Player2DJumpingState(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        PerformInputCommand();
    }

    public override void SetUpState(Player2DContext context)
    {
        base.SetUpState(context);
        _context.animationManager.PlayAnimation("Jump");
        _context.playerMovement.StopPlayer();
        _context.playerMovement.SetRB(true);
        _jumpCor = _context.WaitAndPerformFunction(/*_context.animationManager.GetAnimationLength("Jump")*/0.2f, () =>
        {
            _context.playerMovement.Jump();
            ChangeState(Player2DInAirState.StateType);
            _context.playerMovement.SetRBMaterial(PlayerMovement2D.PhysicMaterialType.NO_FRICTION);
        });
    }

    public override void InterruptState()
    {
        if (_jumpCor != null) _context.coroutineHolder.StopCoroutine(_jumpCor);
    }

    public override void UndoComand()
    {

    }
}
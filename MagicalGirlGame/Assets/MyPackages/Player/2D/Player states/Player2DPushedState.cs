using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DPushedState : Player2DState
{
    public static Type StateType { get => typeof(Player2DPushedState); }
    private bool _isInAirAfterPush = false;
    public Player2DPushedState(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        if (!_context.checks.IsOnGround && !_isInAirAfterPush)
        {
            _isInAirAfterPush = true;
            _context.playerMovement.SetRBMaterial(PlayerMovement2D.PhysicMaterialType.NO_FRICTION);
        }

        if (_context.checks.IsOnGround && _isInAirAfterPush)
        {
            _context.playerMovement.SetRBMaterial(PlayerMovement2D.PhysicMaterialType.NONE);
            _context.playerMovement.StopPlayer();
            _isInAirAfterPush = false;
            _context.animationManager.SetAnimator(true);
            //if (_playerPusher != null) _playerPusher.ResumeCollisons(_playerCols);
            _context.WaitFrameAndPerformFunction(() => { ChangeState(Player2DIdleState.StateType); });

            return;
        }
    }

    public override void SetUpState(Player2DContext context)
    {
        base.SetUpState(context);
        _context.playerMovement.SetRB(true);
        _context.playerMovement.PushPlayer(_context.pushInfo);
    }

    public override void InterruptState()
    {
     
    }
}
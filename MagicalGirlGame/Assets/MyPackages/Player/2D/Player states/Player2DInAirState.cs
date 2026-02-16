using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player2DInAirState : Player2DState
{
    public static Type StateType { get => typeof(Player2DInAirState); }
    private bool _isFalling;
    private bool _jumpOnLanding;
    public Player2DInAirState(GetState function) : base(function)
    {
    }
    public override void SetUpState(Player2DContext context)
    {
        base.SetUpState(context);
        _context.playerMovement.SetRBMaterial(PlayerMovement2D.PhysicMaterialType.NO_FRICTION);
        _context.playerMovement.SetRB(true);
    }
    public override void Update()
    {
        PerformInputCommand();
        if (_context.playerMovement.IsPlayerFalling)
        {
            if (!_isFalling)
            {
                _context.animationManager.PlayAnimation("Fall");
                _isFalling = true;
            }
        }
        if (_context.checks.IsOnGround && math.abs(_context.playerMovement.PlayerRB.linearVelocity.y) < 0.0004)
        {
            _context.playerMovement.SetRBMaterial(PlayerMovement2D.PhysicMaterialType.NORMAL);
            if (_stateTypeToChangeFromInputCommand != null)
            {
                ChangeState(_stateTypeToChangeFromInputCommand);
                _stateTypeToChangeFromInputCommand = null;
            }
            else
            {
                _context.playerMovement.SetRB(false);
                _context.playerMovement.StopPlayer();
                ChangeState(Player2DIdleState.StateType);
            }
        }

    }
    public override void Attack(PlayerCombat.AttackModifiers modifier = PlayerCombat.AttackModifiers.NONE)
    {
        //_stateTypeToChangeFromInputCommand = PlayerAttackingState.StateType;
    }
    public override void Jump()
    {
        //_stateTypeToChangeFromInputCommand = PlayerJumpingState.StateType;
    }
    public override void Move(Vector2 direction)
    {
        _context.playerMovement.MoveInAir(direction);
    }
    public override void Push(PushInfo pushInfo)
    {
        base.Push( pushInfo);
    }
    public override void UndoComand()
    {
        _jumpOnLanding = false;
        _stateTypeToChangeFromInputCommand = null;
    }
    public override void InterruptState()
    {
        _isFalling = false;
        _jumpOnLanding = false;
    }
}
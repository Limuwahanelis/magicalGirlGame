using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player2DState: PlayerStateMain
{
    public delegate Player2DState GetState(Type state);
    protected Player2DContext _context;
    protected GetState _getType;
    protected Type _stateTypeToChangeFromInputCommand;

    public Player2DState(GetState function)
    {
        _getType = function;
    }
    public virtual void SetUpState(Player2DContext context)
    {
        _context = context;
    }
    public override void Push(PushInfo pushInfo)
    {
        _context.pushInfo = pushInfo;
        ChangeState(Player2DPushedState.StateType);
    }
    public void ChangeState(Type newStateType)
    {
        Player2DState state = _getType(newStateType);
        _context.ChangePlayerState(state);
        state.SetUpState(_context);
    }
}
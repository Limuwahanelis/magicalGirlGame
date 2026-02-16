using System;
using System.Collections;
using UnityEngine;

public abstract class PlayerState:PlayerStateMain
{
    public delegate PlayerState GetState(Type state);
    protected PlayerContext _context;
    protected GetState _getType;
    protected Type _stateTypeToChangeFromInputCommand;
    public PlayerState(GetState function)
    {
        _getType = function;
    }
    public virtual void SetUpState(PlayerContext context)
    {
        _context = context;
    }
    public void ChangeState(Type newStateType)
    {
        _stateTypeToChangeFromInputCommand = null;
        PlayerState state = _getType(newStateType);
        _context.ChangePlayerState(state);
        state.SetUpState(_context);
    }
}

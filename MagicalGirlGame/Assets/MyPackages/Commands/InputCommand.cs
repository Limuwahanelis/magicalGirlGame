using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputCommand
{
    protected PlayerStateMain _playerState;
    public InputCommand(PlayerStateMain playerState)
    {
        _playerState = playerState;
    }
    public abstract void Execute();

    public abstract void Undo();

}

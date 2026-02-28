using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DPlacingConstructState : Player2DState
{
    public static Type StateType { get => typeof(Player2DPlacingConstructState); }
    public Player2DPlacingConstructState(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        PerformInputCommand();
        _context.constructPlacement.ConstructToplace.transform.position = HelperClass.MousPosWorld2D;
    }

    public override void SetUpState(Player2DContext context)
    {
        base.SetUpState(context);
        TimeControl.SetTimeStop(true);
        _context.constructPlacement.SpawnConstruct();
    }
    public override void Attack(PlayerSpells.SpellTypes spellType, PlayerSpells.SpellForm spellForm)
    {
        if (_context.constructPlacement.ConstructToplace.CanBePlaced)
        {
            _context.constructPlacement.PlaceConstruct();
            TimeControl.SetTimeStop(false);
            ChangeState(Player2DIdleState.StateType);
        }
    }
    public override void InterruptState()
    {
        TimeControl.SetTimeStop(false);
    }
}
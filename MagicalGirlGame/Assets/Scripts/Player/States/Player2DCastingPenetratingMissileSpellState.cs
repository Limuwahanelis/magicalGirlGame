using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DCastingPenetratingMissileSpellState : Player2DState
{
    PenetratingMissileSpell _missile;
    public static Type StateType { get => typeof(Player2DCastingPenetratingMissileSpellState); }
    public Player2DCastingPenetratingMissileSpellState(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        PerformInputCommand();
        _missile.Attack();
    }

    public override void SetUpState(Player2DContext context)
    {
        base.SetUpState(context);
         _missile = _context.spells.AvailableSpells[PlayerSpells.SpellTypes.PEN] as PenetratingMissileSpell;
        _context.spells.SetSpellElement(_missile);
        _missile.StartAttack();
        ChangeState(Player2DIdleState.StateType);
    }

    public override void InterruptState()
    {
     
    }
}
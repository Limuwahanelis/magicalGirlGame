using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DCastingMissileSpellState : Player2DState
{
    MissileSpell _missileSpell;
    public static Type StateType { get => typeof(Player2DCastingMissileSpellState); }
    public Player2DCastingMissileSpellState(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        PerformInputCommand();
        _missileSpell.Attack();
    }

    public override void SetUpState(Player2DContext context)
    {
        base.SetUpState(context);
        _missileSpell = _context.spells.AvailableSpells[PlayerSpells.SpellTypes.PROJECTILES] as MissileSpell;
        _missileSpell.StartAttack();
        _context.spells.SetSpellElement(_missileSpell);
    }
    public override void EndAttack()
    {
        ChangeState(Player2DIdleState.StateType);
    }
    public override void InterruptState()
    {
     
    }
}
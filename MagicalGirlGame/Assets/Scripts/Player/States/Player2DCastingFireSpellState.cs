using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DCastingFireSpellState : Player2DState
{
    FireSpell _fireSpell;
    public static Type StateType { get => typeof(Player2DCastingFireSpellState); }
    public Player2DCastingFireSpellState(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        PerformInputCommand();
        _fireSpell.Attack();
    }

    public override void SetUpState(Player2DContext context)
    {
        base.SetUpState(context);
        _fireSpell= _context.spells.AvailableSpells[PlayerSpells.SpellTypes.FIRE] as FireSpell;
        _fireSpell.StartAttack();
    }

    public override void InterruptState()
    {
     
    }
}
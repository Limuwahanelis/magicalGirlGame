using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DCastingWindSpellState : Player2DState
{
    WindSpell _windSpell;
    public static Type StateType { get => typeof(Player2DCastingWindSpellState); }
    public Player2DCastingWindSpellState(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        PerformInputCommand();
    }

    public override void SetUpState(Player2DContext context)
    {
        base.SetUpState(context);
        _windSpell = _context.spells.AvailableSpells[PlayerSpells.SpellTypes.WIND] as WindSpell;
        _windSpell.StartAttack();
    }

    public override void InterruptState()
    {
     
    }
}
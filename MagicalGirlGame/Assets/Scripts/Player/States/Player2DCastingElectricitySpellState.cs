using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DCastingElectricitySpellState : Player2DState
{
    ElectricitySpell _electricitySpell;
    public static Type StateType { get => typeof(Player2DCastingElectricitySpellState); }
    public Player2DCastingElectricitySpellState(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        PerformInputCommand();
        _electricitySpell.Attack();
    }

    public override void SetUpState(Player2DContext context)
    {
        base.SetUpState(context);
        _electricitySpell = _context.spells.AvailableSpells[PlayerSpells.SpellTypes.ELECTRICYTY] as ElectricitySpell;
        _electricitySpell.StartAttack();
    }

    public override void InterruptState()
    {
     
    }
}
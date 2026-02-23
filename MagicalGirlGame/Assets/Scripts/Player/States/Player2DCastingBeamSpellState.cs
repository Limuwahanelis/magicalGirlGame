using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DCastingBeamSpellState : Player2DState
{
    private BeamSpell _beamSpell;
    public static Type StateType { get => typeof(Player2DCastingBeamSpellState); }
    public Player2DCastingBeamSpellState(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        PerformInputCommand();
        _beamSpell.Attack();
    }

    public override void SetUpState(Player2DContext context)
    {
        base.SetUpState(context);
        _beamSpell = _context.spells.AvailableSpells[PlayerSpells.SpellTypes.BEAM] as BeamSpell;
        _context.spells.SetSpellElement(_beamSpell);
        _beamSpell.StartAttack();

    }

    public override void InterruptState()
    {
     
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
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
    public override void Attack(PlayerSpells.SpellTypes spellType, PlayerSpells.SpellForm spellForm)
    {
        if (spellType == PlayerSpells.SpellTypes.EARTH) ChangeState(Player2DPlacingConstructState.StateType);
        else if (spellType == PlayerSpells.SpellTypes.WIND) ChangeState(Player2DCastingWindSpellState.StateType);
        else
        {
            switch (spellForm)
            {
                case PlayerSpells.SpellForm.NORMAL:
                    {
                        switch (spellType)
                        {
                            case PlayerSpells.SpellTypes.FIRE: ChangeState(Player2DCastingFireSpellState.StateType); break;
                            case PlayerSpells.SpellTypes.ELECTRICYTY: ChangeState(Player2DCastingElectricitySpellState.StateType); break;
                        }
                        break;
                    }
                case PlayerSpells.SpellForm.BEAM: ChangeState(Player2DCastingBeamSpellState.StateType); break;
                case PlayerSpells.SpellForm.PROJECTILES: ChangeState(Player2DCastingMissileSpellState.StateType); break;
                case PlayerSpells.SpellForm.PEN: ChangeState(Player2DCastingPenetratingMissileSpellState.StateType); break;
            }
        }
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
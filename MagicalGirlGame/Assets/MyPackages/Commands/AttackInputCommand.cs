using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInputCommand : InputCommand
{
    private PlayerSpells.SpellTypes _spellType;
    private PlayerSpells.SpellForm _spellForm;
    public AttackInputCommand(PlayerStateMain playerState, PlayerSpells.SpellTypes spellType, PlayerSpells.SpellForm spellForm) : base(playerState)
    {
        _spellType = spellType;
        _spellForm = spellForm;
    }

    public override void Execute()
    {
        _playerState.Attack(_spellType, _spellForm);
    }

    public override void Undo()
    {
        _playerState.UndoComand();
    }
}

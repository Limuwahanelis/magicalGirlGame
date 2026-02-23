using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] bool _debug;
    [SerializeField] PlayerController2D _player;
    [SerializeField] bool _useCommands;
    [SerializeField,ConditionalField("_useCommands")] PlayerInputStack _inputStack;
    [SerializeField] GameEventSO _pauseEvent;
    [SerializeField] PlayerSpells _playerSpell;
    private Vector2 _direction;
    int _spellFormCount;
    // Start is called before the first frame update
    void Start()
    {

        _spellFormCount = Enum.GetValues(typeof(PlayerSpells.SpellForm)).Length;
        _player = GetComponent<PlayerController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.IsAlive)
        {

            if (!PauseSettings.IsGamePaused)
            {
                _player.CurrentPlayerState.Move(_direction);

            }
        }
    }
    private void OnMove(InputValue value)
    {
        _direction = value.Get<Vector2>();
        if (_debug) Logger.Log(_direction);
    }
    void OnJump(InputValue value)
    {
        if (PauseSettings.IsGamePaused) return;
        if (_useCommands) _inputStack.CurrentCommand= new JumpInputCommand(_player.CurrentPlayerState);
        else _player.CurrentPlayerState.Jump();

    }
    void OnVertical(InputValue value)
    {
        _direction = value.Get<Vector2>();
    }
    private void OnPause()
    {
        _pauseEvent.Raise();
    }
    private void OnSpell(InputValue value)
    {
        Logger.Log(value.Get<float>());
        _playerSpell.SelectSpell((int)value.Get<float>());
    }
    private void OnSpellForm(InputValue value)
    {
        int spellFormindexToSelect = ((int)_playerSpell.SelectedSpellForm)+(int)value.Get<float>();
        if (spellFormindexToSelect > _spellFormCount) spellFormindexToSelect = 1;
        else if (spellFormindexToSelect < 1) spellFormindexToSelect = _spellFormCount;

        _playerSpell.SelectSpellForm(spellFormindexToSelect);
    }
    private void OnAttack(InputValue value)
    {
        if (PauseSettings.IsGamePaused) return;
        //if (_useCommands)
        //{
        //    _inputStack.CurrentCommand = new AttackInputCommand(_player.CurrentPlayerState);
        //    if (_direction.y > 0) _inputStack.CurrentCommand = new AttackInputCommand(_player.CurrentPlayerState, PlayerCombat.AttackModifiers.UP_ARROW);
        //    if (_direction.y < 0) _inputStack.CurrentCommand = new AttackInputCommand(_player.CurrentPlayerState, PlayerCombat.AttackModifiers.DOWN_ARROW);
        //}
        //else
        //{
            
            if(_direction.y==0) _player.CurrentPlayerState.Attack(_playerSpell.SelectedSpellType,_playerSpell.SelectedSpellForm);
        //    else if (_direction.y > 0) _player.CurrentPlayerState.Attack(PlayerCombat.AttackModifiers.UP_ARROW);
        //    else if (_direction.y < 0) _player.CurrentPlayerState.Attack(PlayerCombat.AttackModifiers.DOWN_ARROW);
        //}
    }
}

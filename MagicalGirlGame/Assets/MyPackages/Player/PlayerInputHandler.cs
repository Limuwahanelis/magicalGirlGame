using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] bool _debug;
    [SerializeField] PlayerController2D _player;
    [SerializeField] bool _useCommands;
    [SerializeField, ConditionalField("_useCommands")] PlayerInputStack _inputStack;
    [SerializeField] GameEventSO _pauseEvent;
    [SerializeField] PlayerSpells _playerSpell;
    [SerializeField] InputActionReference _attackAction;
    [SerializeField] ConstructPlacement _constructPlacement;
    private Vector2 _direction;
    int _spellFormCount;
    // Start is called before the first frame update
    void Start()
    {

        _spellFormCount = Enum.GetValues(typeof(PlayerSpells.SpellForm)).Length;
        _player = GetComponent<PlayerController2D>();
        _attackAction.action.performed += LMB;
        _attackAction.action.canceled += LMB;
        _attackAction.action.started += LMB;
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
        if (_useCommands) _inputStack.CurrentCommand = new JumpInputCommand(_player.CurrentPlayerState);
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
        if (_constructPlacement.ConstructToplace != null)
        {
            _constructPlacement.RotateConstruct();
        }
        else
        {
            int spellFormindexToSelect = ((int)_playerSpell.SelectedSpellForm) + (int)value.Get<float>();
            if (spellFormindexToSelect > _spellFormCount) spellFormindexToSelect = 1;
            else if (spellFormindexToSelect < 1) spellFormindexToSelect = _spellFormCount;

            _playerSpell.SelectSpellForm(spellFormindexToSelect);
        }
    }
    //private void OnAttack(InputValue value)
    //{
    //    if (PauseSettings.IsGamePaused) return;
    //    //if (_useCommands)
    //    //{
    //    //    _inputStack.CurrentCommand = new AttackInputCommand(_player.CurrentPlayerState);
    //    //    if (_direction.y > 0) _inputStack.CurrentCommand = new AttackInputCommand(_player.CurrentPlayerState, PlayerCombat.AttackModifiers.UP_ARROW);
    //    //    if (_direction.y < 0) _inputStack.CurrentCommand = new AttackInputCommand(_player.CurrentPlayerState, PlayerCombat.AttackModifiers.DOWN_ARROW);
    //    //}
    //    //else
    //    //{

    //    if (_direction.y == 0) _player.CurrentPlayerState.Attack(_playerSpell.SelectedSpellType, _playerSpell.SelectedSpellForm);
    //    //    else if (_direction.y > 0) _player.CurrentPlayerState.Attack(PlayerCombat.AttackModifiers.UP_ARROW);
    //    //    else if (_direction.y < 0) _player.CurrentPlayerState.Attack(PlayerCombat.AttackModifiers.DOWN_ARROW);
    //    //}
    //}
    public void LMB(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                if (context.interaction is HoldInteraction)
                {
                    _player.CurrentPlayerState.Attack(_playerSpell.SelectedSpellType, _playerSpell.SelectedSpellForm);
                    Logger.Log("Hold");
                }
                else
                {
                    Logger.Log("Press");
                }
                break;

            case InputActionPhase.Canceled:
                _player.CurrentPlayerState.EndAttack();
                Logger.Log("Cancel");
                break;
        }
    }
    private void OnDestroy()
    {
        _attackAction.action.performed -= LMB;
        _attackAction.action.canceled -= LMB;
        _attackAction.action.started -= LMB;
    }
}

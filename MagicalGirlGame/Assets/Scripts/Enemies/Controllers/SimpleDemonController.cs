using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class SimpleDemonController : DemonEnemyController
{
    [SerializeField] Transform patrolPoint1;
    [SerializeField] Transform patrolPoint2;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _Speed;
    [SerializeField] EnemyGroundCheck _nextStepCheck;
    [SerializeField] float _timeToRest;
    SimpleDemonContext _context;
    protected override void Initialize()
    {
        List<Type> states = AppDomain.CurrentDomain.GetAssemblies().SelectMany(domainAssembly => domainAssembly.GetTypes())
        .Where(type => typeof(EnemyState).IsAssignableFrom(type) && !type.IsAbstract && type.ToString().Contains("SimpleDemon")).ToArray().ToList();


        EnemyState.GetState getState = GetState;
        foreach (Type state in states)
        {
            _npcStates.Add(state, (EnemyState)Activator.CreateInstance(state, getState));
        }

        SimpleDemonStatePatrolling patrolState = GetState(SimpleDemonStatePatrolling.StateType) as SimpleDemonStatePatrolling;
        patrolState.SetPatrolPoints(patrolPoint1,patrolPoint2);
         _context = new SimpleDemonContext()
        {
            ChangeNPCState = ChangeState,
            playerTran = _playerTransform,
            enemyTran = _mainBody.transform,
            speed = _Speed,
            _nextStepCheck = _nextStepCheck,
            timeToRestAtPoint = _timeToRest,
            rb = _rb
        };
        EnemyState newState = GetState(Type.GetType(_initialStateType));
        newState.SetUpState(_context);
        _currentNPCState = newState;
    }
}

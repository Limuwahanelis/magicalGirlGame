using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDemonStateAttackingPlayer : EnemyState
{
    public static Type StateType { get => typeof(SimpleDemonStateAttackingPlayer); }
    private SimpleDemonContext _context;
    private float _timer;
    public SimpleDemonStateAttackingPlayer(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 1f)
        {
            Logger.Log("Attack");
            _timer = 0f;
        }

    }

    public override void FixedUpdate()
    {
        
    }

    public override void SetUpState(NPCContext context)
    {
        base.SetUpState(context);
        _context = (SimpleDemonContext)context;
        _context.playerDetection.OnPlayerLeftAttackrange += StartChase;
        _timer = 0f;
    }

    public override void InterruptState()
    {
        _context.playerDetection.OnPlayerLeftAttackrange -= StartChase;
    }
    private void StartChase()
    {
        Logger.Log("CHASE");
        ChangeState(SimpleDemonStateChasingPlayer.StateType);
    }
}
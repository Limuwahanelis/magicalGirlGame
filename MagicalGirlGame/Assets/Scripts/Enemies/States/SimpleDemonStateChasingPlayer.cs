using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDemonStateChasingPlayer : EnemyState
{
    public static Type StateType { get => typeof(SimpleDemonStateChasingPlayer); }
    private SimpleDemonContext _context;
    public SimpleDemonStateChasingPlayer(GetState function) : base(function)
    {
    }

    public override void Update()
    {

    }
    public override void FixedUpdate()
    {
        if (_context._nextStepCheck.IsOnGround)
        {
            Vector2 pos = _context.rb.position;
            pos.x += Time.deltaTime * _context.speed;
            _context.rb.MovePosition(pos);
        }
    }
    public override void SetUpState(NPCContext context)
    {
        base.SetUpState(context);
        _context = (SimpleDemonContext)context;
        _context.playerDetection.OnPlayerDetected += StopChasingAndAttack;
        _context.playerDetection.OnPlayerLeftAttackrange += ReturnToPatrol;
    }

    public override void InterruptState()
    {
        _context.playerDetection.OnPlayerDetected -= StopChasingAndAttack;
        _context.playerDetection.OnPlayerLeftAttackrange -= ReturnToPatrol;
    }
    private void ReturnToPatrol()
    {
        ChangeState(SimpleDemonStatePatrolling.StateType);
    }
    private void StopChasingAndAttack()
    {

        ChangeState(SimpleDemonStateAttackingPlayer.StateType);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDemonStateFalling : EnemyState
{
    public static Type StateType { get => typeof(SimpleDemonStateFalling); }
    private SimpleDemonContext _context;
    public SimpleDemonStateFalling(GetState function) : base(function)
    {
    }

    public override void Update()
    {
        if(_context.groundCheck.IsOnGround)
        {
            SimpleDemonStatePatrolling state = _getType(SimpleDemonStatePatrolling.StateType) as SimpleDemonStatePatrolling;
            state.SetPatrolPoints(_context.patrolPoints);
            ChangeState(SimpleDemonStatePatrolling.StateType);
        }
    }

    public override void SetUpState(NPCContext context)
    {
        base.SetUpState(context);
        _context = (SimpleDemonContext)context;
        _context.rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public override void InterruptState()
    {
        _context.rb.linearVelocity = Vector2.zero;
        _context.rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
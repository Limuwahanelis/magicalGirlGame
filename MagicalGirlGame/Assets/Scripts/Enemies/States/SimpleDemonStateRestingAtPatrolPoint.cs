using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDemonStateRestingAtPatrolPoint : EnemyState
{
    float _timer;
    public static Type StateType { get => typeof(SimpleDemonStateRestingAtPatrolPoint); }
    private SimpleDemonContext _context;
    public SimpleDemonStateRestingAtPatrolPoint(GetState function) : base(function)
    {
    }

    public override void Update()
    {

    }

    public override void FixedUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer >= _context.timeToRestAtPoint)
        {
            ChangeState(SimpleDemonStatePatrolling.StateType);
        }
    }
    public override void SetUpState(NPCContext context)
    {
        base.SetUpState(context);
        _timer = 0;
        _context = (SimpleDemonContext)context;
    }

    public override void InterruptState()
    {
     
    }
}
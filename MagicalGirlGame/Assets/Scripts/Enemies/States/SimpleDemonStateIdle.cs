using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDemonStateIdle : EnemyState
{
    public static Type StateType { get => typeof(SimpleDemonStateIdle); }
    private SimpleDemonContext _context;
    public SimpleDemonStateIdle(GetState function) : base(function)
    {
    }

    public override void Update()
    {

    }

    public override void SetUpState(NPCContext context)
    {
        base.SetUpState(context);
        _context = (SimpleDemonContext)context;
    }

    public override void InterruptState()
    {
     
    }
}
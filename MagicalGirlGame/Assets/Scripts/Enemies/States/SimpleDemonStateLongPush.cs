using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDemonStateLongPush : EnemyState
{
    public static Type StateType { get => typeof(SimpleDemonStateLongPush); }
    private SimpleDemonContext _context;
    public SimpleDemonStateLongPush(GetState function) : base(function)
    {
    }

    public override void Update()
    {

    }

    public override void SetUpState(NPCContext context)
    {
        base.SetUpState(context);
        _context = (SimpleDemonContext)context;
        //_context.rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public override void InterruptState()
    {
    }
    
}
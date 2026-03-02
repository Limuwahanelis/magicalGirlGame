using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SimpleDemonStatePatrolling : EnemyState
{
    List<Transform> _patrolPoints= new List<Transform>();
    int _index = 0;
    Vector2 _movePos;
    GlobalEnums.HorizontalDirections _moveDir;
    
    public static Type StateType { get => typeof(SimpleDemonStatePatrolling); }
    private SimpleDemonContext _context;
    public SimpleDemonStatePatrolling(GetState function) : base(function)
    {
    }

    public override void Update()
    {

        
    }
    public override void FixedUpdate()
    {
        if (Vector2.Distance(_context.enemyTran.position, _patrolPoints[_index].position) > 0.06)
        {
            //if(_context._nextStepCheck.IsOnGround)
            //{
            _movePos = _context.rb.position;
            if (_moveDir == GlobalEnums.HorizontalDirections.LEFT) _context.rb.MovePosition(_movePos + Vector2.left * _context.speed * Time.deltaTime);
            else _context.rb.MovePosition(_movePos + Vector2.right * _context.speed * Time.deltaTime);
            //}
            //else
            //{
            //    _index++;
            //    if(_index>_patrolPoints.Count) _index = 0;
            //    ChangeState(SimpleDemonStateRestingAtPatrolPoint.StateType);
            //}
        }
        else
        {
            _index++;
            if (_index >= _patrolPoints.Count) _index = 0;
            ChangeState(SimpleDemonStateRestingAtPatrolPoint.StateType);
        }
    }
    public override void SetUpState(NPCContext context)
    {
        base.SetUpState(context);
        _context = (SimpleDemonContext)context;
        if (_patrolPoints[_index].position.x < _context.enemyTran.position.x)
        {
            _moveDir = GlobalEnums.HorizontalDirections.LEFT;
        }
        else _moveDir = GlobalEnums.HorizontalDirections.RIGHT;
    }
    public void SetPatrolPoints(Transform point1,Transform point2)
    {
        _patrolPoints.Add(point1);
        _patrolPoints.Add(point2);
    }

    public override void InterruptState()
    {
     
    }
}
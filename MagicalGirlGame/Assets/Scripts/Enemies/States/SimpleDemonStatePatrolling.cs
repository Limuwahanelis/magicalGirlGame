using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SimpleDemonStatePatrolling : EnemyState
{
    List<Vector2> _patrolPoints= new List<Vector2>();
    int _index = 0;
    Vector2 _movePos;
    GlobalEnums.HorizontalDirections _moveDir;
    Vector2 rightScale =new Vector2(1,1);
    Vector2 leftScale = new Vector2(-1,1);
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
        if (Vector2.Distance(_context.enemyTran.position, _patrolPoints[_index]) > 0.06)
        {
            if (_context._nextStepCheck.IsOnGround)
            {
                _movePos = _context.rb.position;
                if (_moveDir == GlobalEnums.HorizontalDirections.LEFT) _context.rb.MovePosition(_movePos + Vector2.left * _context.speed * Time.deltaTime);
                else _context.rb.MovePosition(_movePos + Vector2.right * _context.speed * Time.deltaTime);
            }
            else
            {
                _index++;
                if (_index >= _patrolPoints.Count) _index = 0;
                ChangeState(SimpleDemonStateRestingAtPatrolPoint.StateType);
            }
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
        if (_patrolPoints[_index].x < _context.enemyTran.position.x)
        {
            _moveDir = GlobalEnums.HorizontalDirections.LEFT;
            _context.enemyTran.localScale = leftScale;
        }
        else
        {
            _moveDir = GlobalEnums.HorizontalDirections.RIGHT;
            _context.enemyTran.localScale = rightScale;
        }
    }
    public void SetPatrolPoints(List<Transform> points)
    {
        _patrolPoints.Clear();
        foreach (Transform t in points)
        {
            _patrolPoints.Add(t.position);
        }
    }

    public override void InterruptState()
    {
     
    }
}
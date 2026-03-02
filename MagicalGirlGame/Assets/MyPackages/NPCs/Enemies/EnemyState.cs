using System;
using UnityEngine;

public abstract class EnemyState:NPCState
{

    public EnemyState(GetState function):base(function)
    {
    }
    public override void SetUpState(NPCContext context)
    {
        base.SetUpState(context);
    }
    public virtual void Attack(PlayerCombat.AttackModifiers modifier = PlayerCombat.AttackModifiers.NONE) { }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushInfo
{
    public Vector3 pushPosition;
    public Collider2D[] involvedColliders;
    public DamageInfo.DamageType pushType;
    public float pushForce;
    public PushInfo(Vector3 pushpos, Collider2D[] involvedColliders=null, DamageInfo.DamageType pushType=DamageInfo.DamageType.NONE,float pushForce=-1) 
    {
        this.pushPosition = pushpos;
        this.involvedColliders = involvedColliders;
        this.pushType = pushType;
        this.pushForce = pushForce;
    }
}

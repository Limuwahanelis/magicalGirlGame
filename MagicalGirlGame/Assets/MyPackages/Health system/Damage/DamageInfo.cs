using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageInfo
{
    [Flags]
    public enum DamageType
    {
        NONE = 0,
        ENEMY = 1,
        MISSILE = 2,
        TRAPS = 4,
        PLAYER = 8,
        YING = 16,
        YANG = 32,
        FIRE = 64,
        WATER = 128,
        ELECTRICITY = 256,
        ALL = ~0,
    }

    public int dmg;
    public Vector3 dmgPosition;
    public Collider2D[] involvedColliders;
    public DamageType damageType;
    public DamageInfo(int dmg,Vector3 dmgPosition, DamageType damageType, Collider2D[] colliders=null) 
    {
        this.dmg = dmg;
        this.dmgPosition = dmgPosition;
        involvedColliders = colliders;
        this.damageType = damageType;
    }
}

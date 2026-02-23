using System.Collections.Generic;
using UnityEngine;

public abstract class ContinousSpell : Spell
{
    protected List<IDamagable> _damageablesInRange = new List<IDamagable>();

    protected DamageInfo.DamageType _damageType;
    public virtual void SetElement(DamageInfo.DamageType damageType) { }
    public virtual void SetSpellType(DamageInfo.DamageType type, ElementInfo info)
    { }
}

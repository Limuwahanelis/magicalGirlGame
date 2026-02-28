using System.Collections.Generic;
using UnityEngine;

public abstract class ContinousSpell : Spell
{
    protected List<IDamagable> _damageablesInRange = new List<IDamagable>();

   
    public virtual void SetElement(DamageInfo.DamageType damageType) { }

}

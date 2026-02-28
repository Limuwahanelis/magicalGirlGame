using UnityEngine;
using static DamageInfo;

public class PenetratingMissileSpell : Spell
{
    bool _canCastMissile = true;
    ItemSpawner _missileSpawner;
    ElementInfo _elementInfo;
    Transform _missileSpawnTran;
    PenetratingMissile _missile;
    public PenetratingMissileSpell(ItemSpawner missileSpawner, Transform missileSpawnTran)
    {
        _missileSpawner = missileSpawner;
        _missileSpawnTran = missileSpawnTran;
    }
    public override void Attack()
    {
        _missile.SetActivate(true);
    }
    public override void SetSpellType(DamageType type, ElementInfo info)
    {
        _damageType = type;
        _elementInfo = info;
    }
    public override void EndAttack()
    {
        
    }

    public override void StartAttack()
    {
        _missile = _missileSpawner.GetItem().GetComponent<PenetratingMissile>();
        _missile.transform.position = _missileSpawnTran.position;
        _missile.transform.up = HelperClass.MousPosWorld2D - _missile.transform.position;
        _missile.SetUp(_elementInfo, _damageType);
    }
}

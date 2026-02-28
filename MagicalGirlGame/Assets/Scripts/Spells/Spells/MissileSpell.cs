using System.Collections;
using UnityEngine;

public class MissileSpell : ContinousSpell
{
    bool _canCastMissile = true;
    ItemSpawner _missileSpawner;
    ElementInfo _elementInfo;
    float _missileCooldown;
    Transform _missileSpawnTran;
    MonoBehaviour _corutineHolder;

    public MissileSpell(ItemSpawner missileSpawner,float missileCooldown, Transform missileSpawnTran,MonoBehaviour corutineHolder)
    {
        _missileSpawner = missileSpawner;
        _missileCooldown = missileCooldown;
        _missileSpawnTran = missileSpawnTran;
        _corutineHolder = corutineHolder;
    }
    public override void SetSpellType(DamageInfo.DamageType type, ElementInfo info)
    {
        _damageType = type;
        _elementInfo = info;
    }
    public override void Attack()
    {
        _corutineHolder.StartCoroutine(DamageCor());
    }

    public override void EndAttack()
    {
        
    }

    public override void StartAttack()
    {
        _canCastMissile = true;
    }
    IEnumerator DamageCor()
    {
        if (!_canCastMissile) yield break;
        Missile missile = _missileSpawner.GetItem().GetComponent<Missile>();
        missile.transform.position = _missileSpawnTran.position;
        missile.transform.up = HelperClass.MousPosWorld2D - missile.transform.position;
        missile.SetUp(_elementInfo, _damageType);
        _canCastMissile = false;
        yield return new WaitForSeconds(_missileCooldown);
        _canCastMissile = true;
    }
}

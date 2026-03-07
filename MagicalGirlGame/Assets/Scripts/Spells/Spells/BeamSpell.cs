using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeamSpell : ContinousSpell
{
    private readonly Beam _beam;
    private readonly Transform _beamTransform;
    private readonly List<IDamagable> _damageables;
    private readonly BoxCollider2D _beamTrigger;
    private readonly Transform _beamCastTran;
    private readonly int _attackDamage;
    private readonly float _attackCooldown;
    private readonly AudioEvent _fireAttackAudioEvent;
    private readonly AudioSource _audioSource;
    private readonly float _yinYangDamgeMult;
    private readonly int _layerMaskToHit;
    private DamageInfo _damageInfo;
    private DamageInfo _yinYangdamageInfo;
    private ElementInfo _elementInfo;
    bool _canDealDamage = true;

    private float _beamDmgMult;
    RaycastHit2D[] _hit;
    public BeamSpell(Transform beamTransform, List<IDamagable> damageables, BoxCollider2D beamTrigger, Transform beamCastTran
                    ,int attackDamage, float attackCooldown, AudioEvent fireAttackAudioEvent, AudioSource audioSource, float yinYangDamgeMult,int layerMaskToHit )
    {
        _beamTransform = beamTransform;
        _damageables = damageables;
        _beamTrigger = beamTrigger;
        _beamCastTran = beamCastTran;
        _attackDamage = attackDamage;
        _attackCooldown = attackCooldown;
        _fireAttackAudioEvent = fireAttackAudioEvent;
        _audioSource = audioSource;
        _yinYangDamgeMult = yinYangDamgeMult;
        _layerMaskToHit = layerMaskToHit;
    }
    public override void StartAttack()
    {
        ShootBeam();
    }
    public override void Attack()
    {
        ShootBeam();
    }
    private RaycastHit2D FindClsestCol(RaycastHit2D[] hits)
    {
        float closestDistance = Vector2.Distance(_beamTransform.position, hits[0].point);
        RaycastHit2D closestHit = hits[0];
        for (int i = 1; i < _hit.Length; i++)
        {
            float distance = Vector2.Distance(_beamCastTran.position, _hit[i].point);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestHit = _hit[i];
            }
        }
        return closestHit;
    }
    private void ShootBeam()
    {
        Vector2 beamDir = HelperClass.MousPosWorld - _beamTransform.position;
        float distance = 20f;
        beamDir = beamDir.normalized;
        float angle = Vector2.SignedAngle(Vector2.right, beamDir);
        _hit = Physics2D.BoxCastAll(_beamTransform.position, _beamTrigger.size, Vector2.SignedAngle(Vector2.right, beamDir), beamDir, 20f, _layerMaskToHit);
        if (_hit.Length>0)
        {
            RaycastHit2D hit= FindClsestCol(_hit);
            _beamTransform.up = beamDir;
            float beamLength = ((Vector3)hit.rigidbody.position - _beamTransform.position).magnitude;
            _beamTransform.localScale = new Vector2(_beamTransform.localScale.x, beamLength);
        }
        else
        {
            _beamTransform.up = beamDir;
            _beamTransform.localScale = new Vector2(_beamTransform.localScale.x, distance);
        }
    }
    public override void SetSpellType(DamageInfo.DamageType type, ElementInfo info)
    {
        Logger.Log(type);
        _damageType = type;
        _elementInfo = info;
    }

    public override void EndAttack()
    {
        _beamTransform.localScale = new Vector2(_beamTransform.localScale.x, 0);
    }
    IEnumerator DamageCor()
    {
        if (!_canDealDamage) yield break;
        _damageInfo.damageType = _damageType;
        _damageInfo.dmgPosition = _beamCastTran.position;
        _damageInfo.dmg = _elementInfo.Damage;
        _yinYangdamageInfo.damageType = DamageInfo.DamageType.YING;
        _yinYangdamageInfo.dmgPosition = _beamCastTran.position;
        //_yinYangdamageInfo.dmg = _yinYangDamage;

        for (int i = 0; i < _damageablesInRange.Count; i++)
        {
            _damageablesInRange[i].TakeDamage(_damageInfo);
            _damageablesInRange[i].TakeDamage(_yinYangdamageInfo);
        }
        _canDealDamage = false;
        yield return new WaitForSeconds(_attackCooldown);
        _canDealDamage = true;
    }


}

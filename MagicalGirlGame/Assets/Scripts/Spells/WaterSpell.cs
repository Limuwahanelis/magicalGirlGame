using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpell : ContinousSpell
{
    PlayerSpells _playerSpells;
    ParticleSystem _particleSystem;
    PolygonCollider2D _fireTrigger;
    Transform _mainBody;
    float _fireRange;
    float _fireAngle;
    private int _attackDamage;
    private float _attackCooldown;
    private readonly AudioEvent _waterAudioEvent;
    private readonly AudioSource _auioSource;
    private DamageInfo _damageInfo;
    private DamageInfo _yinYangdamageInfo;
    bool _canDealDamage = true;
    ParticleSystem.EmitParams _params;
    private int _yinYangDamage;
    public WaterSpell(PlayerSpells playerSpells, ParticleSystem system, List<IDamagable> damageables, PolygonCollider2D fireTrigger, Transform mainBody, 
        float fireRange, float fireAngle, int attackDamage, float attackCooldown, AudioEvent waterAudioEvent, AudioSource auioSource, int yinYangDamage)
    {
        _playerSpells = playerSpells;
        _particleSystem = system;
        _damageablesInRange = damageables;
        _fireTrigger = fireTrigger;
        _mainBody = mainBody;
        _fireRange = fireRange;
        _fireAngle = fireAngle;
        _attackDamage = attackDamage;
        _attackCooldown = attackCooldown;
        _waterAudioEvent = waterAudioEvent;
        _auioSource = auioSource;
        _params = new ParticleSystem.EmitParams();
        _yinYangDamage = yinYangDamage;
    }
    public override void Attack()
    {
        Vector2 direction = HelperClass.MousPosWorld - _mainBody.position;
        _particleSystem.transform.up = direction.normalized;
        ParticleSystem.MainModule mainModule = _particleSystem.main;
        ParticleSystem.MinMaxCurve curve = mainModule.startRotation;
        curve.mode = ParticleSystemCurveMode.TwoConstants;
        float radians = -_particleSystem.transform.rotation.eulerAngles.z * Mathf.PI / 180f;
        float spreadRad = 2f * Mathf.PI / 180f;
        curve.constant = radians;
        curve.constantMin = radians - spreadRad;
        curve.constantMax = radians + spreadRad;
        mainModule.startRotation = curve;
        List<Vector2> _trianglePoints = GetTriangle();
        List<Vector2> tmp = new List<Vector2>() { _mainBody.transform.InverseTransformPoint(_trianglePoints[0]), _mainBody.transform.InverseTransformPoint(_trianglePoints[1]), _mainBody.transform.InverseTransformPoint(_trianglePoints[2]) };
        _fireTrigger.points = tmp.ToArray();
        _playerSpells.StartCoroutine(DamageCor());
    }

    public override void EndAttack()
    {
        _waterAudioEvent.Pause(_auioSource);
        _particleSystem.Stop(false, ParticleSystemStopBehavior.StopEmitting);
        _fireTrigger.enabled = false;
    }

    public override void StartAttack()
    {
        _waterAudioEvent.Play(_auioSource);
        ParticleSystem.MainModule mainModule = _particleSystem.main;
        mainModule.startLifetime = 0.95f;
        _particleSystem.transform.position = _mainBody.transform.position;
        ParticleSystem.EmissionModule emissionModule = _particleSystem.emission;
        emissionModule.rateOverTime = 80;
        _particleSystem.Play();
        _fireTrigger.enabled = true;
    }

    List<Vector2> GetTriangle()
    {
        List<Vector2> toReturn = new List<Vector2>() { new Vector2(), new Vector2(), new Vector2() };
        Vector2 pointA = _mainBody.transform.position;
        Vector2 mouseDir = (HelperClass.MousPosWorld - _mainBody.transform.position).normalized;
        Vector2 fireForwardPoint = pointA + mouseDir * (_fireRange + _fireRange * 0.15f);
        Vector2 fireForwardDir = (fireForwardPoint - (Vector2)_mainBody.transform.position);

        float mult = fireForwardPoint.magnitude;

        Quaternion rot = Quaternion.AngleAxis(_fireAngle / 2, Vector3.forward);
        Vector2 ABdir = rot * fireForwardDir;

        float aFunParam = ABdir.y / ABdir.x;

        rot = Quaternion.AngleAxis(-_fireAngle / 2, Vector3.forward);
        Vector2 ACdir = rot * fireForwardDir;

        toReturn[0] = pointA + ABdir;
        toReturn[1] = pointA;
        toReturn[2] = pointA + ACdir;


        return toReturn;
    }

    IEnumerator DamageCor()
    {
        if (!_canDealDamage) yield break;
        _damageInfo.damageType = DamageInfo.DamageType.WATER;
        _damageInfo.dmgPosition = _mainBody.transform.position;
        _damageInfo.dmg = 0;
        _yinYangdamageInfo.damageType = DamageInfo.DamageType.YING;
        _yinYangdamageInfo.dmgPosition = _mainBody.transform.position;
        _yinYangdamageInfo.dmg = _yinYangDamage;
        for (int i = 0; i < _damageablesInRange.Count; i++)
        {
            _damageablesInRange[i].TakeDamage(_damageInfo);
        }
        _canDealDamage = false;
        yield return new WaitForSeconds(_attackCooldown);
        _canDealDamage = true;
    }
}

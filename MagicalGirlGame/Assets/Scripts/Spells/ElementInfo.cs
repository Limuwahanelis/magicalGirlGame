using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ElementInfo")]
public class ElementInfo : ScriptableObject
{
    [SerializeField] private int _yinYangDamage;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private int _damage;
    [SerializeField] private DamageInfo.DamageType _yinYangDamageType;
    public int YinYangDamage { get => _yinYangDamage;  }
    public float AttackCooldown { get => _attackCooldown; }
    public int Damage { get => _damage; }
    public DamageInfo.DamageType YinYangDamageType { get => _yinYangDamageType;  }
}
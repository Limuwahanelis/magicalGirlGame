using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class YingYangeHealth : HealthSystem
{
    public UnityEvent OnBalanceAchieved;
    [SerializeField] int _staringYingyang;
    [SerializeField] int _minYingYang;
    [SerializeField] int _maxYingYang;
    [SerializeField] float _timeToHoldBalance;
    [SerializeField] float _yingYangAutomaticChangeRate;
    [SerializeField] int _yingYangAutomaticChangeValue;
    [SerializeField] Image _balanceFill;
    private float _yingYangCenter;
    private float _currentBalanceTime;
    private float _changeTimer = 0;
    private bool _wasHit = true;
    override protected void Start()
    {
        base.Start();
        _yingYangCenter = (_minYingYang + _maxYingYang) / 2;
        (_hpBar as YingYangBar).SetYinyangBorders(_minYingYang, _maxYingYang);
    }
    public override void TakeDamage(DamageInfo info)
    {
        if (!IsAlive) return;

        if (info.damageType == DamageInfo.DamageType.YANG)
        {
            _currentHP += info.dmg;
            _currentHP = math.clamp(_currentHP, 0, _maxHP);
        }
        else if (info.damageType == DamageInfo.DamageType.YING)
        {
            _currentHP -= info.dmg;
            _currentHP = math.clamp(_currentHP, 0, _maxHP);
        }
        else return;
        _wasHit = true;
        if (_hpBar != null) _hpBar.SetHealth(_currentHP);
        OnHitEvent?.Invoke(info);
    }

    private void Update()
    {
        if (!_wasHit || !IsAlive) return;
        if(_currentHP >= _minYingYang &&  _currentHP <= _maxYingYang)
        {
            _currentBalanceTime += Time.deltaTime;
            _balanceFill.fillAmount = _currentBalanceTime / _timeToHoldBalance;
            if (_currentBalanceTime >=_timeToHoldBalance)
            {
                OnBalanceAchieved?.Invoke();
            }
        }
        if(_changeTimer > _yingYangAutomaticChangeRate)
        {
            _changeTimer = 0;
            if(_currentHP >= _yingYangCenter) _currentHP += _yingYangAutomaticChangeValue;
            else _currentHP -= _yingYangAutomaticChangeValue;
            _currentHP = math.clamp(_currentHP,0, _maxHP);
            _hpBar.SetHealth(_currentHP);
        }
        else _changeTimer += Time.deltaTime;
        
    }
    public void Hit() 
    {
        _wasHit = true;
    }
    public void ApplyYang()
    {
        _currentHP += 1;
        _hpBar.SetHealth(_currentHP);
    }
    public void ApplyYing()
    {
        _currentHP -= 1;
        _hpBar.SetHealth(_currentHP);
    }

    [CustomEditor(typeof(YingYangeHealth))]
    public class HHEditor:Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(GUILayout.Button("HIt"))
            {
               ( target as YingYangeHealth).Hit();
            }
            if (GUILayout.Button("Heal"))
            {
                (target as YingYangeHealth).ApplyYang();
            }
            if (GUILayout.Button("dama"))
            {
                (target as YingYangeHealth).ApplyYing();
            }
        }
    }

}

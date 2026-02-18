using MyBox;
using System.Collections;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class YingYangBar : HealthBar
{
    private MaterialPropertyBlock _propertyBlock;
    [SerializeField] Renderer _renderer;
    [SerializeField] int _maxHP = 100;
    [SerializeField] bool _startWithNotMaxHP;
    [SerializeField, ConditionalField("_startWithNotMaxHP")] int _currentHP;
    [SerializeField] bool _lazyHealthBar;
    [SerializeField] float _timeToMoveHealth;
    [SerializeField] int _lazyHPLostPerSecond;
    [SerializeField] float _lengthPerHP = 1f;
    [SerializeField] GameObject _rightYinYangBorder;
    [SerializeField] GameObject _leftYinYangBorder;

    private float _lazyHP;
    private bool _initialized = false;
    bool _isReducingHP;
    private void Awake()
    {
        Initialize();
    }
    public override void Initialize()
    {
        if (_initialized) return;
        if (_propertyBlock == null) _propertyBlock = new MaterialPropertyBlock();
        _renderer.SetPropertyBlock(_propertyBlock);
        _lazyHP = _currentHP = _maxHP;
        _initialized = true;
        if (_startWithNotMaxHP) SetHealth(_currentHP);
    }
    public void SetYinyangBorders(int leftBorder,int rightBorder)
    {
        float leftBorderPosx = (leftBorder/(float)_maxHP) * _renderer.gameObject.transform.localScale.x - _renderer.gameObject.transform.localScale.x / 2;
        float rightBorderPosx = (rightBorder / (float)_maxHP) * _renderer.gameObject.transform.localScale.x - _renderer.gameObject.transform.localScale.x / 2;
        _leftYinYangBorder.transform.localPosition = new Vector3(leftBorderPosx, 0, 0) ;
        _rightYinYangBorder.transform.localPosition = new Vector3(rightBorderPosx, 0, 0);
    }
    public override void AdjustForLength()
    {
        transform.localScale = new Vector3(_maxHP * _lengthPerHP, transform.lossyScale.y, transform.lossyScale.z);
    }
    public override void SetHealth(int hp)
    {
        _lazyHP = _currentHP = hp;
        _propertyBlock.SetFloat("_Value", hp / (float)_maxHP);
        _propertyBlock.SetFloat("_LazyFillValue", _lazyHP / _maxHP);
        _renderer.SetPropertyBlock(_propertyBlock);

    }
    public override void SetMaxHealth(int value)
    {
        _maxHP = value;
        SetHealth(_maxHP);
    }
    public override void ReduceHP(int value)
    {
        StartCoroutine(HealthReduceHealthCor());
        _currentHP -= value;

        _propertyBlock.SetFloat("_Value", _currentHP / (float)_maxHP);
        _renderer.SetPropertyBlock(_propertyBlock);
    }
    IEnumerator HealthReduceHealthCor()
    {
        if (_isReducingHP) yield break;
        _isReducingHP = true;
        float time = 0;
        while (time <= _timeToMoveHealth)
        {
            time += Time.deltaTime;
            yield return null;
        }
        while (_lazyHP > _currentHP)
        {
            _lazyHP -= Time.deltaTime * _lazyHPLostPerSecond;
            _lazyHP = math.clamp(_lazyHP, _currentHP, _maxHP);
            _propertyBlock.SetFloat("_LazyFillValue", _lazyHP / _maxHP);
            _renderer.SetPropertyBlock(_propertyBlock);
            yield return null;
        }
        _isReducingHP = false;
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(YingYangBar))]
    public class HPEditor : Editor
    {
        SerializedProperty _currentValue;
        SerializedProperty _maxValue;
        SerializedProperty _bar;
        SerializedProperty _fill;
        StandardHealthBar _instance;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("hp"))
            {
                (target as StandardHealthBar).ReduceHP(10);
            }
            if (GUILayout.Button("AdjustForLength"))
            {
                (target as StandardHealthBar).AdjustForLength();
            }
        }
    }
#endif
}

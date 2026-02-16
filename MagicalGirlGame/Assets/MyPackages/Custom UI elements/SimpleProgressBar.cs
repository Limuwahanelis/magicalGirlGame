using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class SimpleProgressBar : MonoBehaviour
{
    public float CurrentValue => _currentValue;
    public float MaxValue => _maxValue;
    [SerializeField] Color _fillColor;
    [SerializeField] Color _bgColor;
    [SerializeField] RectTransform _fill;
    [SerializeField] RectTransform _bg;
    [SerializeField] RectTransform _bar;
    [SerializeField] float _minValue;
    [SerializeField] float _maxValue;
    [SerializeField] float _currentValue;
    Vector2 tmp;
    Vector2 tmp2;
    public void Initialize()
    {

    }
    public void ChangeValue(float valueChange)
    {
        _currentValue += valueChange;
        UpdateDisplay();
    }
    public void SetValue(float newValue)
    {
        _currentValue = math.clamp(newValue, 0, _maxValue);
        UpdateDisplay();
    }

    public void SetMaxValue(float value)
    {
        _maxValue = value;
        UpdateDisplay();
    }
    private void UpdateDisplay()
    {

        Vector2 anchorMax = _fill.anchorMax;
        Vector2 anchorMin = _fill.anchorMin;
        float anchorX = (_currentValue - _minValue) / (_maxValue - _minValue);
        float anchorXMin = 0 / _maxValue;
        anchorMax.x = anchorX;
        _fill.anchorMax = anchorMax;
        if (anchorXMin <= anchorX)
        {
            anchorMin.x = anchorXMin;
            _fill.anchorMin = anchorMin;
        }
        _fill.sizeDelta = Vector2.zero;
    }
    public void ApplyColor(Color fillColor, Color bgColor)
    {
#if UNITY_EDITOR
        _fillColor = fillColor;
        _bgColor = bgColor;
        UnityEditor.EditorUtility.SetDirty(this);
        _fill.GetComponent<Image>().color = fillColor;
        _bg.GetComponent<Image>().color = bgColor;
        UnityEditor.EditorUtility.SetDirty(_fill.GetComponent<Image>());
        UnityEditor.EditorUtility.SetDirty(_bg.GetComponent<Image>());
#endif
    }
    private void OnValidate()
    {
        _currentValue = math.clamp(_currentValue, _minValue, _maxValue);
    }
}

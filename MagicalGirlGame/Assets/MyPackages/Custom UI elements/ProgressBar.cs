using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public int CurrentValue => _currentValue;
    public int MaxValue => _maxValue;
    [SerializeField] Color _fillColor;
    [SerializeField] Color _bgColor;
    [SerializeField] RectTransform _fill;
    [SerializeField] RectTransform _bg;
    [SerializeField] RectTransform _bar;
    [SerializeField] int _maxValue;
    [SerializeField] int _currentValue;
    [SerializeField] float _lengthPerHp = 1f;
    Vector2 tmp;
    Vector2 tmp2;
    public void Initialize()
    {

    }
    public void ChangeValue(int valueChange)
    {
        _currentValue += valueChange;
        UpdateDisplay();
    }
    public void AdjustForLength()
    {
        Vector2 tmp = gameObject.GetComponent<RectTransform>().sizeDelta;
        tmp.x = _maxValue * _lengthPerHp;
        gameObject.GetComponent<RectTransform>().sizeDelta = tmp;
    }
    public void SetValue(int newValue)
    {
        _currentValue = math.clamp(newValue, 0, _maxValue);
        UpdateDisplay();
    }

    public void SetMaxValue(int value)
    {
        _maxValue = value;
        UpdateDisplay();
    }
    private void UpdateDisplay()
    {

        float pct = _currentValue / (float)_maxValue;
        float value = math.remap(0, 1, -_bar.sizeDelta.x / 2, 0, pct);
        tmp = _fill.anchoredPosition;
        tmp2 = _fill.sizeDelta;
        tmp.x = value;
        tmp2.x = value * 2;
        _fill.anchoredPosition = tmp;
        _fill.sizeDelta = tmp2;
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
        _currentValue = math.clamp(_currentValue, 0, _maxValue);
    }
}

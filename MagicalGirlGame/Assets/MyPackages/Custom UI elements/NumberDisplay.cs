using TMPro;
using UnityEngine;

public class NumberDisplay : MonoBehaviour
{
    [SerializeField] bool _showMaxValue;
    [SerializeField] TMP_Text _currentValueText;
    [SerializeField] TMP_Text _maxValueText;


    public void SetMaxValue(float maxValue)
    {
        _maxValueText.text = (_showMaxValue ? "/" : "")+ maxValue.ToString();
    }
    public void SetMaxValue(int maxValue)
    {
        _maxValueText.text = (_showMaxValue ? "/" : "") + maxValue.ToString();
    }
    public void SetValue(float value)
    {
        _currentValueText.text = value.ToString();
    }
    public void SetValue(int value)
    {
        _currentValueText.text = value.ToString();
    }
    public void SetMaxValueColor(Color color)
    {
        _maxValueText.color = color;
    }
    public void SetValueColor(Color color)
    {
        _currentValueText.color = color;
    }
}

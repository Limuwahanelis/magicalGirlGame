using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(SimpleProgressBar))]
public class SimpleProgressBarEditor: Editor
{
    SerializedProperty _currentValue;
    SerializedProperty _maxValue;
    SerializedProperty _minValue;
    SerializedProperty _bar;
    SerializedProperty _bg;
    SerializedProperty _fill;
    SerializedProperty _normalColor;
    SerializedProperty _BGColor;
    private Vector2 tmp;
    private Vector2 tmp2;
    private RectTransform _barRectTransform;
    private RectTransform _bgRectTransform;
    private RectTransform _fillRectTransform;
    private void OnEnable()
    {
        _currentValue = serializedObject.FindProperty("_currentValue");
        _maxValue = serializedObject.FindProperty("_maxValue");
        _minValue = serializedObject.FindProperty("_minValue");
        _bar = serializedObject.FindProperty("_bar");
        _bg = serializedObject.FindProperty("_bg");
        _fill = serializedObject.FindProperty("_fill");
        _normalColor = serializedObject.FindProperty("_fillColor");
        _BGColor = serializedObject.FindProperty("_bgColor");
        _barRectTransform = ((RectTransform)(_bar.objectReferenceValue));
        _fillRectTransform = ((RectTransform)(_fill.objectReferenceValue));
        _bgRectTransform = ((RectTransform)(_bg.objectReferenceValue));
    }
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject, "_minValue", "_maxValue", "_currentValue");

        EditorGUI.BeginChangeCheck();
        float newMin = EditorGUILayout.FloatField("Min Value", _minValue.floatValue);
        if (EditorGUI.EndChangeCheck())
        {
            if ( newMin < _maxValue.floatValue)
            {
                _minValue.floatValue = newMin;
                if (_currentValue.floatValue < newMin)
                    _currentValue.floatValue = newMin;
            }
        }

        EditorGUI.BeginChangeCheck();
        float newMax = EditorGUILayout.FloatField("Max Value", _maxValue.floatValue);
        if (EditorGUI.EndChangeCheck())
        {
            if (newMax > _minValue.floatValue)
            {
                _maxValue.floatValue = newMax;
                if (_currentValue.floatValue > newMax)
                    _currentValue.floatValue = newMax;
            }
        }

        EditorGUILayout.Slider(_currentValue, _minValue.floatValue, _maxValue.floatValue);
        if (_fill.objectReferenceValue != null && _fillRectTransform != null)
        {
            Image img = _fillRectTransform.GetComponent<Image>();
            img.color = _normalColor.colorValue;
            EditorUtility.SetDirty(img);
        }
        if (_bg.objectReferenceValue != null && _bgRectTransform != null)
        {
            Image img = _bgRectTransform.GetComponent<Image>();
            img.color = _BGColor.colorValue;
            EditorUtility.SetDirty(img);
        }
        if (GUI.changed)
        {
            UpdateFill();
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void UpdateFill()
    {
        if (_fillRectTransform == null) return;
        Vector2 anchorMax = _fillRectTransform.anchorMax;
        Vector2 anchorMin = _fillRectTransform.anchorMin;
        float anchorX = (_currentValue.floatValue-_minValue.floatValue) /(_maxValue.floatValue - _minValue.floatValue);
        float anchorXMin = 0 / _maxValue.floatValue;
        anchorMax.x = anchorX;
        _fillRectTransform.anchorMax = anchorMax;
        if (anchorXMin <= anchorX)
        {
            anchorMin.x = anchorXMin;
            _fillRectTransform.anchorMin = anchorMin;
        }
        _fillRectTransform.sizeDelta = Vector2.zero;

    }
}
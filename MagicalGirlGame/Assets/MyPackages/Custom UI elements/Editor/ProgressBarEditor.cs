using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Unity.Mathematics;

[CustomEditor(typeof(ProgressBar))]
public class ProgressBarEditor: Editor
{
    SerializedProperty _currentValue;
    SerializedProperty _maxValue;
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
        base.OnInspectorGUI();
        serializedObject.Update();

        if (GUILayout.Button("AdjustForLength"))
        {
            (target as ProgressBar).AdjustForLength();
            UpdateFill();
        }
        if (_fill.objectReferenceValue != null && _fillRectTransform!=null)
        {
            Image img = _fillRectTransform.GetComponent<Image>();
            img.color = _normalColor.colorValue;
            img.pixelsPerUnitMultiplier = _maxValue.intValue;
            EditorUtility.SetDirty(img);
        }
        if(_bg.objectReferenceValue != null && _bgRectTransform != null)
        {
            Image img = _bgRectTransform.GetComponent<Image>();
            img.color = _BGColor.colorValue;
            img.pixelsPerUnitMultiplier = _maxValue.intValue;
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
        float anchorX = _currentValue.intValue / (float)_maxValue.intValue;
        float anchorXMin = 0 / (float)_maxValue.intValue;
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
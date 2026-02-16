using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(SegmentedBarFill))]
public class SegmentedBarFillEditor: Editor
{
    SerializedProperty _image;
    SerializedProperty _color;
    private void OnEnable()
    {
        _image = serializedObject.FindProperty("_fillImage");
        _color = serializedObject.FindProperty("_normalColor");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        if (_image.objectReferenceValue != null)
        {
            (_image.objectReferenceValue as Image).color = _color.colorValue;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
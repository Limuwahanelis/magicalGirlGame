using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

[CustomEditor(typeof(NumberDisplay))]
public class NumberDisplayEditor: Editor
{
    SerializedProperty _showMaxValue;
    SerializedProperty _maxValueText;
    private void OnEnable()
    {
        _showMaxValue = serializedObject.FindProperty("_showMaxValue");
        _maxValueText = serializedObject.FindProperty("_maxValueText");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

            if (!_showMaxValue.boolValue)
            {
                (_maxValueText.objectReferenceValue as TMP_Text).gameObject.SetActive(false);
            }
            else
            {
                (_maxValueText.objectReferenceValue as TMP_Text).gameObject.SetActive(true);
            }
            EditorUtility.SetDirty(_maxValueText.objectReferenceValue as TMP_Text);
        
    }
}
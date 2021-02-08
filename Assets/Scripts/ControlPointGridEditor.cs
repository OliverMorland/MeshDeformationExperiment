using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ControlPointGrid))]
[CanEditMultipleObjects]
public class ControlPointGridEditor : Editor
{

    public string m_saveToPath = "Assets/TargetGridData/EuroManToAsiaFemale4.txt";
    float m_labelWidth = 150f;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ControlPointGrid controlPointGrid = (ControlPointGrid)target;

        GUILayout.Space(20f);
        GUILayout.Label("Saving Grid", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();
        GUILayout.Label("File save path: ", GUILayout.Width(m_labelWidth));
        m_saveToPath = GUILayout.TextField(m_saveToPath);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Save Grid Layout"))
        {
            Debug.Log("Saving Grid layout to " + m_saveToPath);
            controlPointGrid.SaveGrid(m_saveToPath);
        }
    }



}
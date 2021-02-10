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
    int selected = 0;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ControlPointGrid controlPointGrid = (ControlPointGrid)target;

        GUILayout.Space(20f);
        GUILayout.Label("Control Point Selections", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(Resources.Load<Texture>("Thumbnails/nose"), GUILayout.Height(80f), GUILayout.Width(80f)))
        {
            Debug.Log("Selecting nose points");
            GameObject[] noseControlPoints = { controlPointGrid.FindControlPoint(2, 3, 2),
                                               controlPointGrid.FindControlPoint(2, 3, 3)};
            Selection.objects = noseControlPoints;
        }
        if (GUILayout.Button(Resources.Load<Texture>("Thumbnails/cheeks"), GUILayout.Height(80f), GUILayout.Width(80f)))
        {
            Debug.Log("Selecting Cheek points");
            GameObject[] cheekControlPoints = { controlPointGrid.FindControlPoint(1, 3, 2),
                                               controlPointGrid.FindControlPoint(1, 3, 3),
                                               controlPointGrid.FindControlPoint(3, 3, 2),
                                               controlPointGrid.FindControlPoint(3, 3, 3)};
            Selection.objects = cheekControlPoints;
        }
        if (GUILayout.Button(Resources.Load<Texture>("Thumbnails/lobes"), GUILayout.Height(80f), GUILayout.Width(80f)))
        {
            Debug.Log("Selecting Skull Lobes points");
            GameObject[] lobeControlPoints = { controlPointGrid.FindControlPoint(1, 1, 3),
                                               controlPointGrid.FindControlPoint(3, 1, 3)};
            Selection.objects = lobeControlPoints;
        }
        if (GUILayout.Button(Resources.Load<Texture>("Thumbnails/brow"), GUILayout.Height(80f), GUILayout.Width(80f)))
        {
            Debug.Log("Selecting brow points");
            GameObject[] browControlPoints = { controlPointGrid.FindControlPoint(1, 3, 3),
                                                controlPointGrid.FindControlPoint(2, 3, 3),
                                                controlPointGrid.FindControlPoint(3, 3, 3)};
            Selection.objects = browControlPoints;
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button(Resources.Load<Texture>("Thumbnails/neckSideRight"), GUILayout.Height(80f), GUILayout.Width(80f)))
        {
            Debug.Log("Selecting right neck points");
            GameObject[] rightNeckControlPoints = { controlPointGrid.FindControlPoint(3, 2, 1),
                                                controlPointGrid.FindControlPoint(3, 2, 2)};
            Selection.objects = rightNeckControlPoints;
        }
        if (GUILayout.Button(Resources.Load<Texture>("Thumbnails/neckSideLeft"), GUILayout.Height(80f), GUILayout.Width(80f)))
        {
            Debug.Log("Selecting left neck points");
            GameObject[] leftNeckControlPoints = { controlPointGrid.FindControlPoint(1, 2, 1),
                                               controlPointGrid.FindControlPoint(1, 2, 2)};
            Selection.objects = leftNeckControlPoints;
        }
        GUILayout.EndHorizontal();

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
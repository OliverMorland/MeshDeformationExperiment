using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ControlPointGrid : MonoBehaviour
{
    public string m_LoadFromPath;

    public int N;
    public int M;
    public int L;

    class GridData
    {
        public Vector3[] gridPointPositions;
        public Vector3[] basisPointPositions;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("'S' key pressed");
            SaveGrid("Assets/GridData.txt");
        }

        if (Input.GetKey(KeyCode.L))
        {
            Debug.Log("'L' key pressed");
            LoadGrid("Assets/GridData.txt");
        }
    }


    public void SaveGrid(string filePath)
    {
        //Find all point positions
        Vector3[] controlPointsPositions;
        Vector3[] basisPoints;
        controlPointsPositions = new Vector3[transform.childCount];
        for (int i = 0; i < controlPointsPositions.Length; i++)
        {
            controlPointsPositions[i] = transform.GetChild(i).transform.position;
        }

        //Create json text
        GridData gridData = new GridData();
        gridData.gridPointPositions = controlPointsPositions;
        gridData.basisPointPositions = null;
        string jsonString = JsonUtility.ToJson(gridData);

        File.WriteAllText(filePath, jsonString);
    }


    public void LoadGrid(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        GridData gridData = JsonUtility.FromJson<GridData>(jsonString);
        Debug.Log($"Coords {gridData.gridPointPositions[1].z}");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ControlPointGrid : MonoBehaviour
{
    public string m_LoadFromPath;
    public string m_SaveToPath;
    public GameObject[] m_basisPoints;
    Vector3[] basisPointPositions;
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
        //Determine Basis Point positions
        basisPointPositions = new Vector3[m_basisPoints.Length];
        for (int i = 0; i < basisPointPositions.Length; i++)
        {
            basisPointPositions[i] = m_basisPoints[i].transform.position;
        }

        //Load Grid Points
        if (!string.IsNullOrEmpty(m_LoadFromPath))
        {
            LoadGrid(m_LoadFromPath);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            if (!string.IsNullOrEmpty(m_SaveToPath))
            {
                Debug.Log("'S' key pressed");
                SaveGrid(m_SaveToPath);
            }
        }


        if (Input.GetKey(KeyCode.L))
        {
            if (!string.IsNullOrEmpty(m_LoadFromPath))
            {
                Debug.Log("'L' key pressed");
                LoadGrid(m_LoadFromPath);
            }
        }
    }


    public void SaveGrid(string filePath)
    {
        //Find all control point positions
        Vector3[] controlPointsPositions;
        controlPointsPositions = new Vector3[transform.childCount];
        for (int i = 0; i < controlPointsPositions.Length; i++)
        {
            controlPointsPositions[i] = transform.GetChild(i).transform.position;
        }

        //Create json text
        GridData gridData = new GridData();
        gridData.gridPointPositions = controlPointsPositions;
        gridData.basisPointPositions = basisPointPositions;
        string jsonString = JsonUtility.ToJson(gridData);

        File.WriteAllText(filePath, jsonString);
    }


    public void LoadGrid(string filePath)
    {
        //Load grid data
        string jsonString = File.ReadAllText(filePath);
        GridData gridData = JsonUtility.FromJson<GridData>(jsonString);

        //Set control point positions
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.position = gridData.gridPointPositions[i];
        }

    }
}

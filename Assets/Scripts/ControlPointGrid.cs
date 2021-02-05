﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ControlPointGrid : MonoBehaviour
{
    public string m_LoadFromPath;
    public string m_SaveToPath;
    public GameObject[] m_basisPoints;
    public Vector3[] m_basisPointPositions;
    Vector3[] m_controlPointStartPositions;

    GameObject[] m_controlPoints;
    public int N;
    public int M;
    public int L;

    [Header("Grid Controls")]
    public int col;
    public int row;
    public int ele;

    [Range(-0.5f, 0.5f)] public float x_displ = 0;
    [Range(-0.5f, 0.5f)] public float y_displ = 0;
    [Range(-0.5f, 0.5f)] public float z_displ = 0;


    class GridData
    {
        public Vector3[] gridPointPositions;
        public Vector3[] m_basisPointPositions;
        public Vector3 gridOriginPosition;
        public Vector3 gridScale;
    }

    void Start()
    {
        //Get Control Points
        m_controlPoints = new GameObject[transform.childCount];
        m_controlPointStartPositions = new Vector3[transform.childCount];
        for (int i = 0; i < m_controlPoints.Length; i++)
        {
            m_controlPoints[i] = transform.GetChild(i).gameObject;
            m_controlPointStartPositions[i] = transform.GetChild(i).transform.position;

        }

        //Load Grid Points
        if (File.Exists(m_LoadFromPath))
        {
            LoadGrid(m_LoadFromPath);
        }
        else{
            //Determine Basis Point positions
            m_basisPointPositions = new Vector3[m_basisPoints.Length];
            for (int i = 0; i < m_basisPointPositions.Length; i++)
            {
                m_basisPointPositions[i] = m_basisPoints[i].transform.position;
            }
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


        if (Input.GetKeyUp(KeyCode.D))
        {
            Debug.Log("Displacing point");
            displaceControlPoint();
        }


    }


    void displaceControlPoint()
    {
        int index = ((N+1) * (M+1) * ele) + ((N+1) * row) + col;
        m_controlPoints[index].transform.localPosition = m_controlPointStartPositions[index] + new Vector3(x_displ, y_displ, z_displ);
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
        gridData.m_basisPointPositions = m_basisPointPositions;
        gridData.gridOriginPosition = transform.position;
        gridData.gridScale = transform.localScale;
        string jsonString = JsonUtility.ToJson(gridData);

        File.WriteAllText(filePath, jsonString);
    }


    public void LoadGrid(string filePath)
    {
        //Load grid data
        string jsonString = File.ReadAllText(filePath);
        GridData gridData = JsonUtility.FromJson<GridData>(jsonString);

        //Set grid position and scale
        transform.position = gridData.gridOriginPosition;
        transform.localScale = gridData.gridScale;

        //Set Basis Positions
        m_basisPointPositions = gridData.m_basisPointPositions;

        //Set control point positions
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.position = gridData.gridPointPositions[i];
        }

    }

    
}


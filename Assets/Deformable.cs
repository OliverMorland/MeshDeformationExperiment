﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deformable : MonoBehaviour
{
    MeshFilter m_meshFilter;
    Vector3[] m_uvw;
    public GameObject m_BezierSurfacePointPrefab;
    public GameObject[] m_ControlPointsTop;
    public GameObject[] m_ControlPointsBottom;
    GameObject[] SurfacePoints;
    public int n = 2;
    public int m = 2;
    float resolution = 10f;
  
    Vector3[] m_vertices;
    Vector2[] m_uvCoords;

    public float m_speed = 10f;
    public float m_maxAmplitude = 1f;
    public bool m_flipNormals = false;


    // Start is called before the first frame update
    void Start()
    {
        m_meshFilter = GetComponent<MeshFilter>();

        Vector3[] vertices = m_meshFilter.mesh.vertices;

        m_uvw = new Vector3[vertices.Length];


        Vector3 min = m_ControlPointsTop[0].transform.position;
        Vector3 max = m_ControlPointsTop[0].transform.position;
        for (int i = 0; i < m_ControlPointsTop.Length; i++)
        {
            min.x = Mathf.Min(min.x, m_ControlPointsTop[i].transform.position.x);
            min.y = Mathf.Min(min.y, m_ControlPointsTop[i].transform.position.y);
            min.z = Mathf.Min(min.z, m_ControlPointsTop[i].transform.position.z);

            max.x = Mathf.Max(max.x, m_ControlPointsTop[i].transform.position.x);
            max.y = Mathf.Max(max.y, m_ControlPointsTop[i].transform.position.y);
            max.z = Mathf.Max(max.z, m_ControlPointsTop[i].transform.position.z);

        }

        for (int i = 0; i < m_ControlPointsBottom.Length; i++)
        {
            min.x = Mathf.Min(min.x, m_ControlPointsBottom[i].transform.position.x);
            min.y = Mathf.Min(min.y, m_ControlPointsBottom[i].transform.position.y);
            min.z = Mathf.Min(min.z, m_ControlPointsBottom[i].transform.position.z);

            max.x = Mathf.Max(max.x, m_ControlPointsBottom[i].transform.position.x);
            max.y = Mathf.Max(max.y, m_ControlPointsBottom[i].transform.position.y);
            max.z = Mathf.Max(max.z, m_ControlPointsBottom[i].transform.position.z);

        }

        Vector3 delta = max - min;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 v = transform.TransformPoint(vertices[i]);

            //Convert based on reference frame
            Vector3 uvw = Vector3.zero;
            if (Mathf.Approximately(delta.x, 0f))
            {
                uvw.x = 0f;
            }
            else
            {
                uvw.x = (v.x - min.x) / delta.x;
            }

            if (Mathf.Approximately(delta.y, 0f))
            {
                uvw.y = 0f;
            }
            else
            {
                uvw.y = (v.y - min.y) / delta.y;
            }

            if (Mathf.Approximately(delta.z, 0f))
            {
                uvw.z = 0f;
            }
            else
            {
                uvw.z = (v.z - min.z) / delta.z;
            }


            m_uvw[i] = uvw;
        }

        m_meshFilter.mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {

        /*
        //Move control points
        float t = Time.time;
        float amplitude = m_maxAmplitude * Mathf.Sin(m_speed * t) + 0.4f;

        Vector3 displacement = new Vector3(0, amplitude, 0);

        m_ControlPointsTop[3].transform.position = new Vector3(m_ControlPointsTop[3].transform.position.x, amplitude, m_ControlPointsTop[3].transform.position.z);
        m_ControlPointsTop[4].transform.position = new Vector3(m_ControlPointsTop[4].transform.position.x, amplitude, m_ControlPointsTop[4].transform.position.z);
        m_ControlPointsTop[5].transform.position = new Vector3(m_ControlPointsTop[5].transform.position.x, amplitude, m_ControlPointsTop[5].transform.position.z);
        */


        UpdateVertexPositions();

    }





    void UpdateVertexPositions()
    {
        //Populating vertice array
        int arraySize = (int)(resolution * resolution);
        SurfacePoints = new GameObject[arraySize];
        m_vertices = m_meshFilter.mesh.vertices;

        for (int i = 0; i < m_uvw.Length; i++)
        {
            float u = m_uvw[i].z;
            float v = m_uvw[i].x;
            float w = m_uvw[i].y;
            //Vector3 pos = m_uvw[i];
            Vector3 topPos = GetBezierSurfacePosition(v, u, n, m, m_ControlPointsTop);
            Vector3 bottomPos = GetBezierSurfacePosition(v, u, n, m, m_ControlPointsBottom);

            Vector3 pos = Vector3.Lerp(bottomPos, topPos, w);

            m_vertices[i] = transform.InverseTransformPoint(pos);


        }

        //Add vertices to mesh
        for (int i = 0; i < m_uvw.Length; i++)
        {
            Debug.Log($"vertices: {m_vertices[i]} ||| u: {m_uvw[i]}");
        }

        m_meshFilter.mesh.vertices = m_vertices;


        m_meshFilter.mesh.RecalculateNormals();
        //Add mesh to mesh filter
        //m_meshFilter = GetComponent<MeshFilter>();
        //m_meshFilter.mesh = m_mesh;
    }





    GameObject FindPoint(int c, int r, GameObject [] controlPoints)
    {
        return controlPoints[c + ((n + 1) * r)];
    }


    int Factorial(int value)
    {
        int result = value;
        while (value > 1)
        {
            value = value - 1;
            result = result * value;
        }

        return result;
    }



    int Coefficient(int n, int i)
    {
        //Force i to be positive
        i = Mathf.Abs(i);

        if (n < i)
        {
            Debug.Log("WARNING: n must be greater than i");
            return 0;
        }

        if (i == 0 || n - i == 0)
        {
            return 1;
        }

        return Factorial(n) / (Factorial(i) * Factorial(n - i));
    }



    Vector3 GetBezierSurfacePosition(float v, float u, int n, int m, GameObject[] controlPoints)
    {
        Vector3 SUM_V = new Vector3(0, 0, 0);
        for (int c = 0; c <= n; c++)
        {

            Vector3 SUM_U = new Vector3(0, 0, 0);
            for (int r = 0; r <= m; r++)
            {
                float exp_u = Mathf.Pow(u, r) * Mathf.Pow(1 - u, m - r);
                SUM_U += Coefficient(m, r) * exp_u * FindPoint(c, r, controlPoints).transform.position;
            }

            float exp_v = Mathf.Pow(v, c) * Mathf.Pow(1 - v, n - c);
            SUM_V += Coefficient(n, c) * exp_v * SUM_U;
        }

        return SUM_V;
    }




}

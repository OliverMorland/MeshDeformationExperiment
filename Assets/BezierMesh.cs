using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierMesh : MonoBehaviour
{
    public GameObject m_BezierSurfacePointPrefab;
    public GameObject[] m_ControlPoints;
    GameObject[] SurfacePoints;
    public int n = 2;
    public int m = 2;
    float resolution = 10f;
    float v = 0;
    float u = 0;

    MeshFilter m_meshFilter;
    Mesh m_mesh;
    Vector3[] m_vertices;
    Vector2[] m_uvCoords;

    public float m_speed = 10f;
    public float m_maxAmplitude = 1f;
    public bool m_flipNormals = false;

    // Start is called before the first frame update
    void Start()
    {
        m_mesh = new Mesh();

        //Populating vertice array
        int arraySize = (int)(resolution * resolution);
        SurfacePoints = new GameObject[arraySize];
        m_vertices = new Vector3[arraySize];
        m_uvCoords = new Vector2[arraySize];
        for (u = 0; u <= 1; u += (1 / resolution))
        {
            for (v = 0; v <= 1; v += (1 / resolution))
            {
                int index = (int)((v + (resolution * u)) * resolution);
                m_vertices[index] = GetBezierSurfacePosition(v, u, n, m);
                m_uvCoords[index] = new Vector2(u, v);
            }
        }

        //Add vertices to mesh
        for (int i = 0; i < resolution * resolution; i++)
        {
            Debug.Log(m_vertices[i]);
        }
        m_mesh.vertices = m_vertices;

        //Add UV array to mesh
        m_mesh.uv = m_uvCoords;

        //Adding triangles
        int[] trianglePoints = new int[540];

        int counter = 0;
        int res = (int)resolution;
        for (int r = 0; r < res-1; r++)
        {
            for (int c = 0; c < res-1; c++)
            {
                int P0 = c + ((res) * r);
                int P1 = (c + 1) + ((res) * r);
                int P2 = c + ((res) * (r + 1));
                int P3 = (c + 1) + ((res) * (r + 1));

                trianglePoints[(6 * counter) + 0] = P1;
                trianglePoints[(6 * counter) + 1] = P0;
                trianglePoints[(6 * counter) + 2] = P2;
                trianglePoints[(6 * counter) + 3] = P2;
                trianglePoints[(6 * counter) + 4] = P3;
                trianglePoints[(6 * counter) + 5] = P1;

                if (m_flipNormals == true)
                {
                    trianglePoints[(6 * counter) + 0] = P1;
                    trianglePoints[(6 * counter) + 1] = P3;
                    trianglePoints[(6 * counter) + 2] = P2;
                    trianglePoints[(6 * counter) + 3] = P2;
                    trianglePoints[(6 * counter) + 4] = P0;
                    trianglePoints[(6 * counter) + 5] = P1;
                }

                counter++;
            }
        }
        m_mesh.triangles = trianglePoints;

        //Add normals to mesh
        m_mesh.RecalculateNormals();
        /*
        if (m_flipNormals == true)
        {
            Vector3[] normals = m_mesh.normals;
            for (int i = 0; i < normals.Length; i++)
            {
                normals[i] = normals[i] * -1f;
                Debug.DrawLine(m_vertices[i], m_vertices[i] + normals[i]);
            }
            m_mesh.normals = normals;
        }
        */

        //Add mesh to mesh filter
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshFilter.mesh = m_mesh;
    }

    // Update is called once per frame
    void Update()
    {

        //Update Bezier surface points
        if (Input.GetKeyUp(KeyCode.U))
        {
            Debug.Log("Updating..");
            UpdateVertexPositions();
        }

        //Move control points
        float t = Time.time;
        float amplitude =  m_maxAmplitude * Mathf.Sin(m_speed * t);

        Vector3 displacement = new Vector3(0, amplitude, 0);

        m_ControlPoints[3].transform.position = new Vector3(amplitude, m_ControlPoints[3].transform.position.y, m_ControlPoints[3].transform.position.z);
        m_ControlPoints[4].transform.position = new Vector3(amplitude, m_ControlPoints[4].transform.position.y, m_ControlPoints[4].transform.position.z);
        m_ControlPoints[5].transform.position = new Vector3(amplitude, m_ControlPoints[5].transform.position.y, m_ControlPoints[5].transform.position.z);

        UpdateVertexPositions();

    }





    void UpdateVertexPositions()
    {
        //Populating vertice array
        int arraySize = (int)(resolution * resolution);
        SurfacePoints = new GameObject[arraySize];
        m_vertices = new Vector3[arraySize];
        for (u = 0; u <= 1; u += (1 / resolution))
        {
            for (v = 0; v <= 1; v += (1 / resolution))
            {
                int index = (int)((v + (resolution * u)) * resolution);
                m_vertices[index] = GetBezierSurfacePosition(v, u, n, m);
            }
        }

        //Add vertices to mesh
        for (int i = 0; i < resolution * resolution; i++)
        {
            Debug.Log(m_vertices[i]);
        }
        m_mesh.vertices = m_vertices;

        //Add mesh to mesh filter
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshFilter.mesh = m_mesh;
    }


    GameObject FindPoint(int c, int r)
    {
        return m_ControlPoints[c + ((n + 1) * r)];
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



    Vector3 GetBezierSurfacePosition(float v, float u, int n, int m)
    {
        Vector3 SUM_V = new Vector3(0, 0, 0);
        for (int c = 0; c <= n; c++)
        {

            Vector3 SUM_U = new Vector3(0, 0, 0);
            for (int r = 0; r <= m; r++)
            {
                float exp_u = Mathf.Pow(u, r) * Mathf.Pow(1 - u, m - r);
                SUM_U += Coefficient(m, r) * exp_u * FindPoint(c, r).transform.position;
            }

            float exp_v = Mathf.Pow(v, c) * Mathf.Pow(1 - v, n - c);
            SUM_V += Coefficient(n, c) * exp_v * SUM_U;
        }

        return SUM_V;
    }



   
}

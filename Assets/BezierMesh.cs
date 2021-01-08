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

    // Start is called before the first frame update
    void Start()
    {
        m_mesh = new Mesh();

        int arraySize = (int)(resolution * resolution);
        SurfacePoints = new GameObject[arraySize];
        m_vertices = new Vector3[arraySize];
        for (u = 0; u <= 1; u += (1 / resolution))
        {
            for (v = 0; v <= 1; v += (1 / resolution))
            {
                //GameObject newBezierSurfacePoint = Instantiate(m_BezierSurfacePointPrefab, transform);
                //newBezierSurfacePoint.transform.position = GetBezierSurfacePosition(v, u, n, m);
                int index = (int)((v + (resolution * u)) * resolution);
                //SurfacePoints[index] = newBezierSurfacePoint;

                m_vertices[index] = GetBezierSurfacePosition(v, u, n, m);
            }
        }

        //Add vertices to mesh
        for (int i = 0; i < resolution * resolution; i++)
        {
            Debug.Log(m_vertices[i]);
        }
        m_mesh.vertices = m_vertices;

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

                counter++;
            }
        }
        m_mesh.triangles = trianglePoints;

        //Add normals to mesh
        m_mesh.RecalculateNormals();

        //Add mesh to mesh filter
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshFilter.mesh = m_mesh;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //Update Bezier surface points
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Updating..");

            for (u = 0; u <= 1; u += (1 / resolution))
            {
                for (v = 0; v <= 1; v += (1 / resolution))
                {
                    int index = (int)((v + (resolution * u)) * resolution);
                    SurfacePoints[index].transform.position = GetBezierSurfacePosition(v, u, n, m);

                    //Instead of surface points we must update the vertices
                    m_vertices[index] = GetBezierSurfacePosition(v, u, n, m);
                }
            }

            //Add vertices to mesh
            m_mesh.vertices = m_vertices;

            //Adding triangles
            m_mesh.triangles = CreateTriangles(n, m);

            //Add normals to mesh
            m_mesh.RecalculateNormals();

            //Add mesh to mesh filter
            m_meshFilter = GetComponent<MeshFilter>();
            m_meshFilter.mesh = m_mesh;

        }
        */
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


    void UpdateMesh()
    {
        //Add vertices to mesh
        m_mesh.vertices = CreateVertices(n, m);

        //Adding triangles
        m_mesh.triangles = CreateTriangles(n, m);

        //Add normals to mesh
        m_mesh.RecalculateNormals();

        //Add mesh to mesh filter
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshFilter.mesh = m_mesh;
    }


    Vector3[] CreateVertices(int _n, int _m)
    {
        Vector3[] vertices = new Vector3[(_n + 1) * (_m + 1)];

        for (int r = 0; r <= _m; r++)
        {
            for (int c = 0; c <= _n; c++)
            {
                vertices[c + ((_n + 1) * r)] = new Vector3(c, r, 0);
            }
        }

        return vertices;
    }



    int[] CreateTriangles(int _n, int _m)
    {
        int[] trianglePoints = new int[6 * _n * _m];

        int counter = 0;
        for (int r = 0; r < _m; r++)
        {
            for (int c = 0; c < _n; c++)
            {
                int P0 = c + ((_n + 1) * r);
                int P1 = (c + 1) + ((_n + 1) * r);
                int P2 = c + ((_n + 1) * (r + 1));
                int P3 = (c + 1) + ((_n + 1) * (r + 1));

                trianglePoints[(6 * counter) + 0] = P1;
                trianglePoints[(6 * counter) + 1] = P0;
                trianglePoints[(6 * counter) + 2] = P2;
                trianglePoints[(6 * counter) + 3] = P2;
                trianglePoints[(6 * counter) + 4] = P3;
                trianglePoints[(6 * counter) + 5] = P1;

                counter++;
            }
        }

        return trianglePoints;
    }
}

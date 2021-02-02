using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deformable : MonoBehaviour
{
    MeshFilter m_meshFilter;
    Vector3[] m_uvw;
    public GameObject m_BezierSurfacePointPrefab;
    public GameObject[] m_ControlPoints;
    public GameObject[] m_ControlPointsTop;
    public GameObject[] m_ControlPointsBottom;
    GameObject[] SurfacePoints;
    public int n = 3;
    public int m = 3;
    public int l = 1;
    float resolution = 10f;
  
    Vector3[] m_vertices;
    Vector2[] m_uvCoords;

    public float m_speed = 10f;
    public float m_maxAmplitude = 1f;
    public int m_layerIndex = 0;
    public bool m_flipNormals = false;


    // Start is called before the first frame update
    void Start()
    {
        m_meshFilter = GetComponent<MeshFilter>();

        Vector3[] vertices = m_meshFilter.mesh.vertices;

        m_uvw = new Vector3[vertices.Length];

        Vector3 min = m_ControlPoints[0].transform.position;
        Vector3 max = m_ControlPoints[0].transform.position;
        for (int i = 0; i < m_ControlPoints.Length; i++)
        {
            min.x = Mathf.Min(min.x, m_ControlPoints[i].transform.position.x);
            min.y = Mathf.Min(min.y, m_ControlPoints[i].transform.position.y);
            min.z = Mathf.Min(min.z, m_ControlPoints[i].transform.position.z);

            max.x = Mathf.Max(max.x, m_ControlPoints[i].transform.position.x);
            max.y = Mathf.Max(max.y, m_ControlPoints[i].transform.position.y);
            max.z = Mathf.Max(max.z, m_ControlPoints[i].transform.position.z);

        }

        /*
        for (int i = 0; i < m_ControlPointsBottom.Length; i++)
        {
            min.x = Mathf.Min(min.x, m_ControlPointsBottom[i].transform.position.x);
            min.y = Mathf.Min(min.y, m_ControlPointsBottom[i].transform.position.y);
            min.z = Mathf.Min(min.z, m_ControlPointsBottom[i].transform.position.z);

            max.x = Mathf.Max(max.x, m_ControlPointsBottom[i].transform.position.x);
            max.y = Mathf.Max(max.y, m_ControlPointsBottom[i].transform.position.y);
            max.z = Mathf.Max(max.z, m_ControlPointsBottom[i].transform.position.z);
        }
        */

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


            //Determine Control Points to use
            int layerIndex = 0;
            float layer_w = 0;
            while (layer_w < w)
            {
                layer_w += 1 / l;
                layerIndex++;
            }

            GameObject[] controlPointsTopLayer = m_ControlPointsTop;
            GameObject[] controlPointsBottonLayer = m_ControlPointsBottom;


            //Find positions over top and bottom layers of grid
            Vector3 topPos = GetBezierSurfacePosition(v, u, n, m, controlPointsTopLayer);
            Vector3 bottomPos = GetBezierSurfacePosition(v, u, n, m, controlPointsBottonLayer);

            //Interpolate position using w and top and bottom points
            Vector3 pos = Vector3.Lerp(bottomPos, topPos, w);
            m_vertices[i] = transform.InverseTransformPoint(pos);


        }

        //Add vertices to mesh
        m_meshFilter.mesh.vertices = m_vertices;
        m_meshFilter.mesh.RecalculateNormals();
        //Add mesh to mesh filter
        //m_meshFilter = GetComponent<MeshFilter>();
        //m_meshFilter.mesh = m_mesh;
    }


    GameObject [] GetControlPointsOfLayer(int layerIndex)
    {
        GameObject[] layerControlPoints = new GameObject[(n + 1) * (m + 1)];
        for (int i = 0; i < layerControlPoints.Length; i++)
        {
            layerControlPoints[i] = m_ControlPoints[i + layerIndex * (n + 1) * (m + 1)];
        }

        return layerControlPoints;
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

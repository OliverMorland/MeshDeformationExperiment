using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Deformable : MonoBehaviour
{
    MeshFilter m_meshFilter;
    Vector3[] m_uvw;
    public ControlPointGrid m_ControlPointsGrid;
    public string m_meshFileName = "exampleMesh";

    GameObject[] m_ControlPoints;
    GameObject[] SurfacePoints;
    int n = 3;
    int m = 3;
    int l = 1;
    float resolution = 10f;
  
    Vector3[] m_vertices;
    Vector2[] m_uvCoords;

    int m_layerIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        //Populate control points
        m_ControlPoints = new GameObject[m_ControlPointsGrid.transform.childCount];
        for (int i = 0; i < m_ControlPoints.Length; i++)
        {
            m_ControlPoints[i] = m_ControlPointsGrid.transform.GetChild(i).gameObject;
        }
        n = m_ControlPointsGrid.N;
        m = m_ControlPointsGrid.M;
        l = m_ControlPointsGrid.L;

        m_meshFilter = GetComponent<MeshFilter>();

        Vector3[] vertices = m_meshFilter.mesh.vertices;

        m_uvw = new Vector3[vertices.Length];

        GameObject[] basisPoints = m_ControlPointsGrid.m_basisPoints;
        Vector3 min = basisPoints[0].transform.position;
        Vector3 max = basisPoints[0].transform.position;
        for (int i = 0; i < basisPoints.Length; i++)
        {
            min.x = Mathf.Min(min.x, basisPoints[i].transform.position.x);
            min.y = Mathf.Min(min.y, basisPoints[i].transform.position.y);
            min.z = Mathf.Min(min.z, basisPoints[i].transform.position.z);

            max.x = Mathf.Max(max.x, basisPoints[i].transform.position.x);
            max.y = Mathf.Max(max.y, basisPoints[i].transform.position.y);
            max.z = Mathf.Max(max.z, basisPoints[i].transform.position.z);

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

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Saving FBX");
            AssetDatabase.CreateAsset(m_meshFilter.mesh, $"Assets/DeformedMeshes/{m_meshFileName}.asset");
            AssetDatabase.SaveAssets();
        }


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

            GameObject[] controlPointsTopStack;
            GameObject[] controlPointsBottomStack;

            GetControlPoints(m_uvw[i], out controlPointsBottomStack, out controlPointsTopStack);

            //Find positions over top and bottom layers of grid
            Vector3 topPos = GetBezierSurfacePosition(v, u, 2, 2, controlPointsTopStack);
            Vector3 bottomPos = GetBezierSurfacePosition(v, u, 2, 2, controlPointsBottomStack);

            //Interpolate position using w and top and bottom points
            float converted_w = w - (float)((int)w);
            Vector3 pos = Vector3.Lerp(bottomPos, topPos, converted_w);
            m_vertices[i] = transform.InverseTransformPoint(pos);


        }

        //Add vertices to mesh
        m_meshFilter.mesh.vertices = m_vertices;
        m_meshFilter.mesh.RecalculateNormals();
        //Add mesh to mesh filter
        //m_meshFilter = GetComponent<MeshFilter>();
        //m_meshFilter.mesh = m_mesh;
    }

    void GetStackPoints()
    {
        //Threshold uvw values, e,g v = 1.2 so v_index = 1
        int v_index = (int)m_uvw[2].x;
        int w_index = (int)m_uvw[2].y;
        int u_index = (int)m_uvw[2].z;

        Debug.Log($"v: {v_index}, w: {w_index}, u: {u_index}");

        //Finding bottom layer control points
        GameObject[] bottomLayer = new GameObject[(n + 1) * (m + 1)];
        for (int i = 0; i < bottomLayer.Length; i++)
        {
            int index = i + w_index * (n + 1) * (m + 1);
            bottomLayer[i] = m_ControlPoints[index];
        }

        //Finding top layer control points
        GameObject[] topLayer = new GameObject[(n + 1) * (m + 1)];
        w_index++;
        for (int i = 0; i < topLayer.Length; i++)
        {
            int index = i + w_index * (n + 1) * (m + 1);
            topLayer[i] = m_ControlPoints[index];
        }

        //Finding stack Control Points
        GameObject[] topStack = new GameObject[9];
        GameObject[] bottomStack = new GameObject[9];
        for (int c = (v_index * 2); c < (v_index * 2) + 3; c++)
        {
            for (int r = (u_index * 2); r < (u_index * 2) + 3; r++)
            {
                int stackIndex = (c - (v_index * 2)) + (3 * (r - u_index * 2));
                topStack[stackIndex] = FindPoint(c, r, n, topLayer);
                bottomStack[stackIndex] = FindPoint(c, r, n, bottomLayer);
            }
        }

        //Resize Stack Contol points
        for (int i = 0; i < 9; i++)
        {
            topStack[i].transform.localScale *= 2f;
            bottomStack[i].transform.localScale *= 2f;
        }


    }


    GameObject [] GetControlPointsOfLayer(int layerIndex)
    {
        GameObject[] layerControlPoints = new GameObject[(n + 1) * (m + 1)];
        Debug.Log("Control Points in layer: " + layerControlPoints.Length);
        for (int i = 0; i < layerControlPoints.Length; i++)
        {
            int index = i + layerIndex * (n + 1) * (m + 1);
            layerControlPoints[i] = m_ControlPoints[index];
        }

        return layerControlPoints;
    }


    void GetControlPoints(Vector3 uvw, out GameObject[] bottomStack, out GameObject [] topStack)
    {
        //Threshold uvw values, e,g v = 1.2 so v_index = 1
        int v_index = (int)uvw.x;
        int w_index = (int)uvw.y;
        int u_index = (int)uvw.z;

        //Finding bottom layer control points
        GameObject[] bottomLayer = new GameObject[(n + 1) * (m + 1)];
        for (int i = 0; i < bottomLayer.Length; i++)
        {
            int index = i + w_index * (n + 1) * (m + 1);
            bottomLayer[i] = m_ControlPoints[index];
        }

        //Finding top layer control points
        GameObject[] topLayer = new GameObject[(n + 1) * (m + 1)];
        w_index ++;
        for (int i = 0; i < topLayer.Length; i++)
        {
            int index = i + w_index * (n + 1) * (m + 1);
            topLayer[i] = m_ControlPoints[index];
        }

        //Finding stack Control Points
        topStack = new GameObject[9];
        bottomStack = new GameObject[9];
        for (int c = (v_index * 2); c < (v_index * 2) + 3; c++)
        {
            for (int r = (u_index * 2); r < (u_index * 2) + 3; r++)
            {
                int stackIndex = (c - (v_index * 2)) + (3 * (r - u_index * 2));
                topStack[stackIndex] = FindPoint(c, r, n, topLayer);
                bottomStack[stackIndex] = FindPoint(c, r, n, bottomLayer);
            }
        }
    }


    GameObject FindPoint(int c, int r, int n, GameObject [] controlPoints)
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
        u = u - (float)((int)u);
        v = v - (float)((int)v);

        Vector3 SUM_V = new Vector3(0, 0, 0);
        for (int c = 0; c <= n; c++)
        {
            Vector3 SUM_U = new Vector3(0, 0, 0);
            for (int r = 0; r <= m; r++)
            {
                float exp_u = Mathf.Pow(u, r) * Mathf.Pow(1 - u, m - r);
                SUM_U += Coefficient(m, r) * exp_u * FindPoint(c, r, n, controlPoints).transform.position;
            }

            float exp_v = Mathf.Pow(v, c) * Mathf.Pow(1 - v, n - c);
            SUM_V += Coefficient(n, c) * exp_v * SUM_U;
        }

        return SUM_V;
    }

}

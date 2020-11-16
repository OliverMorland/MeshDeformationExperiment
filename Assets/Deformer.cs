using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deformer : MonoBehaviour
{
    class PointUVW
    {
        public float u;
        public float v;
        public float w;
        public GameObject[] effectiveGridPoints;

        public PointUVW(GameObject [] _effectiveGridPoints, Vector3 _meshPointPos, float _gridUnitLength)
        {
            /*
            u = _u;
            v = _v;
            w = _w;
            */
            effectiveGridPoints = _effectiveGridPoints;


            u = Mathf.Abs((_meshPointPos.x - _effectiveGridPoints[0].transform.position.x) / _gridUnitLength);
            v = Mathf.Abs((_meshPointPos.y - _effectiveGridPoints[0].transform.position.y) / _gridUnitLength);
            w = Mathf.Abs((_meshPointPos.z - _effectiveGridPoints[0].transform.position.z) / _gridUnitLength);

        }



        public Vector3 [] GetEffectiveGridPointPositions()
        {
            Vector3[] effectiveGridPointPositions = new Vector3[8];
            for (int i = 0; i < 8; i++)
            {
                effectiveGridPointPositions[i] = effectiveGridPoints[i].transform.position;
            }
            return effectiveGridPointPositions;
        }

    }

    //Grid dimensions
    public int width = 3;
    public int height = 3;
    public int depth = 3;

    //Grid
    public GameObject m_GridPointPrefab;
    GameObject[] m_GridPoints;

    //Mesh Point
    public GameObject[] m_MeshPoints;
    PointUVW[] m_MeshPoints_uvw;


    // Start is called before the first frame update
    void Start()
    {
        //Initialise Array
        m_GridPoints = new GameObject[width * height * depth];

        //Instantiate grid points
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                for (int d = 0; d < depth; d++)
                {
                    GameObject newPoint = Instantiate(m_GridPointPrefab, transform);
                    newPoint.transform.position = new Vector3(w, h, d);
                    newPoint.name = string.Format("x: {0}, y: {1}, z: {2}", w, h, d);
                    int idx = d + (h * depth) + (w * depth * height);
                    m_GridPoints[idx] = newPoint;
                }
            }
        }


        //Determine mesh points uvw values
        float length = (m_GridPoints[1].transform.position.z - m_GridPoints[0].transform.position.z);
        m_MeshPoints_uvw = new PointUVW[m_MeshPoints.Length];
        for (int i = 0; i < m_MeshPoints.Length; i++)
        {
            GameObject[] effectiveGridPoints = FindSurroundingGridPoints(m_MeshPoints[i].transform.position);
            float u = Mathf.Abs((m_MeshPoints[i].transform.position.x - effectiveGridPoints[0].transform.position.x) / length);
            float v = Mathf.Abs((m_MeshPoints[i].transform.position.y - effectiveGridPoints[0].transform.position.y) / length);
            float w = Mathf.Abs((m_MeshPoints[i].transform.position.z - effectiveGridPoints[0].transform.position.z) / length);
            Debug.Log(string.Format("u: {0}, v: {1}, w: {2}", u, v, w));
            m_MeshPoints_uvw[i] = new PointUVW(effectiveGridPoints, m_MeshPoints[i].transform.position, length);
        }

    }


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_MeshPoints.Length; i++)
        {
            m_MeshPoints[i].transform.position = GetPointPos(m_MeshPoints_uvw[i]);
        }
    }



    GameObject FindGridPoint(int _w, int _h, int _d)
    {
        int index = _d + (_h * depth) + (_w * depth * height);

        if (index < m_GridPoints.Length)
        {
            return m_GridPoints[index];
        }
        else
        {
            return null;
        }
    }


    bool IsPointInGrid(Vector3 pointPos)
    {
        if (pointPos.x > width || pointPos.x < 0)
        {
            return false;
        }
        if (pointPos.y > height || pointPos.y < 0)
        {
            return false;
        }
        if (pointPos.z > depth || pointPos.z < 0)
        {
            return false;
        }

        return true;
    }


    GameObject[] FindSurroundingGridPoints(Vector3 pointPos)
    {
        int x_coord = 0;
        int y_coord = 0;
        int z_coord = 0;

        //Finding grid coords
        for (float w = 0; w < pointPos.x; w++)
        {
            x_coord = (int)w;
        }
        for (float h = 0; h < pointPos.y; h++)
        {
            y_coord = (int)h;
        }
        for (float d = 0; d < pointPos.z; d++)
        {
            z_coord = (int)d;
        }

        //Define effective grid points
        GameObject A = FindGridPoint(x_coord + 1, y_coord, z_coord + 1);
        GameObject B = FindGridPoint(x_coord, y_coord, z_coord + 1);
        GameObject C = FindGridPoint(x_coord + 1, y_coord + 1, z_coord + 1);
        GameObject D = FindGridPoint(x_coord, y_coord + 1, z_coord + 1);
        GameObject E = FindGridPoint(x_coord + 1, y_coord + 1, z_coord);
        GameObject F = FindGridPoint(x_coord, y_coord + 1, z_coord);
        GameObject G = FindGridPoint(x_coord + 1, y_coord, z_coord);
        GameObject H = FindGridPoint(x_coord, y_coord, z_coord);

        GameObject[] effectiveGridPoints = { A, B, C, D, E, F, G, H };

        //Check to see if points are in grid
        foreach (GameObject gridPoint in effectiveGridPoints)
        {
            if (gridPoint == null)
            {
                return null;
            }
        }

        return effectiveGridPoints;
    }


    Vector3 GetPointPos(PointUVW point_UVW_data)
    {

        float u = point_UVW_data.u;
        float v = point_UVW_data.v;
        float w = point_UVW_data.w;
        Vector3[] vertices = point_UVW_data.GetEffectiveGridPointPositions();


        //Base point
        //horizontal points
        Vector3 x = vertices[0] + (vertices[6] - vertices[0]) * w;
        Vector3 y = vertices[1] + (vertices[7] - vertices[1]) * w;

        //vertical points
        Vector3 i = vertices[0] + (vertices[1] - vertices[0]) * u;
        Vector3 j = vertices[6] + (vertices[7] - vertices[6]) * u;

        Debug.DrawLine(x, y, Color.red);
        Debug.DrawLine(i, j, Color.blue);
        Vector3 BasePoint = x + (y - x) * u;


        //Top Point
        //horizontal points
        Vector3 a = vertices[2] + (vertices[4] - vertices[2]) * w;
        Vector3 b = vertices[3] + (vertices[5] - vertices[3]) * w;

        //vertical points
        Vector3 c = vertices[2] + (vertices[3] - vertices[2]) * u;
        Vector3 d = vertices[4] + (vertices[5] - vertices[4]) * u;

        Debug.DrawLine(a, b, Color.red);
        Debug.DrawLine(c, d, Color.blue);
        Vector3 TopPoint = a + (b - a) * u;

        Debug.DrawLine(BasePoint, TopPoint, Color.green);

        return BasePoint + (TopPoint - BasePoint) * v;
    }
}

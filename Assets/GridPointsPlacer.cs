using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPointsPlacer : MonoBehaviour
{

    public int width = 3;
    public int height = 3;
    public int depth = 3;

    public GameObject m_GridPointPrefab;
    public GameObject MeshPoint;
    GameObject[] m_GridPoints;

    // Start is called before the first frame update
    void Start()
    {
        //Initialise Array
        m_GridPoints = new GameObject[width * height * depth];

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
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (IsPointInGrid(MeshPoint.transform.position) == false)
            {
                Debug.Log("Point is outside grid");
                return;
            }

            foreach (GameObject gridPoint in FindSurroundingGridPoints(MeshPoint.transform.position))
            {
                gridPoint.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }
        }
    }


    GameObject FindGridPoint(int _w, int _h, int _d)
    {
        int index = _d + (_h * depth) + (_w * depth * height);

        if (index < m_GridPoints.Length)
        {
            return m_GridPoints[index];
        }
        else{
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectingDeformer : MonoBehaviour
{

    //Grid
    GameObject[] m_GridPoints;

    //Point
    public GameObject P;
    public GameObject[] Points;
    PointUVW[] Points_uvw;

    //Cube vertices
    public GameObject CubeObject;
    public GameObject [] VertexObjects = new GameObject[8];
    Vector3[] vertices = new Vector3[8];



    private void Start()
    {

        for (int i = 0; i < 8; i++)
        {
            vertices[i] = VertexObjects[i].transform.position;
        }

        float length = (vertices[1] - vertices[0]).magnitude;
        Points_uvw = new PointUVW[Points.Length];
        for (int i = 0; i < Points.Length; i++)
        {
            //Determine correct gridpoints
            Vector3 [] effectiveGridPoints = FindSurroundingGridPoints(Points[i].transform.position);
 
            //Detrmine uvw
            float u = Mathf.Abs((Points[i].transform.position.x - vertices[0].x) / length);
            float v = Mathf.Abs((Points[i].transform.position.y - vertices[0].y) / length);
            float w = Mathf.Abs((Points[i].transform.position.z - vertices[0].z) / length);
            Points_uvw[i] = new PointUVW(u, v, w);
            //Points_uvw[i] = new PointUVW(u, v, w, effectiveGridPoints)
        }
        


    }

    private void Update()
    {

        for (int i = 0; i < 8; i++)
        {
            vertices[i] = VertexObjects[i].transform.position;
        }


        for (int i = 0; i < Points.Length; i++)
        {
             Points[i].transform.position = GetPointPos(Points_uvw[i].u, Points_uvw[i].v, Points_uvw[i].w);
        }

    }


    Vector3 GetPointPos(float u, float v, float w)
    {
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


    Vector3 [] FindSurroundingGridPoints(Vector3 pointPos)
    {
        float x_coord = 0;
        float y_coord = 0;
        float z_coord = 0;

        for (float w = 0; w < pointPos.x; w++)
        {
            x_coord = w;
        }
        for (float h = 0; h < pointPos.y; h++)
        {
            y_coord = h;
        }
        for (float d = 0; d < pointPos.z; d++)
        {
            z_coord = d;
        }

        Debug.Log(string.Format("x: {0}, y: {1}, z: {2}", x_coord, y_coord, z_coord));

        //Define points
        Vector3 A;
        Vector3 B;
        Vector3 C;
        Vector3 D;
        Vector3 E;
        Vector3 F;
        Vector3 G;
        Vector3 H;

        Vector3[] gridPoints = new Vector3[8];
        for (int i = 0; i < 8; i++)
        {
            gridPoints[i] = VertexObjects[i].transform.position;
        }

        return gridPoints;
    }



    GameObject GetGridPoint(int w, int h, int d)
    {
        return m_GridPoints[w * h * d];
    }


    class PointUVW{

        public float u;
        public float v;
        public float w;
        //public Vector3 [] gridPoints;

        public PointUVW (float _u, float _v, float _w)
        {
            u = _u;
            v = _v;
            w = _w;

            //Effective Grid Points
            //gridPoints = _gridPoints
        }

    }



}

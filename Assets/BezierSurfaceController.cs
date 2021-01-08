using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierSurfaceController : MonoBehaviour
{
    public GameObject m_ControlPointPrefab;
    public GameObject m_BezierSurfacePointPrefab;
    //public GameObject m_Point;
    public GameObject m_ControlPointsParent;
    public GameObject m_SurfacePointsParent;
    public int n;
    public int m;
    [Range(0, 1)] public float u = 0.5f;
    [Range(0, 1)] public float v = 0.5f;
    public float resolution = 10f;

    private GameObject[] P;
    private GameObject[] SurfacePoints;


    // Start is called before the first frame update
    void Start()
    {
        AddPoints();

        int arraySize = (int)(resolution * resolution);
        SurfacePoints = new GameObject[arraySize];
        for (u = 0; u <= 1; u += (1 / resolution))
        {
            for (v = 0; v <= 1; v += (1 / resolution))
            {
                GameObject newBezierSurfacePoint = Instantiate(m_BezierSurfacePointPrefab, m_SurfacePointsParent.transform);
                newBezierSurfacePoint.transform.position = GetBezierSurfacePosition(v, u, n, m);
                int index = (int)((v + (resolution * u)) * resolution);
                SurfacePoints[index] = newBezierSurfacePoint;
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        //m_Point.transform.position = GetBezierSurfacePosition(v, u, n, m);


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
                }
            }
        }
    }



    void AddPoints()
    {
        P = new GameObject[(n + 1) * (m + 1)];

        for (int r = 0; r <= m; r++)
        {
            for (int c = 0; c <= n; c++)
            {
                GameObject newControlPoint = Instantiate(m_ControlPointPrefab, m_ControlPointsParent.transform);
                P[c + ((n + 1) * r)] = newControlPoint;
                newControlPoint.name = string.Format("c: {0}, r: {1}", c, r);
                newControlPoint.transform.position = new Vector3(c, r, 0);
            }
        }
    }



    GameObject FindPoint(int c, int r)
    {
        return P[c + ((n +1) * r)];
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

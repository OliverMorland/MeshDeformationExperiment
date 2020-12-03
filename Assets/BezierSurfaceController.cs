using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierSurfaceController : MonoBehaviour
{
    public GameObject m_ControlPointPrefab;
    public GameObject m_Point;
    public int n;
    public int m;
    [Range(0, 1)] public float u = 0.5f;
    [Range(0, 1)] public float v = 0.5f;

    public int find_C = 0;
    public int find_R = 0;

    private GameObject[] P;


    // Start is called before the first frame update
    void Start()
    {
        AddPoints();


    }

    // Update is called once per frame
    void Update()
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
            SUM_V += Coefficient(n, c) * exp_v * FindPoint(c, 0).transform.position;// * SUM_U;
        }
        m_Point.transform.position = SUM_V;



        /*
        //Test U
        Vector3 SUM_U = new Vector3(0, 0, 0);
        for (int r = 0; r <= m; r++)
        {
            float exp_u = Mathf.Pow(u, r) * Mathf.Pow(1 - u, m - r);
            SUM_U += Coefficient(m, r) * exp_u * FindPoint(0, r).transform.position;
        }
        m_Point.transform.position = SUM_U;
        */



        if (Input.GetKeyUp(KeyCode.Space))
        {
            print(FindPoint(find_C, find_R).name);
        }


    }


    void AddPoints()
    {
        P = new GameObject[(n + 1) * (m + 1)];

        for (int c = 0; c <= n; c++)
        {
            for (int r = 0; r <= m; r++)
            {
                GameObject newControlPoint = Instantiate(m_ControlPointPrefab, transform);
                P[c + (n * r)] = newControlPoint;
                newControlPoint.name = string.Format("c: {0}, r: {1}", c, r);
                newControlPoint.transform.position = new Vector3(c, r, 0);
            }
        }
    }



    GameObject FindPoint(int c, int r)
    {
        print(P[c + (n * r)].name);
        return P[c + (n * r)];
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

}

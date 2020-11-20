using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierController : MonoBehaviour
{

    public GameObject[] P;
    public GameObject C;
    [Range(0, 1)] public float t = 0.5f;


    // Update is called once per frame
    void Update()
    {
        int n = P.Length - 1;

        Vector3 C_pos = new Vector3(0, 0, 0);
        for (int i = 0; i <= n; i++)
        {
            float exponents = Mathf.Pow(t, i) * Mathf.Pow(1 - t, n - i);
            C_pos += Coefficient(n, i) * exponents * P[i].transform.position;
        }
        C.transform.position = C_pos;
    }



    int Factorial(int value)
    {
        int result = value;
        while(value > 1)
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

        if (i == 0 || n - i == 0){
            return 1;
        }

        return Factorial(n) / (Factorial(i) * Factorial(n - i));
    }


}

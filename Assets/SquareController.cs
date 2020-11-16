using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour
{

    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;
    public GameObject P;
    public Vector3 m_startPosition = new  Vector3(0.1f, 0.1f, 0);

    float u;
    float v;
    float w;


    // Start is called before the first frame update
    void Start()
    {

        float sidelength = (B.transform.position - A.transform.position).magnitude;
        u = (P.transform.position.x - A.transform.position.x) / sidelength;
        v = (P.transform.position.y - A.transform.position.y) / sidelength;
        w = (P.transform.position.z - A.transform.position.z) / sidelength;

    }

    // Update is called once per frame
    void Update()
    {
        //Update P position
        Vector3 d = (D.transform.position - C.transform.position) - (B.transform.position - A.transform.position);
        Vector3 u_vec = (B.transform.position - A.transform.position) + d * v;
        Vector3 pos = A.transform.position + v * (C.transform.position - A.transform.position) + u * u_vec;
        P.transform.position = pos;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    public GameObject m_VerticeController;
    public GameObject m_Cube;
    public MeshFilter m_CubeMeshFilter;
    Vector3[] m_CubeVerticePositions;


    // Start is called before the first frame update
    void Start()
    {

        m_CubeVerticePositions = m_CubeMeshFilter.mesh.vertices;
        for (int i = 0; i < 8; i++)
        {
            GameObject newVertContr = Instantiate(m_VerticeController);
            newVertContr.name = "v: " + i;
            newVertContr.transform.position = m_Cube.transform.position + m_CubeVerticePositions[i];
        }


        for (int i = 0; i < m_CubeMeshFilter.mesh.vertices.Length; i++)
        {
            m_CubeMeshFilter.mesh.vertices[i] = new Vector3(0, 1, 0);
        }
        m_CubeMeshFilter.mesh.RecalculateNormals();
        m_CubeMeshFilter.mesh.RecalculateBounds();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

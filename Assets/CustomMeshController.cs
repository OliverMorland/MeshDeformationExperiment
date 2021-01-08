using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMeshController : MonoBehaviour
{

    public int n = 2;
    public int m = 2;
    private Vector3[] P;

    MeshFilter m_meshFilter;
    Mesh m_mesh;
    Vector3[] m_vertices;

    public GameObject Prefab;


    // Start is called before the first frame updates
    void Start()
    {
        m_mesh = new Mesh();
        UpdateMesh();
    }


    void Update()
    {
        
    }


    void UpdateMesh()
    {
        //Add vertices to mesh
        m_mesh.vertices = CreateVertices(n, m);

        //Adding triangles
        m_mesh.triangles = CreateTriangles(n, m);

        //Add normals to mesh
        m_mesh.RecalculateNormals();

        //Add mesh to mesh filter
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshFilter.mesh = m_mesh;
    }


    Vector3 [] CreateVertices(int _n, int  _m)
    {
        Vector3 [] vertices = new Vector3[(_n + 1) * (_m + 1)];

        for (int r = 0; r <= _m; r++)
        {
            for (int c = 0; c <= _n; c++)
            {
                vertices[c + ((_n + 1) * r)] = new Vector3(c, r, 0);
            }
        }

        return vertices; 
    }



    int [] CreateTriangles(int _n, int _m)
    {
        int[] trianglePoints = new int[6 * _n * _m];

        int counter = 0;
        for (int r = 0; r < _m; r++)
        {
            for (int c = 0; c < _n; c++)
            {
                int P0 = c + ((_n + 1) * r);
                int P1 = (c + 1) + ((_n + 1) * r);
                int P2 = c + ((_n + 1) * (r + 1));
                int P3  = (c + 1) + ((_n + 1) * (r + 1));

                trianglePoints[(6 * counter) + 0] = P1;
                trianglePoints[(6 * counter) + 1] = P0;
                trianglePoints[(6 * counter) + 2] = P2;
                trianglePoints[(6 * counter) + 3] = P2;
                trianglePoints[(6 * counter) + 4] = P3;
                trianglePoints[(6 * counter) + 5] = P1;

                counter++;
            }
        }

        return trianglePoints;
    }





}

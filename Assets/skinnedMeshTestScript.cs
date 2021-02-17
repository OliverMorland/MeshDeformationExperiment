using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skinnedMeshTestScript : MonoBehaviour
{

    SkinnedMeshRenderer m_skinnedMeshRenderer;


    // Start is called before the first frame update
    void Start()
    {

        m_skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        ObjUtil.SaveMesh(m_skinnedMeshRenderer.sharedMesh, "Assets/SavedMesh.obj");

        Vector3[] vertices = m_skinnedMeshRenderer.sharedMesh.vertices;
        Vector3[] deltaVertices = new Vector3[vertices.Length];
        Vector3[] deltaNormals = new Vector3[vertices.Length];
        Vector3[] deltaTangents = new Vector3[vertices.Length];
        Vector2[] uvs = m_skinnedMeshRenderer.sharedMesh.uv;
        int[] tris = m_skinnedMeshRenderer.sharedMesh.triangles;

        m_skinnedMeshRenderer.sharedMesh.GetBlendShapeFrameVertices(7, 0, deltaVertices, deltaTangents, deltaNormals);

        for (int i = 0; i < vertices.Length; i++)
        {
            deltaVertices[i] = deltaVertices[i] + vertices[i];
        }

        ObjUtil.SaveMeshFromArrays("Assets/SavedMesh7.obj", deltaVertices, deltaNormals, uvs, tris);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

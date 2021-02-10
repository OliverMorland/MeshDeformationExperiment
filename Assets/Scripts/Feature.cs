using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feature : MonoBehaviour
{
    [Header("Options")]
    public List<Mesh> m_MeshOptions;
    public List<Color> m_ColorOptions;

    public void UpdateFeature(Mesh mesh)
    {
        if (GetComponent<MeshFilter>() == null)
        {
            Debug.Log("Object has no mesh filter");
        }
        else
        {
            Debug.Log("Updating Feature's mesh");
            GetComponent<MeshFilter>().mesh = mesh;
        }
        
    }


    public void UpdateFeature(Color color)
    {
        if (GetComponent<Renderer>() == null)
        {
            Debug.Log("Object has no renderer");
        }
        else
        {
            Debug.Log("Updating Feature's Color");
            GetComponent<Renderer>().material.color = color;
        }

    }
}

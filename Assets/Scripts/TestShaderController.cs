using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShaderController : MonoBehaviour
{
    Material material;
    public string propertyName;

    [Range(-0.2f, 0.1f)]public float lightness;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.SetFloat("_Lightness", lightness);


    }
}

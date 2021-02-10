using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feature : MonoBehaviour
{
    [Header("Options")]
    public List<Mesh> m_MeshOptions;
    public List<Color> m_ColorOptions;

    static string m_referenceGridDataPath = "Assets/TargetGridData/ReferenceGrid.txt";

    public void UpdateFeature(Mesh mesh)
    {
        switch (mesh.name)
        {
            case "Head_Standard":
                UpdateDeformationGrid("Assets/TargetGridData/ReferenceGrid.txt");
                break;
            case "FemaleHead_Standard":
                UpdateDeformationGrid("Assets/TargetGridData/EuroMaleToEuroFemale.txt");
                break;
            case "AsianMale_BaseHead":
                UpdateDeformationGrid("Assets/TargetGridData/EuroMaleToAsiaMale.txt");
                break;
            case "AsianFemale_BaseHead":
                UpdateDeformationGrid("Assets/TargetGridData/EuroMaleToAsiaFemale.txt");
                break;
            default:
                //UpdateDeformationGrid("Assets/TargetGridData/ReferenceGrid.txt");
                break;
        }


        if (GetComponent<MeshFilter>() == null)
        {
            Debug.LogWarning("Object has no mesh filter");
        }
        else
        {
            Debug.Log("Updating Feature's mesh");

            //Load Reference Grid
            GetComponent<Deformable>().m_ControlPointsGrid.LoadGrid(m_referenceGridDataPath);

            //Set Mesh
            GetComponent<MeshFilter>().mesh = mesh;

            //Reset uvw values
            GetComponent<Deformable>().ResetUVWs();

            //Load relevant grid
            string currentGridPath = GetComponent<Deformable>().m_ControlPointsGrid.m_LoadFromPath;
            GetComponent<Deformable>().m_ControlPointsGrid.LoadGrid(currentGridPath);
        }

    }


    public void UpdateFeature(Color color)
    {
        if (GetComponent<Renderer>() == null)
        {
            Debug.LogWarning("Object has no renderer");
        }
        else
        {
            Debug.Log("Updating Feature's Color");
            GetComponent<Renderer>().material.color = color;
        }

    }


    void UpdateDeformationGrid(string gridDataPath)
    {
        if (GetComponent<Deformable>() == null)
        {
            Debug.Log("No Deformable script attatched");
            return;
        }

        GetComponent<Deformable>().m_ControlPointsGrid.m_LoadFromPath = gridDataPath;
        GetComponent<Deformable>().m_ControlPointsGrid.LoadGrid(gridDataPath);

    }
}

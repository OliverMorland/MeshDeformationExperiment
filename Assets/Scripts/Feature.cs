using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Feature : MonoBehaviour
{

    [Header("Options")]
    public List<Mesh> m_MeshOptions;
    public List<Material> m_MaterialOptions;
    public List<Vector3> m_HLSOptions;

    [Header("Icons")]
    [Space(20)]
    public Sprite m_FeatureIcon;
    public List<Sprite> m_MeshOptionIcons;


    static string m_referenceGridDataPath = "Assets/TargetGridData/ReferenceGrid.txt";


    private void Awake()
    {
        //UpdateFeature(m_MeshOptions[0]);
    }

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
                //Do nothing
                break;
        }

        /*
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
        */
        
        if (GetComponent<SkinnedMeshRenderer>() == null)
        {
            Debug.LogError("Object has no skinned mesh renderer");
        }
        else
        {
            //Load Reference Grid
            GetComponent<Deformable>().m_ControlPointsGrid.LoadGrid(m_referenceGridDataPath);

            //Set Mesh
            GetComponent<SkinnedMeshRenderer>().sharedMesh = mesh;

            //Reset uvw values
            GetComponent<Deformable>().ResetUVWs();

            //Load relevant grid
            string currentGridPath = GetComponent<Deformable>().m_ControlPointsGrid.m_LoadFromPath;
            GetComponent<Deformable>().m_ControlPointsGrid.LoadGrid(currentGridPath);
     
        }

    }

    public void UpdateMaterial(Material selectedMaterial)
    {
        if (GetComponent<Renderer>() == null)
        {
            Debug.LogError("Object has no renderer");
        }
        else
        {
            GetComponent<Renderer>().material = selectedMaterial;
        }
    }


    public void UpdateFeature(Vector3 hls)
    {
        if (GetComponent<Renderer>() == null)
        {
            Debug.LogError("Object has no renderer");
        }
        else
        {
            string hue = "_Hue";
            string lightness = "_Lightness";
            Material featureMaterial = GetComponent<Renderer>().material; 
            if ((featureMaterial.GetFloat(hue) != null) || (featureMaterial.GetFloat(lightness) != null))
            {
                GetComponent<Renderer>().material.SetFloat(hue, hls.x);
                GetComponent<Renderer>().material.SetFloat(lightness, hls.y);
            }
        }

    }


    void UpdateDeformationGrid(string gridDataPath)
    {
        if (GetComponent<Deformable>() == null)
        {
            Debug.LogWarning("No Deformable script attatched");
            return;
        }

        GetComponent<Deformable>().m_ControlPointsGrid.m_LoadFromPath = gridDataPath;
        GetComponent<Deformable>().m_ControlPointsGrid.LoadGrid(gridDataPath);

    }

    void UpdateBones(string bonesDataPath)
    {
        Debug.Log("Updating bones");
        if (GetComponent<SkinnedMeshRenderer>() == null)
        {
            Debug.LogError("No skin mesh renderer attatched");
            return;
        }

        Transform headBone;
        Transform neckBone;
        Transform chestBone;

        //Get Bones
        Transform rootBone = GetComponent<SkinnedMeshRenderer>().rootBone;
        if (rootBone != null)
        {
            headBone = rootBone;
            if (headBone.childCount > 0)
            {
                neckBone = headBone.GetChild(0);
                if (neckBone.childCount > 0)
                {
                    chestBone = neckBone.GetChild(0);

                    //Get Data
                    if (File.Exists(bonesDataPath))
                    {
                        string jsonString = File.ReadAllText(bonesDataPath);
                        BoneData boneData = JsonUtility.FromJson<BoneData>(jsonString);
                        Debug.Log("Head bone position: " + boneData.headBonePos);

                        headBone.localPosition = boneData.headBonePos;
                        neckBone.localPosition = boneData.neckBonePos;
                        chestBone.localPosition = boneData.chestBonePos;
                    }
                    else
                    {
                        Debug.Log("File doesn't exist");
                    }

                }
            }
        }
    }
}
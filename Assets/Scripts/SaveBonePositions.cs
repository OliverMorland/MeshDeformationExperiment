using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class BoneData
{
    public Vector3 headBonePos;
    public Vector3 neckBonePos;
    public Vector3 chestBonePos;
}

public class SaveBonePositions : MonoBehaviour
{

    public string m_boneDataSavePath = "Assets/Resources/BonesData/BoneData.txt";

    SkinnedMeshRenderer m_skinnedMeshRenderer;

    [ContextMenu("Save Bone Position")]
    void saveBonePositionsAndRotations()
    {
        if (GetComponent<SkinnedMeshRenderer>())
        {
            //Find root bone
            m_skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
            Transform rootBone = m_skinnedMeshRenderer.rootBone;

            //Set positions and rotations
            BoneData boneData = new BoneData();
            boneData.headBonePos = rootBone.transform.position;

            if(rootBone.childCount > 0)
            {
                Transform neckBone = rootBone.GetChild(0);
                boneData.neckBonePos = neckBone.localPosition;
                
                if (neckBone.childCount > 0)
                {
                    Transform chestBone = neckBone.GetChild(0);
                    boneData.chestBonePos = chestBone.localPosition;
                }
            }

            string boneDataText = JsonUtility.ToJson(boneData);
            File.WriteAllText(m_boneDataSavePath, boneDataText);

        }
        else
        {
            Debug.Log("No bones detected");
        }

    }
}

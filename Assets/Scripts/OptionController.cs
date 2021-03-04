using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionController : MonoBehaviour
{
    [HideInInspector]
    public Feature m_featureToEdit;
    [HideInInspector]
    public Mesh m_meshOption;
    [HideInInspector]
    public Material m_MaterialOption;
    [HideInInspector]
    public Vector3 m_hlsOption;


    public void UpdateFeatureMesh()
    {
        if (m_meshOption == null)
        {
            Debug.Log("Option has no assigned mesh");
        }
        else
        {
            m_featureToEdit.UpdateFeature(m_meshOption);

            if (m_MaterialOption != null)
            {
                m_featureToEdit.UpdateMaterial(m_MaterialOption);
            }
        }
    }

    public void UpdateFeatureColor()
    {
        if (m_hlsOption == null)
        {
            Debug.Log("Option has no assigned color");
        }
        else
        {
            m_featureToEdit.UpdateFeature(m_hlsOption);
        }
    }
}

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
    public Color m_colorOption;

    public void UpdateFeatureMesh()
    {
        if (m_meshOption == null)
        {
            Debug.Log("Option has no assigned mesh");
        }
        else
        {
            m_featureToEdit.UpdateFeature(m_meshOption);
        }
    }


    public void UpdateFeatureColor()
    {
        if (m_colorOption == null)
        {
            Debug.Log("Option has no assigned color");
        }
        else
        {
            m_featureToEdit.UpdateFeature(m_colorOption);
        }
    }
}

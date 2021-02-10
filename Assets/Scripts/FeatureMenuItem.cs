using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureMenuItem : MonoBehaviour
{

    [Header("Feature")]
    [Tooltip("Drag feature from Avatar that you wish this button to affect.")]
    [SerializeField] Feature m_Feature;


    public void UpdateOptionsButtons()
    {
        if (m_Feature == null)
        {
            Debug.LogWarning("Button not linked to feature");
            return;
        }
        CustomizerManager.Instance.PopulateOptions(m_Feature);
    }

}

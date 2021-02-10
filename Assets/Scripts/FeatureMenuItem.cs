using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureMenuItem : MonoBehaviour
{

    [Header("Feature")]
    [Tooltip("Drag feature from Avatar that you wish this button to affect.")]
    [SerializeField] Feature m_Feature;

    [Header("Geometry Options")]
    [Space(20)]
    [SerializeField] GameObject m_GeometryOptionPrefab;
    public List<Mesh> m_GeometryOptions;
    public List<Sprite> m_GeometryIcons;

    [Header("Color Options")]
    [Space(20)]
    [SerializeField] GameObject m_ColorOptionPrefab;
    public List<Color> m_ColorOptions;

    [Header("Panels")]
    [Space(20)]
    [SerializeField] GameObject m_GeometryOptionsPanel;
    [SerializeField] GameObject m_ColorOptionsPanel;


    public void UpdateOptionsButtons()
    {
        CustomizerManager.Instance.PopulateOptions(m_Feature);
    }

}

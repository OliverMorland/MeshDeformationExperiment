using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizerManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject m_GeometryPanel;
    public GameObject m_ColorPanel;

    [Header("Prefabs")]
    public GameObject m_GeometryOptionPrefab;
    public GameObject m_ColorOptionPrefab;


    //Singleton pattern
    private static CustomizerManager m_instance;
    public static CustomizerManager Instance{
        get
        {
            return m_instance;
        }
    }

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else{
            Destroy(this.gameObject);
        }
    }

    public void PopulateOptions(Feature feature)
    {

        ClearAllOptions();

        for (int i = 0; i < feature.m_MeshOptions.Count; i++)
        {
            GameObject newGeoOption = Instantiate(m_GeometryOptionPrefab, m_GeometryPanel.transform);

            if (newGeoOption.GetComponentInChildren<Text>() == null)
            {
                Debug.Log("Geo Option prefab needs text");
            }
            else
            {
                newGeoOption.GetComponentInChildren<Text>().text = feature.m_MeshOptions[i].name;
            }

            if (newGeoOption.GetComponent<OptionController>() != null)
            {
                newGeoOption.GetComponent<OptionController>().m_featureToEdit = feature;
                newGeoOption.GetComponent<OptionController>().m_meshOption = feature.m_MeshOptions[i];
            }
        }

        for (int i = 0; i < feature.m_ColorOptions.Count; i++)
        {
            GameObject newColOption = Instantiate(m_ColorOptionPrefab, m_ColorPanel.transform);

            if (newColOption.GetComponent<Image>() == null)
            {
                Debug.Log("Color Option prefab needs an image");
            }
            else
            {
                newColOption.GetComponent<Image>().color = feature.m_ColorOptions[i];
            }

            if (newColOption.GetComponent<OptionController>() != null)
            {
                newColOption.GetComponent<OptionController>().m_featureToEdit = feature;
                newColOption.GetComponent<OptionController>().m_colorOption = feature.m_ColorOptions[i];
            }
        }
    }


    void ClearAllOptions()
    {
        for (int i = 0; i < m_GeometryPanel.transform.childCount; i++)
        {
            Destroy(m_GeometryPanel.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < m_ColorPanel.transform.childCount; i++)
        {
            Destroy(m_ColorPanel.transform.GetChild(i).gameObject);
        }
    }
}

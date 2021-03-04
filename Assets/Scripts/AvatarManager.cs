using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarManager : MonoBehaviour
{
    public Feature[] m_Features;

    private static AvatarManager m_instance;
    public static AvatarManager Instance {
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

}

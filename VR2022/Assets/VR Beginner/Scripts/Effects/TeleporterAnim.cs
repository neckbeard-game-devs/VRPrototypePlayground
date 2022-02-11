using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TeleporterAnim : MonoBehaviour
{

   float m_MaxAlphaIntensity = 2f;
   float m_MinAlphaIntensity = 0f;
   float m_CurrentTime = 0f;

    [FormerlySerializedAs("fadeSpeed")]
    [SerializeField]
    float m_FadeSpeed = 2.2f;

    bool m_Highlighted = false;

    [FormerlySerializedAs("meshRenderer")]
    [SerializeField]
    MeshRenderer m_MeshRenderer;
    MaterialPropertyBlock m_Block;

    int m_AlphaIntensityID;
    
    void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();

        m_AlphaIntensityID = Shader.PropertyToID("AlphaIntensity");

        m_Block = new MaterialPropertyBlock();
        m_Block.SetFloat(m_AlphaIntensityID, m_CurrentTime);

        m_CurrentTime = 0;

        m_MeshRenderer.SetPropertyBlock(m_Block);
    }
    
    void Update()
    {
        if (m_Highlighted)
        {
            m_CurrentTime += Time.deltaTime * m_FadeSpeed;
        }
        else if (!m_Highlighted)
        {
            m_CurrentTime -= Time.deltaTime * m_FadeSpeed;
        }

        if (m_CurrentTime > m_MaxAlphaIntensity)
            m_CurrentTime = m_MaxAlphaIntensity;
        else if (m_CurrentTime < m_MinAlphaIntensity)
            m_CurrentTime = m_MinAlphaIntensity;

        m_MeshRenderer.GetPropertyBlock(m_Block);
        m_Block.SetFloat(m_AlphaIntensityID, m_CurrentTime);
        m_MeshRenderer.SetPropertyBlock(m_Block);
    }

    public void StartHighlight()
    {
        m_Highlighted = true;
    }

    public void StopHighlight()
    {
        m_Highlighted = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WoodenLogBehaviour : MonoBehaviour
{
    [FormerlySerializedAs("meshRenderer")]
    public MeshRenderer MeshRenderer;
    
    MaterialPropertyBlock m_Block;

    float m_BurnAmount = 0.0f;
    bool m_Burn  = false;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Block = new MaterialPropertyBlock();
        m_Block.SetFloat("BurnAmount", m_BurnAmount);

        MeshRenderer.SetPropertyBlock(m_Block);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Burn && m_BurnAmount < 1.0f)
        {
            m_BurnAmount += 0.2f * Time.deltaTime;
        }

        MeshRenderer.GetPropertyBlock(m_Block);
        m_Block.SetFloat("BurnAmount", m_BurnAmount);
        MeshRenderer.SetPropertyBlock(m_Block);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ignite")
        {
            m_Burn = true;
        }       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionChange : MonoBehaviour
{
    MeshRenderer m_Renderer;
    MaterialPropertyBlock m_Block;
    int m_ColorId;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
        m_Block = new MaterialPropertyBlock();
        m_ColorId = Shader.PropertyToID("_BaseColor");
    }

    public void DialChanged(DialInteractable dial)
    {
        m_Renderer.GetPropertyBlock(m_Block);
        m_Block.SetColor(m_ColorId, Color.Lerp(Color.black, Color.white, dial.CurrentAngle/dial.RotationAngleMaximum));
        m_Renderer.SetPropertyBlock(m_Block);
    }
}

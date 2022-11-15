using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// Handle all the VFX and material effect on the cauldron
/// </summary>
[ExecuteInEditMode]
public class CauldronEffects : MonoBehaviour
{
    public float Speed = 0.0f;
    
    float m_CurrentTime = 0;
    MeshRenderer m_MeshRenderer;
    MaterialPropertyBlock m_Block;

    VisualEffect m_BubbleEffect;
    int m_SpeedModID, m_SpinDirectionID, m_SpinIntensityID, m_BubbleSpawnAmountID;

    private void Start()
    {
        m_BubbleEffect = GetComponent<VisualEffect>();
    }
    
    void OnEnable()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();

        m_SpeedModID = Shader.PropertyToID("SpeedMod");
        m_SpinDirectionID = Shader.PropertyToID("SpinDirection");
        m_SpinIntensityID = Shader.PropertyToID("SpinIntensity");
        m_BubbleSpawnAmountID = Shader.PropertyToID("BubbleSpawnAmount");

        m_Block = new MaterialPropertyBlock();
        m_Block.SetFloat(m_SpeedModID, m_CurrentTime);

        m_CurrentTime = 0;

        m_MeshRenderer.SetPropertyBlock(m_Block);
    }

    public void SetRotationSpeed(int step)
    {
        if (step == -1)
        {
            Speed = -0.4f;
            m_BubbleEffect.SetInt(m_SpinDirectionID, -1);
            m_BubbleEffect.SetFloat(m_SpinIntensityID, 0.75f);
        } else if (step == 0)
        {
            Speed = 0.0f;
            m_BubbleEffect.SetInt(m_SpinDirectionID, -1);
            m_BubbleEffect.SetFloat(m_SpinIntensityID, 0.3f);
        } else if (step == 1)
        {
            Speed = 0.4f;
            m_BubbleEffect.SetInt(m_SpinDirectionID, 1);
            m_BubbleEffect.SetFloat(m_SpinIntensityID, 0.75f);
        }

    }

    public void SetBubbleIntensity(int intensityStep)
    {
        int bubbleIntensity = intensityStep * 3;
        m_BubbleEffect.SetInt(m_BubbleSpawnAmountID, bubbleIntensity);
    }
    
    void Update()
    {
        m_CurrentTime += Time.deltaTime * Speed;

        if (m_CurrentTime > 50.0f)
            m_CurrentTime -= 50.0f;
        else if (m_CurrentTime < -50.0f)
            m_CurrentTime += 50.0f;

        m_MeshRenderer.GetPropertyBlock(m_Block);
        m_Block.SetFloat(m_SpeedModID, m_CurrentTime);
        m_MeshRenderer.SetPropertyBlock(m_Block);
    }
}

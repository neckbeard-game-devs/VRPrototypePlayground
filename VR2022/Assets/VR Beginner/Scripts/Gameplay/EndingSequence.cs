using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class EndingSequence : MonoBehaviour
{
    [FormerlySerializedAs("meshRenderer")]
    public MeshRenderer MeshRenderer;
    
    MaterialPropertyBlock m_Block;
    Color m_ColorSphere;

    // Start is called before the first frame update
    void Start()
    {
        m_ColorSphere = new Color(1,1,1,0);
        m_Block = new MaterialPropertyBlock();
        m_Block.SetColor("_BaseColor", m_ColorSphere);

        MeshRenderer.SetPropertyBlock(m_Block);

    }

    public void StartSequence()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float alpha = m_ColorSphere.a;

        const float length = 4.0f;
        for (float t = 0.0f; t < length; t += Time.deltaTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 1, t / length));

            MeshRenderer.GetPropertyBlock(m_Block);
            m_Block.SetColor("_BaseColor", newColor);
            MeshRenderer.SetPropertyBlock(m_Block);

            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

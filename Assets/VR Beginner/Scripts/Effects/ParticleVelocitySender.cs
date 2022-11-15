using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[ExecuteInEditMode]
public class ParticleVelocitySender : MonoBehaviour
{
    public float MaxVelocity = 5.0f;
    
    VisualEffect m_VisualEffect;

    Vector3 m_PrevPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        m_VisualEffect = GetComponent<VisualEffect>();

        m_PrevPosition = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = transform.position;

        float dist = Vector3.Distance(newPos, m_PrevPosition);
        float velocity = dist / Time.deltaTime;

        float ratio = Mathf.Clamp01(velocity / 5.0f);

        m_VisualEffect.SetFloat("SparkleSpawnRamp", ratio);
        m_PrevPosition = newPos;
    }
}

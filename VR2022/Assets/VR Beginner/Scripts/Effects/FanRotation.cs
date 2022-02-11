using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotation : MonoBehaviour
{
    public float Acceleration = 45.0f;
    public float Deceleration = 20.0f;
    public float MaxSpeed = 360.0f;
    
    float m_CurrentSpeed;
    bool m_On;

    // Update is called once per frame
    void Update()
    {
        if (m_On)
        {
            m_CurrentSpeed = Mathf.Min(m_CurrentSpeed + Acceleration * Time.deltaTime, MaxSpeed);
        }
        else
        {
            m_CurrentSpeed = Mathf.Max(m_CurrentSpeed - Deceleration * Time.deltaTime, 0.0f);
        }

        if (m_CurrentSpeed > 0.001f)
        {
            transform.Rotate(new Vector3(0, 0, m_CurrentSpeed * Time.deltaTime));
        }
    }

    public void SwitchOn()
    {
        m_On = true;
    }

    public void SwitchOff()
    {
        m_On = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    LineRenderer m_LineRenderer;
    
    void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        RaycastHit info;
        if (Physics.Raycast(transform.position, transform.up, out info, 100.0f, ~0, QueryTriggerInteraction.Ignore))
        {
            m_LineRenderer.SetPosition(1, new Vector3(0, info.distance, 0));
        }
        else
        {
            m_LineRenderer.SetPosition(1, new Vector3(0, 100.0f, 0));
        }
    }
}

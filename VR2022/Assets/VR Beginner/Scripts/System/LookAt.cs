using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private Camera m_Camera;

    private void Start()
    {
        m_Camera = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, m_Camera.transform.position.z);
        transform.LookAt(newPosition);
    }
}

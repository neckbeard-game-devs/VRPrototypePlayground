using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This will save the starting position/rotation of an object, and will move it back there when Respawn is called.
/// Useful for object that should never get destroyed when thrown in fire or the cauldron, like the ingredients.
/// </summary>
public class RespawnableObject : MonoBehaviour
{
    Vector3 m_StartingPosition;
    Quaternion m_StartingRotation;

    Rigidbody m_Rigidbody;
    
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        if (m_Rigidbody == null)
        {
            m_StartingPosition = transform.position;
            m_StartingRotation = transform.rotation;
        }
        else
        {
            m_StartingPosition = m_Rigidbody.position;
            m_StartingRotation = m_Rigidbody.rotation;
        }
    }

    public void Respawn()
    {
        if (m_Rigidbody == null)
        {
            transform.position = m_StartingPosition;
            transform.rotation = m_StartingRotation;
        }
        else
        {
            m_Rigidbody.velocity = Vector3.zero;
            m_Rigidbody.angularVelocity = Vector3.zero;
            m_Rigidbody.position = m_StartingPosition;
            m_Rigidbody.rotation = m_StartingRotation;
        }
    }
}

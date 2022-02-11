using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCenterOfMass : MonoBehaviour
{
    public Vector3 LocalCenterOfMass;
    public Rigidbody TargetRigidbody;

    void Awake()
    {
        if (TargetRigidbody != null)
        {
            TargetRigidbody.centerOfMass = LocalCenterOfMass;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (TargetRigidbody != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(TargetRigidbody.transform.TransformPoint(LocalCenterOfMass), 0.01f);
        }
    }
}

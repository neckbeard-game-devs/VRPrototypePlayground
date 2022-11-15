using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// Will spawn the given prefab, either when the Spawn function is called or if a MinInstance is setup, until that
/// amount exist in the scene. If Spawn is called when there is already MaxInstance number of object in the scene,
/// it will destroy the oldest one to make room for the newest.
/// </summary>
public class ObjectSpawner : MonoBehaviour
{
    public GameObject Prefab;
    public VisualEffect SpawnEffect;
    public Transform SpawnPoint;
    public int MaxInstances = 2;
    public int MinInstance = 0;
    
    List<GameObject> m_Instances = new List<GameObject>();

    private void Start()
    {
        Prefab = null;
    }

    public void Spawn()
    {
        var newInst = Instantiate(Prefab, SpawnPoint.position, SpawnPoint.rotation);

        if (m_Instances.Count >= MaxInstances)
        {
            Destroy(m_Instances[0]);
            m_Instances.RemoveAt(0);
        }
        
        SpawnEffect.SendEvent("SingleBurst");
        m_Instances.Add(newInst);
    }

    void Update()
    {
        for (int i = 0; i < m_Instances.Count; ++i)
        {
            //if the object was destroyed, remove from the list
            if (m_Instances[i] == null)
            {
                m_Instances.RemoveAt(i);
                i--;
            }
        }

        if (Prefab != null)
        {
            while (m_Instances.Count < MinInstance)
            {
                Spawn();
            }
        }
    }
}

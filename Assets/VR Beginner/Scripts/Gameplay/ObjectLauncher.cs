using System.Collections.Generic;
using UnityEngine;

public class ObjectLauncher : MonoBehaviour
{
    public Transform SpawnPoint;
    public ProjectileBase ObjectToSpawn;
    public bool IsAutoSpawn = false;
    public float LaunchRate = 0.5f;

    public AudioClip LaunchingClip;
    
    float m_LastLaunch = 0.0f;

    Queue<ProjectileBase> m_ProjectilesPool = new Queue<ProjectileBase>();

    void Awake()
    {
        enabled = false;

        for (int i = 0; i < 32; ++i)
        {
            var newObj = Instantiate(ObjectToSpawn, SpawnPoint.position, SpawnPoint.rotation);
            newObj.gameObject.SetActive(false);
            m_ProjectilesPool.Enqueue(newObj);
        }
    }

    public void Activated()
    {
        //if this is auto spawn regularly, we enable the script so the update is called.
        if (IsAutoSpawn)
        {
            enabled = true;
            m_LastLaunch = LaunchRate;
        }

        Launch();
    }

    public void Deactivated()
    {
        enabled = false;
    }

    void Update()
    {
        if (m_LastLaunch > 0.0f)
        {
            m_LastLaunch -= Time.deltaTime;

            if (m_LastLaunch <= 0.0f)
            {
                Launch();
                m_LastLaunch = LaunchRate;
            }
        }
    }

    void Launch()
    {
        var p = m_ProjectilesPool.Dequeue();
        p.gameObject.SetActive(true);
        p.transform.position = SpawnPoint.position;
        p.Launched(SpawnPoint.transform.forward, this);
        
        SFXPlayer.Instance.PlaySFX(LaunchingClip, SpawnPoint.position, new SFXPlayer.PlayParameters()
        {
            Pitch = Random.Range(0.9f, 1.2f),
            Volume = 1.0f,
            SourceID = -999
        });
    }
    
    public void ReturnProjectile(ProjectileBase proj)
    {
        m_ProjectilesPool.Enqueue(proj);
    }
}

public abstract class ProjectileBase : MonoBehaviour
{
    public abstract void Launched(Vector3 direction, ObjectLauncher launcher);
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallProjectile : ProjectileBase
{
    Rigidbody m_Rigidbody;
    float m_LaunchTime = 0;
    bool m_Launched = false;

    ObjectLauncher m_Launcher;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.useGravity = false;
    }

    void Update()
    {
        if (m_Launched)
        {
            m_LaunchTime += Time.deltaTime;

            if (m_LaunchTime > 10.0f)
            {
                //trigger a collision if we reached 10s without one, to force recycle the projectile, it's now too far
                OnCollisionEnter(null);
            }
        }
    }

    public override void Launched(Vector3 direction, ObjectLauncher launcher)
    {
        //as they are pooled, they could have been already used and have previous velocity
        m_Rigidbody.velocity = Vector3.zero;

        m_Rigidbody.AddForce(direction * 200.0f);
        m_Launcher = launcher;

        m_Launched = true;
        m_LaunchTime = 0.0f;
    }

    void OnCollisionEnter(Collision other)
    {
        gameObject.SetActive(false);
        m_Launcher.ReturnProjectile(this);

        m_Launched = false;
    }
}

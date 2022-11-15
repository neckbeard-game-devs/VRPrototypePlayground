using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Will trigger SFX through the SFXPlayer when the object on which this is added trigger a collision enter event
/// </summary>
public class ContactSoundPlayer : MonoBehaviour
{
    static int s_IDMax = 0;

    public bool CloseCaptioned = false;
    public AudioClip[] Clips;

    int m_ID;

    void Awake()
    {
        m_ID = s_IDMax;
        s_IDMax++;
    }

    void OnCollisionEnter(Collision other)
    {
        //avoid playing hit sound when all physic object settle at the load of the level.
        if (Time.timeSinceLevelLoad < 1.0f)
            return;
        
        AudioClip randomClip = Clips[Random.Range(0, Clips.Length)];
        
        SFXPlayer.Instance.PlaySFX(randomClip, other.contacts[0].point, new SFXPlayer.PlayParameters()
        {
            Volume = 1.0f,
            Pitch = Random.Range(0.8f, 1.2f),
            SourceID = m_ID
        }, 0.5f, CloseCaptioned);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

/// <summary>
/// Central entry point to play single use SFX (e.g. contact sound). Through a single function call, PlaySFX, allow to
/// play a given clip at a given point with the given parameters.
/// The system also support using ID for event, which allow to avoid 2 event with the same ID to play too soon (e.g.
/// contact sound would overlapping when the object collide multiple time in a second)
/// </summary>
public class SFXPlayer : MonoBehaviour
{
    public struct PlayParameters
    {
        public float Pitch;
        public float Volume;
        public int SourceID;
    }

    public class PlayEvent
    {
        public float Time;
    }
    
    static SFXPlayer s_Instance;
    public static SFXPlayer Instance => s_Instance;
    
    public AudioSource SFXReferenceSource;
    public int SFXSourceCount;

    Dictionary<int, PlayEvent> m_PlayEvents = new Dictionary<int, PlayEvent>();
    List<int> m_PlayingSources = new List<int>();
    
    AudioSource[] m_SFXSourcePool;

    
    int m_UsedSource = 0;
    
    void Awake()
    {
        if (s_Instance != null)
        {
            Destroy(this);
            return;
        }
        
        s_Instance = this;

        m_SFXSourcePool = new AudioSource[SFXSourceCount];


        for (int i = 0; i < SFXSourceCount; ++i)
        {
            m_SFXSourcePool[i] = Instantiate(SFXReferenceSource);
            m_SFXSourcePool[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        List<int> IDToRemove = new List<int>();
        foreach (var playEvent in m_PlayEvents)
        {
            playEvent.Value.Time -= Time.deltaTime;
            
            if(playEvent.Value.Time <= 0.0f)
                IDToRemove.Add(playEvent.Key);
        }

        foreach (var id in IDToRemove)
        {
            m_PlayEvents.Remove(id);
        }

        for (int i = 0; i < m_PlayingSources.Count; ++i)
        {
            int id = m_PlayingSources[i];
            if (!m_SFXSourcePool[id].isPlaying)
            {
                m_SFXSourcePool[id].gameObject.SetActive(false);
            }
            
            m_PlayingSources.RemoveAt(i);
            i--;
        }
    }
    
    //This will return a new source based on the SFX Reference Source, useful for script that want to control their own
    //source but still keep all the settings from the reference (e.g. the mixer)
    //NOTE : caller need to clean that source as the SFXPlayer does not keep any track of it
    public AudioSource GetNewSource()
    {
        return Instantiate(SFXReferenceSource);
    }

    /// <summary>
    /// Will play the given clip at the given place. The Parameter contain a SourceID that allow to uniquely identify
    /// an event so it have to wait the given cooldown time before being able to be played again (e.g. useful for
    /// collision sound, as physic system could create multiple collisions in a short time that would lead to stutter)
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="position"></param>
    /// <param name="parameters"></param>
    /// <param name="cooldownTime">Time before another PlaySFX with the same parameters.SourceID can be played again</param>
    /// <param name="isClosedCaptioned">if true, the source will display a closed caption for the given clip if the closed caption system is enabled</param>
    public void PlaySFX(AudioClip clip, Vector3 position, PlayParameters parameters, float cooldownTime = 0.5f, bool isClosedCaptioned = false)
    {
        if(clip == null)
            return;
        
        //can't play this sound again as the previous one with the same source was too early
        if (m_PlayEvents.ContainsKey(parameters.SourceID))
            return;
        
        AudioSource s = m_SFXSourcePool[m_UsedSource];

        
        m_PlayingSources.Add(m_UsedSource);
        
        m_UsedSource = m_UsedSource + 1;
        if (m_UsedSource >= m_SFXSourcePool.Length) m_UsedSource = 0;

        s.gameObject.SetActive(true);
        s.transform.position = position;
        s.clip = clip;

        s.volume = parameters.Volume;
        s.pitch = parameters.Pitch;
        
        m_PlayEvents.Add(parameters.SourceID, new PlayEvent() { Time = cooldownTime });

        
        s.Play();
    }
}

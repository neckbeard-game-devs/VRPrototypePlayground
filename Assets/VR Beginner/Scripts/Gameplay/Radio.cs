using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public AudioSource MusicSource;
    public AudioSource NoiseSource;

    public AudioClip[] MusicClips;

    float m_TuningRatio = 0.0f;
    float m_VolumeRatio = 0.0f;
    
    void Start()
    {
        Tune();
    }

    public void VolumeChanged(DialInteractable dial)
    {
        float ratio = dial.CurrentAngle / dial.RotationAngleMaximum;

        m_VolumeRatio = ratio;
       
        Tune();
    }

    public void TuningChanged(DialInteractable dial)
    {
        //off
        if (dial.CurrentAngle < 0.01f)
        {
            MusicSource.Stop();
            NoiseSource.Stop();
        }
        else if(!MusicSource.isPlaying)
        {
            MusicSource.Play();
            NoiseSource.Play();
        }
        
        if(MusicClips.Length == 0)
            return;

        float ratio = dial.CurrentAngle / dial.RotationAngleMaximum;
        float stepSize = dial.RotationAngleMaximum / MusicClips.Length;

        float stepRatio = dial.CurrentAngle / stepSize;
        int closest = Mathf.RoundToInt(stepRatio);

        float dist = Mathf.Abs(closest - stepRatio) / 0.5f;

        if (closest == 0)
            dist = 1.0f;
        else
        {
            AudioClip c = MusicClips[closest - 1];

            if (c != MusicSource.clip)
            {
                int sample = MusicSource.timeSamples;
                MusicSource.clip = c;
                MusicSource.timeSamples = sample;
            }
        }

        m_TuningRatio = 1.0f - dist;

        Tune();
    }

    void Tune()
    {
        MusicSource.volume = m_TuningRatio * m_VolumeRatio;
        NoiseSource.volume = (1.0f - m_TuningRatio) * m_VolumeRatio;
    }
}

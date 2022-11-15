using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Hourglass : MonoBehaviour
{
    [FormerlySerializedAs("hourglassTop")]
    public GameObject HourglassTop;
    [FormerlySerializedAs("hourglassBottom")]
    public GameObject HourglassBottom;
    [FormerlySerializedAs("timerFill")]
    public float TimerFill = 1.0f;
    [FormerlySerializedAs("particleSand")]
    public ParticleSystem ParticleSand;

    Material m_TopMat;
    Material m_BotMat;

    // Start is called before the first frame update
    void Start()
    {
        m_TopMat = HourglassTop.GetComponent<MeshRenderer>().material;
        m_BotMat = HourglassBottom.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Dot(transform.up, Vector3.down) > 0.8 && TimerFill > 0)
        {
            TimerFill -= 0.1f * Time.deltaTime;
            ParticleSand.Play();
        }
        else if (Vector3.Dot(transform.up, Vector3.down) < -0.8 && TimerFill < 1)
        {
            TimerFill += 0.1f * Time.deltaTime;
            ParticleSand.Play();
        } else
        {
            ParticleSand.Stop();
        }

        m_TopMat.SetFloat("TimerFill", TimerFill);
        m_BotMat.SetFloat("TimerFill", TimerFill);
    }
}

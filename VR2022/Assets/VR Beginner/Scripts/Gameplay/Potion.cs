using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class Potion : MonoBehaviour
{
    static int NextFreeUniqueId = 3000;
    
    public string PotionType = "Default";
    public GameObject plugObj;
    public ParticleSystem particleSystemLiquid;
    public ParticleSystem particleSystemSplash;
    public float fillAmount = 0.8f;
    public GameObject popVFX;
    [FormerlySerializedAs("meshRenderer")]
    public MeshRenderer MeshRenderer;
    [FormerlySerializedAs("smashedObject")]
    public GameObject SmashedObject;
    
    [Header("Audio")]
    public AudioClip PouringClip;
    public AudioClip[] PoppingPlugAudioClip;
    public AudioClip[] BreakingAudioClips;
    
    bool m_PlugIn = true;
    Rigidbody m_PlugRb;
    MaterialPropertyBlock m_MaterialPropertyBlock;
    Rigidbody m_RbPotion;

    int m_UniqueId;

    AudioSource m_AudioSource;
    bool m_Breakable;
    float m_StartingFillAmount;

    void OnEnable()
    {
        particleSystemLiquid.Stop();
        
        if(particleSystemSplash)
            particleSystemSplash.Stop();
        
        m_MaterialPropertyBlock = new MaterialPropertyBlock();
        m_MaterialPropertyBlock.SetFloat("LiquidFill", fillAmount);

        MeshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
        m_PlugRb = plugObj.GetComponent<Rigidbody>();
        popVFX.SetActive(false);

        m_RbPotion = GetComponent<Rigidbody>();

        m_StartingFillAmount = fillAmount;

        m_Breakable = true;
    }

    void Start()
    {
        m_AudioSource = SFXPlayer.Instance.GetNewSource();
        m_AudioSource.gameObject.transform.SetParent(particleSystemLiquid.transform, false);
        m_AudioSource.gameObject.SetActive(true);

        m_AudioSource.clip = PouringClip;
        m_AudioSource.maxDistance = 2.0f;
        m_AudioSource.minDistance = 0.2f;
        m_AudioSource.loop = true;

        m_UniqueId = NextFreeUniqueId++;
    }

    void OnDestroy()
    {
        Destroy(m_AudioSource.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Dot(transform.up, Vector3.down) > 0 && fillAmount > 0 && m_PlugIn == false)
        {
            if (particleSystemLiquid.isStopped)
            {
                particleSystemLiquid.Play();
                m_AudioSource.Play();
            }
            
            fillAmount -= 0.1f * Time.deltaTime;

            float fillRatio = fillAmount / m_StartingFillAmount;

            m_AudioSource.pitch = Mathf.Lerp(1.0f, 1.4f, 1.0f - fillRatio);

            RaycastHit hit;
            if (Physics.Raycast(particleSystemLiquid.transform.position, Vector3.down, out hit, 50.0f, ~0, QueryTriggerInteraction.Collide))
            {
                PotionReceiver receiver = hit.collider.GetComponent<PotionReceiver>();

                if (receiver != null)
                {
                    receiver.ReceivePotion(PotionType);
                }
            }

        } 
        else
        {
            particleSystemLiquid.Stop();
            m_AudioSource.Stop();
        }

        MeshRenderer.GetPropertyBlock(m_MaterialPropertyBlock);
        m_MaterialPropertyBlock.SetFloat("LiquidFill", fillAmount);
        MeshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
    }

    public void ToggleBreakable(bool breakable)
    {
        m_Breakable = breakable;
    }

    public void PlugOff()
    {
        if (m_PlugIn)
        {
            m_PlugIn = false;
            m_PlugRb.transform.SetParent(null);
            m_PlugRb.isKinematic = false;
            m_PlugRb.AddRelativeForce(new Vector3(0, 0, 120));
            popVFX.SetActive(true);

            m_PlugIn = false;

            plugObj.transform.parent = null;

            SFXPlayer.Instance.PlaySFX(PoppingPlugAudioClip[Random.Range(0, PoppingPlugAudioClip.Length)], m_PlugRb.transform.position, new SFXPlayer.PlayParameters()
            {
                Pitch = Random.Range(0.9f, 1.1f),
                Volume = 1.0f,
                SourceID = -99
            });
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (m_Breakable && m_RbPotion.velocity.magnitude > 1.35)
        {

            if (m_PlugIn)
            {
                m_PlugRb.isKinematic = false;
                plugObj.transform.parent = null;

                Collider c;
                if (plugObj.TryGetComponent(out c))
                    c.enabled = true;
                
                Destroy(plugObj, 4.0f);
            }

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }

            if (particleSystemSplash != null)
            {       
                particleSystemSplash.gameObject.SetActive(true);
                if (fillAmount > 0)
                {
                    particleSystemSplash.Play();
                }
            }

            SmashedObject.SetActive(true);

            SFXPlayer.Instance.PlaySFX(BreakingAudioClips[Random.Range(0, BreakingAudioClips.Length)], transform.position, new SFXPlayer.PlayParameters()
            {
                Pitch = Random.Range(0.8f, 1.2f),
                SourceID = m_UniqueId,
                Volume = 1.0f
            });

            Rigidbody[] rbs = SmashedObject.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in rbs)
            {
                rb.AddExplosionForce(100.0f, SmashedObject.transform.position, 2.0f, 15.0F);
            }

            Destroy(SmashedObject, 4.0f);
            Destroy(this);
        }      
    }
}

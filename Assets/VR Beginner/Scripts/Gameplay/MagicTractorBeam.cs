using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
/// <summary>
/// Utility script that can pull toward the hand/controller the currently selected object through a Raycast.
/// </summary>
public class MagicTractorBeam : MonoBehaviour
{
    public LineRenderer BeamRenderer;
    public float TargetDistance = 0.05f;
    public Vector3 LocalRayAxis = new Vector3(1,0,0);
    public bool IsTracting => m_TractingObject;
    public bool IsActive, mainHand;
    public XRDirectInteractor m_DirectInteractor;
    
    Rigidbody m_HighlightedRigidbody;
    SelectionOutline m_CurrentSelectionOutline = null;
    bool m_TractingObject;
    RaycastHit[] m_HitCache = new RaycastHit[16];
    public bool highlightBool;
    public XRGrabInteractable closestGrababble = null;
    private UiAudioControl uac;

   
    void Update()
    {
        if (m_TractingObject)
        {
            Tracting();
        }   
        else if(IsActive)
        {
            Vector3 worldAxis = m_DirectInteractor.transform.TransformDirection(LocalRayAxis);
            m_HighlightedRigidbody = null;
            int mask = ~0;
            mask &= ~(1 << LayerMask.NameToLayer("Hands"));
            int count = Physics.SphereCastNonAlloc(m_DirectInteractor.transform.position, 0.2f, worldAxis, m_HitCache, 15.0f, mask, QueryTriggerInteraction.Ignore);

            if (count != 0)
            {
                closestGrababble = null;

                for (int i = 0; i < count; ++i)
                {
                    if (m_HitCache[i].rigidbody == null)
                        continue;
                    if (BeamRenderer.enabled)
                    {
                        BeamRenderer.SetPosition(1, LocalRayAxis * m_HitCache[i].distance);
                    }
                    var grabbable = m_HitCache[i].rigidbody.GetComponent<XRGrabInteractable>();
                    if (grabbable != null)
                    {
                        closestGrababble = grabbable;
                    }
                }

                if (closestGrababble != null)
                {
                    var filter = closestGrababble.GetComponentInChildren<MeshFilter>();
                    if (filter != null)
                    {
                        m_HighlightedRigidbody = closestGrababble.GetComponent<Rigidbody>();
                        var outline = m_HighlightedRigidbody.GetComponentInChildren<SelectionOutline>();
                        
                        if (outline != null && !highlightBool)
                        {
                            highlightBool = true;
                            m_CurrentSelectionOutline = outline;
                            m_CurrentSelectionOutline.Highlight();
                            BeamRenderer.enabled = true;
                            uac.uiAudioSources[0].Play();
                        }
                        else if (outline != m_CurrentSelectionOutline)
                        {
                            m_CurrentSelectionOutline.RemoveHighlight();
                            highlightBool = false;
                        }
                    }
                }
                else
                {
                    if(highlightBool)
                    {
                        highlightBool = false;
                        m_CurrentSelectionOutline.RemoveHighlight();
                        BeamRenderer.enabled = false;
                        uac.uiAudioSources[0].Play();
                    }
                   
                }
            }
        }
    }

    private void Start()
    {
        uac = FindObjectOfType<UiAudioControl>();
    }
    void Tracting()
    {
        Vector3 target = m_DirectInteractor.transform.position - m_DirectInteractor.transform.right * TargetDistance;
        Vector3 toTarget = target - m_HighlightedRigidbody.transform.position;
            
        if(toTarget.sqrMagnitude > 1.0f)
            toTarget.Normalize();
            
        m_HighlightedRigidbody.velocity = Vector3.zero;
        m_HighlightedRigidbody.AddForce(toTarget * 2.0f, ForceMode.VelocityChange);

        float dist = Vector3.Distance(m_HighlightedRigidbody.gameObject.transform.position, transform.position);
        BeamRenderer.SetPosition(1, LocalRayAxis * dist);
    }
    public void StartTracting()
    {
        if (m_HighlightedRigidbody != null && m_DirectInteractor)
        {
            if (m_HighlightedRigidbody.TryGetComponent(out StationaryOnPull stationary))
            {
                m_HighlightedRigidbody.isKinematic = false;                
            }
           
            if (!m_TractingObject)
            {
                m_TractingObject = true;
                uac.uiAudioSources[5].Play();
            }
            
        }

    }
    public void StopTracting()
    {
        if (m_HighlightedRigidbody != null)
        {
            if (!m_HighlightedRigidbody.isKinematic && (m_HighlightedRigidbody.TryGetComponent(out StationaryOnPull stationary)))
            {
                m_HighlightedRigidbody.isKinematic = true;
            }

            m_TractingObject = false;
            m_HighlightedRigidbody.velocity = Vector3.zero;
            m_HighlightedRigidbody = null;
            uac.uiAudioSources[5].Stop();
        }
        
    }

    public void EnableBeam()
    {
        Debug.Log("Enable Beam");
        if (mainHand)
        {
            uac.uiAudioSources[0].Play();
        }
        
        if (!IsActive)
        {             
            IsActive = true;
        }
        else
        {
            IsActive = false;
            BeamRenderer.enabled = false;
        }
    }
}

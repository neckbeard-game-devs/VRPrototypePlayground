using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Utility script that can pull toward the hand/controller the currently selected object through a Raycast.
/// </summary>
public class MagicTractorBeam : MonoBehaviour
{
    public LineRenderer BeamRenderer;
    public float TargetDistance = 0.05f;
    public Vector3 LocalRayAxis = new Vector3(1,0,0);

    public bool IsEnabled => BeamRenderer.gameObject.activeSelf;
    public bool IsTracting => m_TractingObject;
    
    XRDirectInteractor m_DirectInteractor;
    
    Rigidbody m_HighlightedRigidbody;
    SelectionOutline m_CurrentSelectionOutline = null;
    bool m_TractingObject;
    RaycastHit[] m_HitCache = new RaycastHit[16]; 
    
    void Start()
    {
        m_DirectInteractor = GetComponentInChildren<XRDirectInteractor>();
    }
    
    void Update()
    {
        if(m_CurrentSelectionOutline != null)
            m_CurrentSelectionOutline.RemoveHighlight();
        
        if(m_TractingObject)
            Tracting();
        else if (IsEnabled)
        {
            Vector3 worldAxis = m_DirectInteractor.transform.TransformDirection(LocalRayAxis);
            m_HighlightedRigidbody = null;
            int mask = ~0;
            mask &= ~(1 << LayerMask.NameToLayer("Hands"));
            int count = Physics.SphereCastNonAlloc(m_DirectInteractor.transform.position, 0.2f, worldAxis, m_HitCache, 15.0f, mask, QueryTriggerInteraction.Ignore);

            if (count != 0)
            {
                float closestDistance = float.MaxValue;
                float closestDistanceGrabbable = float.MaxValue;

                XRGrabInteractable closestGrababble = null;

                for (int i = 0; i < count; ++i)
                {
                    if (closestDistance > m_HitCache[i].distance)
                        closestDistance = m_HitCache[i].distance;

                    if (m_HitCache[i].rigidbody == null)
                        continue;

                    var grabbable = m_HitCache[i].rigidbody.GetComponentInChildren<XRGrabInteractable>();

                    if (grabbable != null && m_HitCache[i].distance < closestDistanceGrabbable)
                    {
                        closestDistanceGrabbable = m_HitCache[i].distance;
                        closestGrababble = grabbable;
                    }
                }

                BeamRenderer.SetPosition(1, LocalRayAxis * closestDistance);

                if (closestGrababble != null)
                {
                    var filter = closestGrababble.GetComponentInChildren<MeshFilter>();
                    if (filter != null)
                    {
                        m_HighlightedRigidbody = closestGrababble.GetComponent<Rigidbody>();
                        var outline = m_HighlightedRigidbody.GetComponentInChildren<SelectionOutline>();

                        if (outline != null)
                        {
                            m_CurrentSelectionOutline = outline;
                            m_CurrentSelectionOutline.Highlight();
                        }
                    }
                }
            }
        }
    }

    void Tracting()
    {
        Vector3 target = m_DirectInteractor.transform.position - m_DirectInteractor.transform.right * TargetDistance;
        Vector3 toTarget = target - m_HighlightedRigidbody.transform.position;
            
        if(toTarget.sqrMagnitude > 1.0f)
            toTarget.Normalize();
            
        m_HighlightedRigidbody.velocity = Vector3.zero;
        m_HighlightedRigidbody.AddForce(toTarget * 2.0f, ForceMode.VelocityChange);
    }

    public void StartTracting()
    {
        if (m_HighlightedRigidbody != null && m_DirectInteractor)
        {
            if (m_HighlightedRigidbody.TryGetComponent(out StationaryOnPull stationary))
            {
                m_HighlightedRigidbody.isKinematic = false;                
            }
            m_TractingObject = true;
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
        }
        
    }


    public void EnableBeam()
    {
        //can't enable beam if we have an interactor currently holding something
        if(m_DirectInteractor != null && m_DirectInteractor.isSelectActive)
            return;
        
        BeamRenderer.gameObject.SetActive(true);
    }

    public void DisableBeam()
    {
        BeamRenderer.gameObject.SetActive(false);
    }
}

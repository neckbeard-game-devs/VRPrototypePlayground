using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Small modification of the classic XRGrabInteractable that will keep the position and rotation offset between the
/// grabbed object and the controller instead of snapping the object to the controller. Better for UX and the illusion
/// of holding the thing (see Tomato Presence : https://owlchemylabs.com/tomatopresence/)
/// </summary>
public class XROffsetGrabbable : XRGrabInteractable
{
    class SavedTransform
    {
        public Vector3 OriginalPosition;
        public Quaternion OriginalRotation;
    }
    
    
    Dictionary<IXRInteractor, SavedTransform> m_SavedTransforms = new Dictionary<IXRInteractor, SavedTransform>();

    Rigidbody m_Rb;

    protected override void Awake()
    {
        base.Awake();

        //the base class already grab it but don't expose it so have to grab it again
        m_Rb = GetComponent<Rigidbody>();
    }
    
    protected override void OnSelectEntered(SelectEnterEventArgs evt)
    {
        var interactor = evt.interactorObject;
        if (interactor is IXRSelectInteractor)
        {
            SavedTransform savedTransform = new SavedTransform();
            
            savedTransform.OriginalPosition = interactor.transform.localPosition;
            savedTransform.OriginalRotation = interactor.transform.localRotation;

            m_SavedTransforms[interactor] = savedTransform;
            
            
            bool haveAttach = attachTransform != null;

            interactor.transform.position = haveAttach ? attachTransform.position : m_Rb.worldCenterOfMass;
            interactor.transform.rotation = haveAttach ? attachTransform.rotation : m_Rb.rotation;
        }

        base.OnSelectEntered(evt);
        Debug.Log("Grabbed" + gameObject.name);
    }

    protected override void OnSelectExited(SelectExitEventArgs evt)
    {
        var interactor = evt.interactorObject;
        if (interactor is IXRSelectInteractor)
        {
            SavedTransform savedTransform = null;
            if (m_SavedTransforms.TryGetValue(interactor, out savedTransform))
            {
                interactor.transform.localPosition = savedTransform.OriginalPosition;
                interactor.transform.localRotation = savedTransform.OriginalRotation;

                m_SavedTransforms.Remove(interactor);
            }
        }
        
        base.OnSelectExited(evt);
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        int interactorLayerMask = 1 << interactor.interactionLayers;
        return base.IsSelectableBy(interactor) && (interactionLayers.value & interactorLayerMask) != 0;
    }
}

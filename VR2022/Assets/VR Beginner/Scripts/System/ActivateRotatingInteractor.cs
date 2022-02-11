using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(IXRSelectInteractor))]
public class ActivateRotatingInteractor : MonoBehaviour
{
    public DialInteractable DialToActivate;

    IXRSelectInteractor m_Interactor;
    void Start()
    {
        m_Interactor = GetComponent<IXRSelectInteractor>();
        m_Interactor.selectEntered.AddListener(Activated);
    }

    void Activated(SelectEnterEventArgs args)
    {
        var interactable = args.interactorObject;
        
        DialToActivate.RotatingRigidbody = interactable.transform.GetComponentInChildren<Rigidbody>();
        DialToActivate.gameObject.SetActive(true);

        interactable.transform.gameObject.layer = 0;
    }
}

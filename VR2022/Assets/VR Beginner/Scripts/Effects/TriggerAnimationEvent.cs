using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(IXRSelectInteractor))]
public class TriggerAnimationEvent : MonoBehaviour
{
    public string TriggerName;

    int m_TriggerID;
    
    void Start()
    {
        m_TriggerID = Animator.StringToHash(TriggerName);
        var interactor = GetComponent<IXRSelectInteractor>();
        interactor.selectEntered.AddListener(TriggerAnim);
    }

    public void TriggerAnim(SelectEnterEventArgs args)
    {
        var interactable = args.interactorObject;
        var animator = interactable.transform.GetComponentInChildren<Animator>();

        if (animator != null)
        {
            animator.SetTrigger(TriggerName);
        }

        interactable.transform.gameObject.layer &= ~(1<<LayerMask.NameToLayer("Hands"));
    }
}

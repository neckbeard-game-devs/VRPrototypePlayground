using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRBaseInteractor))]
public class InteractorEventDispatcher : MonoBehaviour
{
    public SelectEnterEvent OnSelectedEnter;

    void Awake()
    {
        var interactor = GetComponent<XRBaseInteractor>();
        interactor.selectEntered.AddListener(evt => { OnSelectedEnter.Invoke(evt); } );
    }
}

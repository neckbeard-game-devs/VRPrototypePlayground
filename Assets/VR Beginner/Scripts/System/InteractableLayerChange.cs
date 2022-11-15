using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractableLayerChange : MonoBehaviour
{
    public IXRSelectInteractor TargetInteractable;
    public LayerMask NewLayerMask;

    public void ChangeLayerDynamic(IXRSelectInteractor interactable)
    {
        interactable.transform.gameObject.layer = NewLayerMask;
    }

    public void ChangeLayer()
    {
        //TargetInteractable.transform.gameObject.layer = NewLayerMask;
        Debug.Log("change layer if needed");
    }
}

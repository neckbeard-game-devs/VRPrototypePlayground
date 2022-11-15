using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Since we can't make an Interactable non interactive, we use that to remove/add the Hands layer to the mask of the
/// Interactable, making it in effect non interactable with the direct Controllers.
/// </summary>
public class DisableInteractable : MonoBehaviour
{
    public void DisableInteraction(IXRSelectInteractor interactable)
    {
        interactable.transform.gameObject.layer &= ~(1<<LayerMask.NameToLayer("Hands"));
    }

    public void EnableInteraction(IXRSelectInteractor interactable)
    {
        interactable.transform.gameObject.layer |= (1<<LayerMask.NameToLayer("Hands"));
    }
}

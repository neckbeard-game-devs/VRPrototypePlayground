using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

/// <summary>
/// Handle destroying object when you throw it in the fire.
/// </summary>
public class FireDestruction : MonoBehaviour
{
    [FormerlySerializedAs("destroyEffect")]
    public VisualEffect DestroyEffect;

    public AudioSource DestroyAudioSource;
    
    void Start()
    {
        DestroyEffect.transform.position = new Vector3(0, 0, 0);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out XROffsetGrabbable scriptX) && !scriptX.isSelected)
        {
            if (!other.gameObject.TryGetComponent(out IndestructableObj scriptY))
            {
                var respawnable = other.GetComponent<RespawnableObject>();
                
                DestroyEffect.gameObject.transform.position = other.gameObject.transform.position;
                DestroyEffect.SendEvent("Explode");
                
                DestroyAudioSource.Play();

                if (respawnable == null)
                {
                    Destroy(other.gameObject, 0.2f);
                }
                else
                {
                    respawnable.Respawn();
                }
            }
        }       
    }
}

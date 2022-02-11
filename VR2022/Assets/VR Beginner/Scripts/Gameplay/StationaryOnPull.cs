using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryOnPull : MonoBehaviour
{
   public void MakeKinematic()
    {
        gameObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
}

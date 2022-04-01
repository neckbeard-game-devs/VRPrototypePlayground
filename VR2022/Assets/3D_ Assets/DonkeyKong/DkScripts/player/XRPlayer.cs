using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class XRPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnFire(InputValue value)
    {
        Debug.Log("fire input system");
    }
    public void OnLeftHandGrab(InputValue value)
    {
        Debug.Log("left hand grab input system");
    }
    public void OnRightHandGrab(InputValue value)
    {
        Debug.Log("right hand grab input system");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class XRPlayer : MonoBehaviour
{
    bool enableBeam;
    public MagicTractorBeam[] mtbs = new MagicTractorBeam[2]; 
    public void OnFire(InputValue value)
    {
        Debug.Log("fire input system");
    }
    public void OnLeftHandGrab(InputValue value)
    {
        Debug.Log("left hand grab input system");
        if (enableBeam)
        {
            enableBeam = false;
            foreach (var mtb in mtbs)
            {
                mtb.EnableBeam();
            }
        }
    }
    public void OnRightHandGrab(InputValue value)
    {
        Debug.Log("right hand grab input system");
        if (enableBeam)
        {
            enableBeam = false;
            foreach (var mtb in mtbs)
            {
                mtb.EnableBeam();
            }
        }
    }
    public void OnThumbStickDown(InputValue value)
    {
        if (!enableBeam)
        {
            enableBeam = true;
            foreach (var mtb in mtbs)
            {
                mtb.EnableBeam();
            }
        }  
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JoyContVR : NeckbeardsVrJoystick
{
    private PacManConClassic pmc;
    private MarioVrController tmc;
    protected override void TrackHand()
    {
        //Add if more debugging needed
        //Debug.Log(rotationGo.eulerAngles.y + "  y");
        //Debug.Log(rotationGo.eulerAngles.z + "  z");
        //Debug.Log(rotationGo.eulerAngles.x + "  x");
        transform.LookAt(hand.transform.position, transform.up);
        x = rotationGo.eulerAngles.x;
        z = rotationGo.eulerAngles.z;

        if (x >= 5 && x <= 80)
        {
            //Debug.Log("right");
            yTilt = 0;
            xTilt = -1;
        }
        else if (x >= 275 && x <= 350)
        {
            //Debug.Log("left");
            yTilt = 0;
            xTilt = 1;
        }
        else if (z >= 190 && z <= 265)
        {
            // Debug.Log("down");
            xTilt = 0;
            yTilt = -1;
        }
        else if (z >= 100 && z <= 175)
        {
            // Debug.Log("up");
            xTilt = 0;
            yTilt = 1;
        }
        else
        {
            xTilt = 0;
            yTilt = 0;
        }
    }
    protected override void ButtonPress()
    {
        if (buttonAnim != null)
        {
            buttonAnim.SetTrigger("press");
        }
    }
    protected override void SetCharacterControllers()
    {
        //change for your character controls
        pmc = FindObjectOfType<PacManConClassic>();
        tmc = FindObjectOfType<MarioVrController>();
    }
    private void OnTriggerEnter(Collider other)
    {            
        if (other.gameObject.CompareTag("hand"))
        {
            if (!zone2Bool)
            {
                pmc.joyCon = this;
                pmc.SetTrackingJoyStick();
            }
            else
            {
                tmc.joyCon = this;
                tmc.SetTrackingJoyStick();
            }
            hand = other.gameObject;
            touchJoyBool = true;

            if (other == handCols[0])
            {
                SetHand(0);
            }
            else if (other == handCols[1])
            {
                SetHand(1);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("hand"))
        {
            touchJoyBool = false;
            transform.localRotation = startPos;
            yTilt = 0;
            xTilt = 0;
        }
    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyContVRCollision : NeckbeardsVrJoystick
{
    private PacManConClassic pmc;
    private MarioVrController tmc;

    protected override void TrackHand()
    {
        transform.LookAt(hand.transform.position, transform.up);
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
                pmc.joyCol = this;
                pmc.SetCollisionJoyStick();
            }
            else
            {
                tmc.joyCol = this;
                tmc.SetCollisionJoyStick();
            }

            hand = other.gameObject;
            touchJoyBool = true;

            //arcade is stand alone can be left as null gameobject
            if (!arcadeBool)
            {
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

        if (other == joyCol[0])
        {
            yTilt = 1;
            xTilt = 0;
        }
        else if (other == joyCol[1])
        {
            yTilt = -1;
            xTilt = 0;
        }
        else if (other == joyCol[2])
        {
            xTilt = 1;
            yTilt = 0;
        }
        else if (other == joyCol[3])
        {
            xTilt = -1;
            yTilt = 0;
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

        if (other == joyCol[0] || other == joyCol[1])
        {
            yTilt = 0;
        }
        else if (other == joyCol[2] || other == joyCol[3])
        {
            xTilt = 0;
        }

    }
}

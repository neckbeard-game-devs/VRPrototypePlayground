using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NeckbeardsVrJoystick : MonoBehaviour
{
    public bool touchJoyBool, zone2Bool, arcadeBool;
    public float yTilt, xTilt;

    public GameObject hand, baseGo;
    public Transform rotationGo;
    public Collider[] joyCol, handCols;
    public Quaternion[] quaternions;
    public Vector3[] attachPos;
    public Transform[] attachGos;
    public Animator buttonAnim;
    public Quaternion startPos;
    public float x, y, z;

    private void Start()
    {
        //stores initial and left and right hand attach positions
        startPos = transform.localRotation;
        attachPos = new Vector3[attachGos.Length];
        for (int i = 0; i < attachPos.Length; i++)
        {
            attachPos[i] = attachGos[i].localPosition;
        }
        SetCharacterControllers();

    }
    private void Update()
    {
        if (touchJoyBool)
        {
            TrackHand();
        }
        else
        {
            transform.localRotation = startPos;
        }

    }
    public void SetHand(int side)
    {
        if (side == 0)
        {
            baseGo.transform.localRotation = quaternions[0];
            attachGos[0].localPosition = attachPos[0];
        }
        else
        {
            baseGo.transform.localRotation = quaternions[1];
            attachGos[0].localPosition = attachPos[1];
        }
    }
    protected abstract void SetCharacterControllers();
    protected abstract void TrackHand();
    protected abstract void ButtonPress();

}

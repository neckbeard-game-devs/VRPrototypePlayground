using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PacManConClassic : MonoBehaviour
{
    //move
    public JoyContVR joyCon;
    public JoyContVRCollision joyCol;
    public Animator anim;
    public Vector3 position;
    public Vector2 move;
    public float moveSpeed = .02f;
    public bool touchJoyBool, moveBool, trackingBool;
    public AudioSource wakaSound;
    //rotate
    [SerializeField]
    private Quaternion[] rotations;
    public GameObject rotationGo;

    //universal collision or tracking based joystick & 0||1 player input
    void Update()
    {
        if (trackingBool)
        {
            if (joyCon != null)
            {
                touchJoyBool = joyCon.touchJoyBool;
                move.y = joyCon.yTilt;
                move.x = joyCon.xTilt;
            }

        }
        else
        {
            if (joyCol != null)
            {
                touchJoyBool = joyCol.touchJoyBool;
                move.y = joyCol.yTilt;
                move.x = joyCol.xTilt;
            }
        }

        if (touchJoyBool)

        {
            position = (move.y * moveSpeed * transform.forward) + (move.x * moveSpeed * transform.right);
            transform.position += position;

            if (move.y == 1)
            {
                rotationGo.transform.localRotation = rotations[0];
            }
             if (move.y == -1)
            {
                rotationGo.transform.localRotation = rotations[1];
            }
            if (move.x == 1)
            {
                rotationGo.transform.localRotation = rotations[2];
            }
             if (move.x == -1)
            {
                rotationGo.transform.localRotation = rotations[3];
            }

           
        }

        if (move.y == 1 || move.y == -1)
        {
            if (!moveBool)
            {
                moveBool = true;
                anim.SetBool("move", true);
                wakaSound.Play();
            }
        }
        else if (move.x == 1 || move.x == -1)
        {
            if (!moveBool)
            {
                moveBool = true;
                anim.SetBool("move", true);
                wakaSound.Play();
            }
        }
        else
        {
            if (moveBool)
            {
                moveBool = false;
                anim.SetBool("move", false);
                wakaSound.Stop();
            }

        }
    }

    public void SetCollisionJoyStick()
    {
        trackingBool = false;
    }
    public void SetTrackingJoyStick()
    {
        trackingBool = true;
    }
}

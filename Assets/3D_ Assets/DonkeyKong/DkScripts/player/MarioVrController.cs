using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MarioVrController : MonoBehaviour
{
    public PlayerAudioCon pac;
    public JoyContVR joyCon;
    public JoyContVRCollision joyCol;
    //move
    public Rigidbody playerRb;
    public Vector3 position, nextPosition;
    public Vector2 move;
    public float moveSpeed = .02f, jumpPower = 250f;
    public bool startBool, ladderBool, climbBool, danceBool, 
            touchJoyBool, playsounds, playSoundBool, trackingBool;

    //rotate
    [SerializeField]
    private Quaternion[] rotations;
    public GameObject rotationGo;
    public Animator anim, ladderAnim;

    //climb
    public GameObject ladderGo;
    
    void Start()
    {
        nextPosition = transform.position;
    }
    public void Jumping()
    {
        anim.SetTrigger("jump");
        playerRb.AddForce(transform.up * jumpPower);
        pac.playerSfx.loop = false;
        pac.playerSfx.Play();
    }

    void Update()
    {
        if (trackingBool)
        {
            if (joyCon != null)
            {
                touchJoyBool = joyCon.touchJoyBool;
                move.x = joyCon.xTilt;
                move.y = joyCon.yTilt;
            }
        }
        else
        {
            if(joyCol != null)
            {
                touchJoyBool = joyCol.touchJoyBool;
                move.x = joyCol.xTilt;
                move.y = joyCol.yTilt;
            }

        }

        if (move.y == 1)
        {
            rotationGo.transform.rotation = rotations[0];
            if (ladderBool && !climbBool)
            {
                climbBool = true;
                StartCoroutine(ClimbCountdown());
            }

        }
        else if (move.y == -1)
        {
            rotationGo.transform.rotation = rotations[1];
            //anim.SetFloat("movementSpeed", 1f);
            if (!danceBool)
            {
                danceBool = true;
                StartCoroutine(DanceCountdown());
            }
        }
        else if (move.x == 1)
        {
            rotationGo.transform.rotation = rotations[2];
            anim.SetFloat("movementSpeed", 1f);
            //anim.SetTrigger("dance");
            playsounds = true;
        }
        else if (move.x == -1)
        {
            rotationGo.transform.rotation = rotations[3];
            anim.SetFloat("movementSpeed", 1f);
            playsounds = true;
        }
        else
        {
            anim.SetFloat("movementSpeed", 0f);
            playerRb.velocity = Vector3.zero;

            if (playsounds)
            {
                playsounds = false;
                playSoundBool = false;
                pac.playerMoveSound.Stop();

                //anim.SetBool("move", false);
            }

        }
        if (startBool && !climbBool)
        {
            position = (move.x * moveSpeed * transform.forward);
            transform.position += position;
        }

        if (playsounds)
        {
            if (!playSoundBool)
            {
                playSoundBool = true;
                pac.playerMoveSound.Play();
                //anim.SetBool("moveCharacter", true);
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
    public void StartHammerTime()
    {
        anim.SetTrigger("hammerTime");
        pac.playerSfx.clip = pac.playerSfxClips[1];
        pac.playerSfx.Play();

        pac.backGroundAudio.clip = pac.backGroundClips[1];
        pac.backGroundAudio.Play();
        StartCoroutine(HammertimeCountdown());
    }
    IEnumerator ClimbCountdown()
    {
       //Debug.Log("climb ladder");
        anim.SetBool("climbLadder", true);
        playerRb.constraints = RigidbodyConstraints.FreezeAll;
        
        ladderAnim.SetTrigger("climb");
        yield return new WaitForSeconds(.5f);

        //Debug.Log("finish climb");
        anim.SetTrigger("finishClimb");
        transform.localPosition = Vector3.zero;
        yield return new WaitForSeconds(1.5f);

       // Debug.Log("climb finished");
        anim.SetBool("climbLadder", false);


        transform.parent = null;
        playerRb.constraints = RigidbodyConstraints.FreezeRotationX
    | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        climbBool = false;
        ladderBool = false;
        yield return new WaitForSeconds(.2f);
        ladderAnim.SetTrigger("climb");


    }
    IEnumerator HammertimeCountdown()
    {
        yield return new WaitForSeconds(15f);
        anim.SetTrigger("hammerTime");
        pac.backGroundAudio.clip = pac.backGroundClips[0];
        pac.backGroundAudio.Play();
    }
    IEnumerator DanceCountdown()
    {
        anim.SetTrigger("dance");
        yield return new WaitForSeconds(15f);
        anim.SetTrigger("dance");
        danceBool = false;
    }
}

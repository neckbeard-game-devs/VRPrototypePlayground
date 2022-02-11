using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyContVR : MonoBehaviour
{
    private PacManConClassic pmc;
    public bool touchJoyBool;
    public float yTilt, xTilt;
    public Transform startPos;
    public Vector3 startVec;
    public GameObject hand, colliderParent;
    public Animator buttonAnim;
    public Collider[] joyCol;
    void Start()
    {
        startVec = colliderParent.transform.localPosition;
        startPos.localRotation = colliderParent.transform.localRotation;
        pmc = FindObjectOfType<PacManConClassic>();
    }
    // Update is called once per frame
    void Update()
    {
        //colliderParent.transform.localPosition = startVec;
        if (touchJoyBool)
        {            
            transform.LookAt(hand.transform.position, transform.up);
        }
    }
    public void ButtonPress()
    {
        if(buttonAnim != null)
        {
            buttonAnim.SetTrigger("press");
        }
    }
    private void OnTriggerEnter(Collider other)
    {                    
        // figure out which hand maybe gameobject[] with both hands
        if (other.gameObject.CompareTag("hand"))
        {
            pmc.joyCon = this;
            hand = other.gameObject;         
            touchJoyBool = true;
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
            transform.localRotation = startPos.localRotation;
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

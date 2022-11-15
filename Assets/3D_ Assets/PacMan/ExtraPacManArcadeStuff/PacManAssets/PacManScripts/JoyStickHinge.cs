using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickHinge : MonoBehaviour
{
    public bool _grabbedBool;
    public float yTilt, xTilt;
    public Transform startPos;
    public Collider[] joyCol;
    public GameObject _hand;
    public PacManConClassic pacMan;

    private void Start()
    {
        pacMan = FindObjectOfType<PacManConClassic>();
        startPos.rotation = transform.rotation;
    }
    private void Update()
    {

        if (_grabbedBool)
        {                   
            transform.LookAt(_hand.transform.position, transform.up);           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hand"))
        {
            _hand = other.gameObject;
            pacMan.touchJoyBool = true;
            _grabbedBool = true; 
        }

        if(other.gameObject == joyCol[0])
        {
            pacMan.move.y = 1;
        }
        else if (other.gameObject == joyCol[1])
        {
            pacMan.move.y = -1;
        }     
        else if (other.gameObject == joyCol[2])
        {
            pacMan.move.x = -1;
        }
        else if (other.gameObject == joyCol[3])
        {
            Debug.Log("Right");
            pacMan.move.x = 1;
        }
        // figure out which hand, maybe have lefthand/righthand tags
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("hand"))
        {
            pacMan.touchJoyBool = false;
            _grabbedBool = false;
            transform.rotation = startPos.rotation;
            pacMan.move.y = 0;
            pacMan.move.x = 0;
        }

        if (other.gameObject == joyCol[0])
        {
            pacMan.move.y = 0;
        }
        else if (other.gameObject == joyCol[1])
        {
            pacMan.move.y = 0;
        }
        else if (other.gameObject == joyCol[2])
        {
            pacMan.move.x = 0;
        }
        else if (other.gameObject == joyCol[3])
        {
            pacMan.move.x = 0;
        }
        // figure out which hand, maybe have lefthand/righthand tags
    }

}

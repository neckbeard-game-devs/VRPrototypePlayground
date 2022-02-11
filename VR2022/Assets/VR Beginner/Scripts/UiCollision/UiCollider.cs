using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiCollider : MonoBehaviour
{
    private UiAudioControl uac;
    public UiControl uiCon;
    public int buttonInt;
    public bool combinationBool, pressedBool;

    void Start()
    {
        uac = FindObjectOfType<UiAudioControl>();
    }

    // Update is called once per frame
   private void ButtonPress()
    {
        if (combinationBool)
        {
            uiCon.CombinationPress(buttonInt);
        }
        else
        {
            uiCon.CollisionPress(buttonInt);
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!pressedBool)
        {
            pressedBool = true;
           // Debug.Log(other.gameObject.name);
            ButtonPress();
        }
       
    }


}

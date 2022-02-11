using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISliderControl : MonoBehaviour
{
    private UiAudioControl uac;
    public Slider slider;
    public Light lightCon;
    public Vector3 startPos, lastPos, endPos;
    public bool moveBool, pitchBool, volumeBool, turnBool, playAudioBool, lightBool;
    public GameObject _hand, turnTableGo;
    public float speed = 1.0f;


    void Start()
    {
        uac = FindObjectOfType<UiAudioControl>();
        startPos = transform.localPosition;
        endPos = new Vector3(29, 0, 0);
    }


    void Update()
    {

        lastPos = transform.localPosition;
        
        if (lastPos.x <= 0)
        {
            transform.localPosition = startPos;
        }
        else if (lastPos.x >= 29)
        {
            transform.localPosition = endPos;
        }

        if (lastPos.z <= -4 || lastPos.z >= 4)
        {
            transform.localPosition = new Vector3(lastPos.x, 0, 0);
        }
        if (lastPos.y <= -4 || lastPos.y >= 4)
        {
            transform.localPosition = new Vector3(lastPos.x, 0, 0);
        }
        slider.value = lastPos.x;

        if (moveBool)
        {
            float step = speed * Time.deltaTime; 
            transform.position = Vector3.MoveTowards(transform.position, _hand.transform.position, step);
           
            if(pitchBool)
            {
                uac.uiAudioSources[1].pitch = slider.value / 10;
            }
            else if (volumeBool)
            {
                uac.uiAudioSources[1].volume = slider.value / 29;
            }
            else if (turnBool && turnTableGo != null)
            {
                turnTableGo.transform.Rotate(0, lastPos.x, 0.0f, Space.Self);
                if (!playAudioBool)
                {
                    playAudioBool = true;
                    if(uac.uiAudioSources[2] != null)
                    {
                        uac.uiAudioSources[2].Play();
                    }                   
                }
                else
                {
                    if (uac.uiAudioSources[2] != null)
                    {
                        uac.uiAudioSources[2].pitch = slider.value / 10;
                    }
                }
            }
            else if (lightBool && lightCon != null)
            {
                lightCon.intensity = slider.value / 5;
            }
        }
        else
        {
            if (playAudioBool)
            {
                playAudioBool = false;
                if (uac.uiAudioSources[2] != null)
                {
                    uac.uiAudioSources[2].Play();
                }
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        _hand = other.gameObject;
        moveBool = true;
    }

    private void OnTriggerExit(Collider other)
    {
        moveBool = false;
        if (playAudioBool)
        {
            playAudioBool = false;
            if (uac.uiAudioSources[2] != null)
            {
                uac.uiAudioSources[2].Stop();
            }
        }
    }
}

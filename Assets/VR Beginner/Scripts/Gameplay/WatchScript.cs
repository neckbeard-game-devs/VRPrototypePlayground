using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WatchScript : MonoBehaviour
{
    private UiAudioControl uac;
    public Slider LoadingSlider;
    public Canvas RootCanvas;      
    public bool m_Loading = false;
    public GameObject menuButton;
    public Animator canAnim;

    void Start()
    {
        uac = FindObjectOfType<UiAudioControl>();
        RootCanvas.worldCamera = Camera.main;      
    }
    public void OpenMenu()
    {
        if (!m_Loading)
        {
            m_Loading = true;
            uac.uiAudioSources[1].Play();
            canAnim.SetTrigger("Open");
            menuButton.SetActive(false);
            LoadingSlider.gameObject.SetActive(false);
        }
       
    }

    public void CloseMenu()
    {       
        if (m_Loading)
        {
            m_Loading = false;
            uac.uiAudioSources[1].Stop();
            canAnim.SetTrigger("Close");
            menuButton.SetActive(true);
            LoadingSlider.gameObject.SetActive(true);
        }
      
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICombinationControl : MonoBehaviour
{
    private UiAudioControl uac;
    public int pressesInt, pressesMax, matchInt;
    public bool resetBool;
    public int[] pressedButtonInts, combinationInts;
    public TMP_Text lockedText, resetLockText, comboText, inputText, buttonText;
    public Image lockedPanelColor;
    public Color[] lockedColors;

    private void Start()
    {
        uac = FindObjectOfType<UiAudioControl>();
        comboText.text = "" + combinationInts[0] + combinationInts[1] + combinationInts[2];
    }
    public void ComboButtonPressed(int comboButton)
    {
        if(pressesInt >= pressesMax)
        {
            if (uac.uiAudioSources[3] != null)
            {
                uac.uiAudioSources[3].clip = uac.combinationAudioClips[1];
                uac.uiAudioSources[3].Play();
            }
        }
        else
        {
            if (uac.uiAudioSources[3] != null)
            {
                uac.uiAudioSources[3].clip = uac.combinationAudioClips[0];
                uac.uiAudioSources[3].Play();
            }

            for (int i = 0; i < pressesMax; i++)
            {
                if (pressesInt == i)
                {
                    pressedButtonInts[i] = comboButton;
                }
            }
            pressesInt += 1;

            switch (pressesInt)
            {
                case 1:
                    inputText.text = "" + pressedButtonInts[0];
                    break;
                case 2:
                    inputText.text = "" + pressedButtonInts[0] + pressedButtonInts[1];
                    break;
                case 3:
                    inputText.text = "" + pressedButtonInts[0] + pressedButtonInts[1] + pressedButtonInts[2];
                    break;
            }
        }
    }

    public void CheckCombo()
    {
        
        if (resetBool)
        {
            Debug.Log("checkComboReset");
            if (pressesInt == 3)
            {
                pressesInt = 0;
                resetBool = false;
                
                for (int i = 0; i < combinationInts.Length; i++)
                {
                    combinationInts[i] = pressedButtonInts[i];
                }
                for (int i = 0; i < pressedButtonInts.Length; i++)
                {
                    pressedButtonInts[i] =0 ;
                }

                comboText.text = "" + combinationInts[0] + combinationInts[1] + combinationInts[2];
                inputText.text = "";
                lockedText.text = "Locked";
                resetLockText.text = "Enter 3 digit combo";
                buttonText.text = "Enter";
                lockedPanelColor.color = lockedColors[0];

                if (uac.uiAudioSources[3] != null)
                {
                    uac.uiAudioSources[3].clip = uac.combinationAudioClips[0];
                    uac.uiAudioSources[3].Play();
                }
            }
            else
            {
                if (uac.uiAudioSources[3] != null)
                {
                    uac.uiAudioSources[3].clip = uac.combinationAudioClips[1];
                    uac.uiAudioSources[3].Play();
                }
            }
        }
        else
        {
            Debug.Log("checkCombo");
            pressesInt = 0;        
            matchInt = 0;
            for (int i = 0; i < combinationInts.Length; i++)
            {
                if (combinationInts[i] == pressedButtonInts[i])
                {
                    matchInt += 1;
                }
            }
            if (matchInt == 3)
            {
                matchInt = 0;
                resetBool = true;
                lockedText.text = "Unlocked";
                resetLockText.text = "Reset 3 Digit Combo";
                buttonText.text = "Reset";
                lockedPanelColor.color = lockedColors[1];
                if (uac.uiAudioSources[3] != null)
                {
                    uac.uiAudioSources[3].clip = uac.combinationAudioClips[2];
                    uac.uiAudioSources[3].Play();
                }
            }
            else
            {
                for (int i = 0; i < pressedButtonInts.Length; i++)
                {
                    pressedButtonInts[i] = 0;
                }
                if (uac.uiAudioSources[3] != null)
                {
                    uac.uiAudioSources[3].clip = uac.combinationAudioClips[1];
                    uac.uiAudioSources[3].Play();
                }
            }
        }
       

    }

    public void ResetLock()
    {
      
        
    }
}

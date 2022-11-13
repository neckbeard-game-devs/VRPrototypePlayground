using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// uncomment for new button setup
//[ExecuteInEditMode]
public class UiControl : MonoBehaviour
{
    public UiAudioControl uac;

    [SerializeField]
    private UICombinationControl[] ucc;
    public Button[] testButtons, combinationButtons, keyboardButtons;
    public bool SetUpButtonsBool, SetUpComboButtonsBool, pressButtonBool;
    public float width, height;
    public ColorBlock colors;
    public WaitForSeconds bPress;
    public float waitTime = 1f;
    void Start()
    {
        ucc = GetComponentsInChildren<UICombinationControl>();
    }

    public void CollisionPress(int testButton)
    {
        bPress = new WaitForSeconds(waitTime);
        StartCoroutine(ButtonPressCountdown(testButton));
    }
    public void CombinationPress(int testButton)
    {
        Debug.Log("CombinationPress 0  " + testButton);
        bPress = new WaitForSeconds(waitTime);
        Debug.Log("CombinationPress 1  ");
        StartCoroutine(CombinationButtonPressCountdown(testButton));
        Debug.Log("CombinationPress 2  ");
    }
    IEnumerator KeyboardButtonPressCountdown0(int testButton)
    {
        Debug.Log("KeyboardPress 2  " + testButton);
        pressButtonBool = true;
        ucc[1].KeyboardButtonPressed(testButton);
        Debug.Log("KeyboardPress 3  " + testButton);
        colors = keyboardButtons[testButton].colors;
        colors.normalColor = colors.pressedColor;
        keyboardButtons[testButton].colors = colors;
        keyboardButtons[testButton].onClick.Invoke();


        
        yield return bPress;
        colors.normalColor = colors.highlightedColor;
        keyboardButtons[testButton].colors = colors;
        keyboardButtons[testButton].GetComponent<UiCollider>().pressedBool = false;
        pressButtonBool = false;
    }
    public void KeyboardPress(int testButton)
    {
        Debug.Log("KeyboardPress 0  " + testButton);
        bPress = new WaitForSeconds(waitTime);
        Debug.Log("KeyboardPress 1  " + testButton);
        StartCoroutine(KeyboardButtonPressCountdown0(testButton));
        
    }
    public void Press0()
    {
        ButtonPressSound();
    }
    public void Press1()
    {
        ButtonPressSound();
    }
    public void Press2()
    {
        ButtonPressSound();
    }
    public void Press3()
    {
        ButtonPressSound();
    }
    public void ButtonPressSound()
    {
        uac.uiAudioSources[0].Play();
    }


    // uncomment for new button setup

    //void Update()
    //{
    //    if (SetUpButtonsBool)
    //    {
    //        SetUpButtonsBool = false;
    //        StartCoroutine(ButtonSetupCountdown());
    //    }
    //    if (SetUpComboButtonsBool)
    //    {
    //        SetUpComboButtonsBool = false;
    //        StartCoroutine(CombinationButtonSetupCountdown());
    //    }
    //}
    IEnumerator ButtonPressCountdown(int testButton)
    {
        
        pressButtonBool = true;
        colors = testButtons[testButton].colors;
        colors.normalColor = colors.pressedColor;
        testButtons[testButton].colors = colors;
        testButtons[testButton].onClick.Invoke();
        Debug.Log("KeyboardPress 3  " + testButton);
        yield return bPress;
        colors.normalColor = colors.highlightedColor;
        testButtons[testButton].colors = colors;
        testButtons[testButton].GetComponent<UiCollider>().pressedBool = false;
        pressButtonBool = false;
    }
    IEnumerator CombinationButtonPressCountdown(int testButton)
    {
        pressButtonBool = true;
        ucc[0].ComboButtonPressed(testButton);
        colors = combinationButtons[testButton].colors;
        colors.normalColor = colors.pressedColor;
        combinationButtons[testButton].colors = colors;
        combinationButtons[testButton].onClick.Invoke();



        yield return bPress;
        colors.normalColor = colors.highlightedColor;
        combinationButtons[testButton].colors = colors;
        combinationButtons[testButton].GetComponent<UiCollider>().pressedBool = false;
        pressButtonBool = false;
    }


    
    IEnumerator ButtonSetupCountdown()
    {
        for (int i = 0; i < testButtons.Length; i++)
        {
            if (testButtons[i] != null)
            {
                if (!testButtons[i].gameObject.GetComponent<UiCollider>())
                {
                    //testButtons[i].onClick.AddListener
                    testButtons[i].gameObject.AddComponent<BoxCollider>();
                    width = testButtons[i].GetComponent<RectTransform>().rect.width;
                    height = testButtons[i].GetComponent<RectTransform>().rect.height;
                    testButtons[i].GetComponent<BoxCollider>().size = new Vector3(width, height, 5f);
                    testButtons[i].GetComponent<BoxCollider>().isTrigger = true;

                    testButtons[i].gameObject.AddComponent<UiCollider>();
                    testButtons[i].gameObject.GetComponent<UiCollider>().uiCon = this;
                    testButtons[i].gameObject.GetComponent<UiCollider>().buttonInt = i;
                }
                else
                {
                    Debug.Log(testButtons[i].gameObject.name + " already set");
                }
            }
        }

        yield return bPress;



    }
    IEnumerator CombinationButtonSetupCountdown()
    {
        for (int i = 0; i < combinationButtons.Length; i++)
        {
            if (combinationButtons[i] != null)
            {
                if (!combinationButtons[i].gameObject.GetComponent<UiCollider>())
                {
                    //testButtons[i].onClick.AddListener
                    combinationButtons[i].gameObject.AddComponent<BoxCollider>();
                    width = combinationButtons[i].GetComponent<RectTransform>().rect.width;
                    height = combinationButtons[i].GetComponent<RectTransform>().rect.height;
                    combinationButtons[i].GetComponent<BoxCollider>().size = new Vector3(width, height, 5f);
                    combinationButtons[i].GetComponent<BoxCollider>().isTrigger = true;

                    combinationButtons[i].gameObject.AddComponent<UiCollider>();
                    combinationButtons[i].gameObject.GetComponent<UiCollider>().uiCon = this;
                    combinationButtons[i].gameObject.GetComponent<UiCollider>().buttonInt = i;
                    combinationButtons[i].gameObject.GetComponent<UiCollider>().combinationBool = true;
                }
                else
                {
                    Debug.Log(combinationButtons[i].gameObject.name + " already set");
                }
            }
        }

        yield return bPress;



    }
}

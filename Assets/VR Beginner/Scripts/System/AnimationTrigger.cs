using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public string TriggerName;

    int m_TriggerID;

    void Awake()
    {
        m_TriggerID = Animator.StringToHash(TriggerName);
    }

    public void TriggerAnimation(Component component)
    {
        Animator animator = component.GetComponentInChildren<Animator>();

        if (animator != null)
        {
            animator.SetTrigger(m_TriggerID);
        }
    }
}

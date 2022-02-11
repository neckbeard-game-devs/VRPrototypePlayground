using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectActivator : MonoBehaviour
{
    public bool Toggle = false;
    public UnityEvent OnActivated;
    public UnityEvent OnDeactivated;

    bool m_Activated = false;
    
    public void Activated()
    {
        if (Toggle)
        {
            if (m_Activated)
                OnDeactivated.Invoke();
            else
                OnActivated.Invoke();
            m_Activated = !m_Activated;
        }
        else
        {
            OnActivated.Invoke();
            m_Activated = true;
        }
    }

    public void Deactivated()
    {
        if (!Toggle)
        {
            OnDeactivated.Invoke();
            m_Activated = false;
        }
    }
}

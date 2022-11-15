using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

//Allow for controlling the camera & hand controller when in editor without a headset present
//will also allow for camera control on mobile, for performace test
public class DebugController : MonoBehaviour
{
    public bool InvertY = false;
    public float CameraRotateSpeed = 180.0f;
    public Transform RigTransform;
    public Transform CameraPivotTransform;
    
    Quaternion m_TargetRotation, m_TargetPivotRotation;
   CursorLockMode m_CursorLockMode = CursorLockMode.Locked;
   bool m_CursorVisible = false;
   
   public static bool VRDisplayIsPresent()
   {
       var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
       SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
       foreach (var xrDisplay in xrDisplaySubsystems)
       {
           if (xrDisplay.running)
           {
               return true;
           }
       }
       return false;
   }
    
    // Start is called before the first frame update
    void Start()
    {
        if (VRDisplayIsPresent() && !Application.isEditor)
        {
            Destroy(this);
            return;
        }
        
        Cursor.lockState = m_CursorLockMode;
        Cursor.visible = m_CursorVisible;
    }

    // Update is called once per frame
    void Update()
    {
        TeleportationAnchor anchor = null;
        RaycastHit hit;
        if (Physics.Raycast(CameraPivotTransform.position, CameraPivotTransform.forward, out hit, 100.0f, LayerMask.GetMask("Teleporter")))
        {
            anchor = hit.collider.GetComponentInParent<TeleportationAnchor>();
        }
        
        if (Input.GetMouseButton(1) && anchor != null)
        {//if the right button is pressed 
            RigTransform.transform.position = anchor.transform.position;
        }
        else
        {
            UpdateLookRotation();
        }
        
    }
    
    void UpdateLookRotation()
    {
        var x = Input.GetAxis("Mouse Y");
        var y = Input.GetAxis("Mouse X");

        x *= InvertY ? -1 : 1;
        m_TargetRotation = RigTransform.localRotation * Quaternion.AngleAxis(y * CameraRotateSpeed * Time.deltaTime, Vector3.up);
        m_TargetPivotRotation = CameraPivotTransform.localRotation * Quaternion.AngleAxis(x * CameraRotateSpeed * Time.deltaTime, Vector3.right);

        transform.localRotation = m_TargetRotation;
        CameraPivotTransform.localRotation = m_TargetPivotRotation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DkPlatformController : MonoBehaviour
{
    public bool setupBool, resetBool, smallPlatform, rightSideBool;
    public int setupInt, platformLength;
    public float x, y, z;
    public GameObject[] platformGos, basePlatforms;
    public BoxCollider bCollider;

    void Update()
    {
        if (setupBool)
        {
            setupBool = false;
            SetupPlatforms();
        }
        else if (resetBool)
        {
            resetBool = false;
            ResetPlatforms();
        }
        else if (setupInt == 1)
        {
            for (int i = 1; i < platformLength; i++)
            {
                if(platformGos[i] == null)
                {
                    setupInt = 0;
                    ResetPlatforms();
                    break;
                }

                if (rightSideBool)
                    platformGos[i].transform.position = new Vector3(transform.position.x + (x * i), 
                        transform.position.y + (y * i), transform.position.z - (z * i));
                else
                    platformGos[i].transform.position = new Vector3(transform.position.x + (x * i), 
                        transform.position.y + (y * i), transform.position.z + (z * i));
            }
        }
    }
    void ResetPlatforms()
    {
        setupInt = 0;
        for (int i = 1; i < platformLength; i++)
        {
            if (platformGos[i] != null)
            {
                DestroyImmediate(platformGos[i]);
            }
        }

        basePlatforms[0].SetActive(true);
        basePlatforms[1].SetActive(false);
        x = 0;
        y = 0;
        z = 0;
        
    }
    void SetupPlatforms()
    {
        setupInt = 0;

        bCollider = basePlatforms[0].GetComponent<BoxCollider>();
        x = 0;
        y = bCollider.bounds.size.y;
        z = bCollider.bounds.size.z;

        platformGos = new GameObject[platformLength];
        platformGos[0] = gameObject;

        for (int i = 1; i < platformLength; i++)
        {
            platformGos[i] = Instantiate(basePlatforms[0], transform.position, transform.rotation);
            platformGos[i].transform.parent = transform;
            platformGos[i].transform.localPosition = new Vector3(0f, 0f, transform.position.z + (z * i));
        }

        if (smallPlatform)
        {
            basePlatforms[0].SetActive(false);
            basePlatforms[1].SetActive(true);
        }

        setupInt = 1;
    }
}

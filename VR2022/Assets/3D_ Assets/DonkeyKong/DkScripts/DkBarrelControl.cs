using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class DkBarrelControl : MonoBehaviour
{
    public Rigidbody barrelRb;
    private PlayerAudioCon pac;
    public float torque;
    public bool rollingBool, triggerBool;

    public Collider[] ladderCols, levelCols, edgeCols;
    public Collider hammerCollider;
    void Start()
    {
        pac = FindObjectOfType<PlayerAudioCon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rollingBool)
        {
            barrelRb.AddTorque(transform.right * torque);
        }
        else
        {
            barrelRb.AddTorque(-transform.right * torque);
           //barrelRb. AddTorque(torque, 0, 0, ForceMode.Force);
        }    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("teleport"))
        {
            for (int x = 0; x < ladderCols.Length; x++)
            {
                if (other == ladderCols[x])
                {

                    //transform.parent = ladderParent[i].gameObject.transform;
                    //tmc.ladderAnim = Ladders[i].ladderAnim;
                }
            }
        }

        if (other.gameObject.CompareTag("navCol"))
        {
            for (int y = 0; y < levelCols.Length; y++)
            {
                if (other == levelCols[y] && !triggerBool)
                {
                    //Debug.Log("Hit level Collider");
                    triggerBool = true;
                    if (rollingBool)
                    {
                        rollingBool = false;
                    }
                    else
                    {
                        rollingBool = true;
                    }
                    transform.position = new Vector3(levelCols[y].transform.position.x, transform.position.y, transform.position.z);
                    StartCoroutine(TriggerCountdown());
                }
            }
        }

        if (other.gameObject.CompareTag("bubbles"))
        {
            for (int z = 0; z < edgeCols.Length; z++)
            {
                if (other == edgeCols[z])
                {
                    //tmc.anim.SetBool("edge", true);
                }
            }
        }

        if (other.gameObject.CompareTag("warpCol"))
        {
            Destroy(gameObject, 5f);
        }

        if (other == hammerCollider)
        {
            pac.playerSfx.clip = pac.playerSfxClips[1];
            pac.playerSfx.Play();
            Destroy(gameObject);
        }
    }
    IEnumerator TriggerCountdown()
    {
        yield return new WaitForSeconds(3f);
        triggerBool = false;
    }
}

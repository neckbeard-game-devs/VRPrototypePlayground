using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderCollider : MonoBehaviour
{
    private MarioVrController tmc;
    public LadderCon[] Ladders;
    public Animator[] ladderAnims;
    public Collider[] ladderCols, levelCols, edgeCols, hammerCols;
    public Transform[] ladderParent;

    private void Start()
    {
        tmc = GetComponent<MarioVrController>();

        ladderCols = new Collider[Ladders.Length];
        ladderParent = new Transform[Ladders.Length];
        ladderAnims = new Animator[Ladders.Length];

        for (int i = 0; i < Ladders.Length; i++)
        {
            ladderCols[i] = Ladders[i].ladderCol;
            ladderParent[i] = Ladders[i].ladderParentGo;
            ladderAnims[i] = Ladders[i].ladderAnim;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //makes sure what for loop to run instead of all used current tags in project
        if (other.gameObject.CompareTag("teleport"))
        {
            for (int x = 0; x < ladderCols.Length; x++)
            {
                if (other == ladderCols[x])
                {
                    tmc.ladderBool = true;
                    transform.parent = ladderParent[x].gameObject.transform;
                    tmc.ladderAnim = Ladders[x].ladderAnim;
                }
            }
        }

        if (other.gameObject.CompareTag("navCol"))
        {
            for (int y = 0; y < levelCols.Length; y++)
            {
                if (other == levelCols[y])
                {
                    transform.position = new Vector3(levelCols[y].transform.position.x, transform.position.y, transform.position.z);
                }
            }
        }

        if (other.gameObject.CompareTag("bubbles"))
        {
            for (int z = 0; z < edgeCols.Length; z++)
            {
                if (other == edgeCols[z])
                {
                    tmc.anim.SetBool("edge", true);
                }
            }
        }

        if (other.gameObject.CompareTag("bigbubbles"))
        {
            for (int a = 0; a < hammerCols.Length; a++)
            {
                if (other == hammerCols[a])
                {
                    tmc.StartHammerTime();
                    Destroy(other.gameObject);
                }
            }
        }
       
        if (other.gameObject.CompareTag("warpCol"))
        {
            tmc.playerRb.isKinematic = true;
            StartCoroutine(ResetPosition());
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("teleport"))
        {
            for (int x = 0; x < ladderCols.Length; x++)
            {
                if (other == ladderCols[x])
                {
                    tmc.ladderBool = false;
                }
            }
        }

        if (other.gameObject.CompareTag("bubbles"))
        {
            for (int y = 0; y < edgeCols.Length; y++)
            {
                if (other == edgeCols[y])
                {
                    tmc.anim.SetBool("edge", false);
                }
            }
        } 
    }
    IEnumerator ResetPosition()
    {
        //Debug.Log("On ground");
        yield return new WaitForSeconds(1f);
        transform.position = tmc.nextPosition;
        tmc.playerRb.isKinematic = false;
    }
}

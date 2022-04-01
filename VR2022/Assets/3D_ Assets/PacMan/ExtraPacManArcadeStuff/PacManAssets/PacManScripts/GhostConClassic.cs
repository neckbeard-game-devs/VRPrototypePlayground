using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostConClassic : MonoBehaviour
{
    private GameControl tgc;
    public GameObject[] navMeshCol;
    public NavMeshAgent ghostAgent;
    public bool setTargetBool, attackBool, hitPacmanBool;
    public int colInt, ranNum, waitInt;
    WaitForSeconds waitingTime;

    void Start()
    {
        waitInt = 2;
        waitingTime = new WaitForSeconds(waitInt);
        SetDestination();
        ghostAgent = GetComponent<NavMeshAgent>();
        tgc = FindObjectOfType<GameControl>();
    }

    // Update is called once per frame
    public void SetDestination()
    {       
        if (setTargetBool)
        {
            ranNum = Random.Range(0, navMeshCol.Length);            
        }
        else
        {
            ranNum = Random.Range(1, navMeshCol.Length);
        }
        setTargetBool = false;
       
        if (ranNum != colInt)
        {
            ghostAgent.SetDestination(navMeshCol[ranNum].transform.position);
        }
        else
        {
            SetDestination();
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!attackBool)
        {
            if (other.gameObject.CompareTag("navCol"))
            {
                //Debug.Log("navCol");
                for (int i = 0; i < navMeshCol.Length; i++)
                {
                    if (other.gameObject == navMeshCol[i] && !setTargetBool)
                    {
                        setTargetBool = true;
                        colInt = i;
                        SetDestination();
                    }
                }
            }
            if (other.gameObject.CompareTag("npc"))
            {
                //Debug.Log("npc");
                if (!setTargetBool)
                {
                    setTargetBool = true;
                    SetDestination();
                }
            }
            if (other.gameObject.CompareTag("playerCol"))
            {
                //Debug.Log("playerCol");
                if (!attackBool)
                {
                    attackBool = true;
                    StartCoroutine(AttackCountdown());
                }
            }
            if (other.gameObject.CompareTag("Player"))
            {
                //Debug.Log("Player");
                if (!hitPacmanBool)
                {
                    hitPacmanBool = true;
                    SetDestination();
                    tgc.PacmanHit();
                }
                SetDestination();
            }
        }      
    }

    IEnumerator AttackCountdown()
    {
        for (int i = 0; i < 5; i++)
        {
            ghostAgent.SetDestination(navMeshCol[0].transform.position);
            yield return waitingTime;
        }
        attackBool = false;

        SetDestination();
    }
}

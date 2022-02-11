using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GhostCon : MonoBehaviour
{
    public GhostSpawner gSpawner;
    public NavMeshAgent agent;
    public GameObject[] navTargets;
    public bool startBool, leaderBool, hitBool, clickBool, defendBool, deadBool, targetBool, setTargetBool, attackBool;
    public int colInt, ranNum;
    // Start is called before the first frame update
    void Start()
    {
        SetDestination();
    }
    public void SetDestination()
    {
        ranNum = Random.Range(1, 5);
        agent.isStopped = false;
        if (navTargets[ranNum] != null && ranNum != colInt)
        {
            agent.destination = navTargets[ranNum].transform.position;           
        }
        else
        {
            ranNum = Random.Range(0, 5);
            agent.isStopped = false;
            if (navTargets[ranNum] != null && ranNum != colInt)
            {
                agent.destination = navTargets[ranNum].transform.position;
            }
            else
            {
                SetDestination();
            }
        }
        startBool = true;
    }
    public void TargetPlayer()
    {
        if (!attackBool)
        {
            attackBool = true;            
            StartCoroutine(AttackCountdown());
        }
        
    }
    IEnumerator AttackCountdown()
    {
        for (int i = 0; i < 5; i++)
        {
            agent.destination = navTargets[0].transform.position;
            yield return new WaitForSeconds(2f);
        }        
        attackBool = false;
        setTargetBool = false;
        colInt = 0;
        SetDestination();
    }
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.CompareTag("teleport"))
        {
            for (int i = 0; i < navTargets.Length; i++)
            {
                if (other.gameObject == navTargets[i])
                {
                    setTargetBool = false;
                    colInt = i;
                    SetDestination();
                }
            }
        }
        if (other.gameObject.CompareTag("ghost"))
        {
            Debug.Log("hit ghost");
            SetDestination();
            colInt = 0;
        }
    }
}

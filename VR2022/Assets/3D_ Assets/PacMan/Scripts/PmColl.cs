using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PmColl : MonoBehaviour
{
    public GameObject[] ghosts;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            if (other.gameObject == ghosts[i])
            {
                ghosts[i].GetComponent<GhostCon>().TargetPlayer();
            }
        }

    }
}

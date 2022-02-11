using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpCol : MonoBehaviour
{
    public GameControl tgc;
    public bool  warpBool;
    public int warpColInt;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("npc") && !warpBool)
        {
            warpBool = true;
            tgc.WarpNpc();
        }

        if (collision.gameObject.CompareTag("Player") && !warpBool)
        {
            warpBool = true;
            tgc.WarpPlayer(warpColInt);
        }

    }

}

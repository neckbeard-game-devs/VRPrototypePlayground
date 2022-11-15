using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCol : MonoBehaviour
{
    private GameControl tgc;
    public bool startBool, destroyBool;



    private void OnTriggerEnter(Collider other)
    {
        if (!startBool)
        {
            startBool = true;
            tgc = FindObjectOfType<GameControl>();
        }

        if (other.gameObject.CompareTag("Player") && !destroyBool)
        {
            destroyBool = true;
            tgc.EatPellets();
            Destroy(gameObject);
        }
    }
}

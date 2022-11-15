using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.AI;
public class PacManCon : MonoBehaviour
{
    // Start is called before the first frame update
    public DialInteractable[] tankLevers;
    public GameObject[] leverGos;
    public GameObject xrRigGO;
    public Transform xrRigSpawnPnt;
    private float rotationSpeed = 0.0f;
    private float moveSpeed = 0.0f;
    public float speedRegulator = 0.05f;
    public NavMeshAgent agent;
    public AudioSource wakaSound;
    public bool playWakaBool;
    public int score;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //xrRigGO.transform.position = xrRigSpawnPnt.transform.position;
        //xrRigGO.transform.rotation = xrRigSpawnPnt.transform.rotation;
        if (leverGos[0].transform.eulerAngles.x >= 20f && leverGos[1].transform.eulerAngles.x >= 20f)
        {
            moveSpeed = ((leverGos[0].transform.eulerAngles.x + leverGos[1].transform.eulerAngles.x) * speedRegulator);
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            //Vector3 movement = Vector3.forward * Time.deltaTime * moveSpeed;
            //agent.Move(movement);
            if (!playWakaBool)
            {
                playWakaBool = true;
                wakaSound.Play();
            }
        }
        else
        {
            if (playWakaBool)
            {
                playWakaBool = false;
                wakaSound.Stop();
            }
        }
        rotationSpeed = leverGos[0].transform.eulerAngles.x + (leverGos[1].transform.eulerAngles.x * -1.0f);

        if (Mathf.Abs(rotationSpeed) > 1.0f)
        {
            transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * rotationSpeed);
        }
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bubbles"))
        {
            Destroy(other.gameObject);
            score += 1;
        }
        if (other.gameObject.CompareTag("bigbubbles"))
        {
            Destroy(other.gameObject);
            score += 1;
        }
    }
}

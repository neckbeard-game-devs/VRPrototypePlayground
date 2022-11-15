using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[ExecuteInEditMode]
public class GhostSpawner : MonoBehaviour
{
    public GhostCon[] ghostCons;
    public NavMeshAgent[] ghostAgents;
    public bool targetPlayerBool, spawnBubbles;
    public bool[] ghostBool;
    public GameObject[] ghostPrefabs, ghostGos, dotPrefab;
    public Transform playeGo, dotSpawnPos, newSpawnPos;
    public GameObject[,] grid;
    public int width, height;
    void Start()
    {
        grid = new GameObject[width, height];
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnBubbles)
        {
            spawnBubbles = false;           
            for (int i = 0; i <= width - 1; i+=5)
            {
                for (int j = 0; j <= height - 1; j+=5)
                {
                    grid[i, j] = Instantiate(dotPrefab[0], new Vector3(i, 144, j), transform.rotation);
                }
            }
        }
    }
}

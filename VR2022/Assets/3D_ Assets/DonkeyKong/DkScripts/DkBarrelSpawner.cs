using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DkBarrelSpawner : MonoBehaviour
{
    private DkBarrelControl dbk;
    public GameObject barrelPrefab;
    private GameObject barrelGo;
    public Transform barrelSpawnPoint;
    private bool spawnBool = true;

    public Collider[] ladderCols, levelCols, edgeCols;
    public Collider hammerCollider;
    void Update()
    {
        if (spawnBool)
        {
            spawnBool = false;
            StartCoroutine(SpawnBarrelsYeah());
        }
    }

    IEnumerator SpawnBarrelsYeah()
    {
        yield return new WaitForSeconds(6f);
        barrelGo = Instantiate(barrelPrefab, barrelSpawnPoint.position, barrelSpawnPoint.rotation);
        dbk = barrelGo.GetComponent<DkBarrelControl>();
        dbk.levelCols = levelCols;
        dbk.hammerCollider = hammerCollider;

        spawnBool = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointActivation : MonoBehaviour {

    float spawnWait;
    float spawnInterval;
    float spawnCD;
    float spawnCount;
    public GameObject enemySpawn;

    private void Start()
    {

        spawnInterval = 3f;
        spawnWait = 3f;
        spawnCD = 15f;
        spawnCount = 5f;
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            StartCoroutine(SpawnEnemy());

        }

    }

        IEnumerator SpawnEnemy()
    {

        yield return new WaitForSeconds(spawnWait);

        while (true)

        {
            for (int i = 0; i < spawnCount; i++)
            {
                Instantiate(enemySpawn, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(spawnInterval);
            }

            yield return new WaitForSeconds(spawnCD);

        }
    }
}
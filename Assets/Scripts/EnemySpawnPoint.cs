using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour {

    public bool isSpawningEnemies;
    float spawnWait;
    float spawnInterval;
    float spawnCD;
    float spawnCount;
    public List<GameObject> enemySpawns = new List<GameObject>();

    private void Start()
    {
        isSpawningEnemies = true;
        spawnInterval = 5f;
        spawnWait = 3f;
        spawnCD = 15f;
        spawnCount = 5f;
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {

        

        for (int j = 0; j < spawnCount; j++)
        {
            yield return new WaitForSeconds(spawnWait);
            {
                for (int i = 0; i < enemySpawns.Count; i++)
                {
                    Instantiate(enemySpawns[i], transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(spawnInterval);
                }

                yield return new WaitForSeconds(spawnCD);

            }
            
        }      
    }   
}
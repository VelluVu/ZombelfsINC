using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPack : MonoBehaviour {

    public GameObject enemySpawn;
    int packSize;

    private void Start()
    {
        packSize = 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < packSize; i++)
            {
                Instantiate(enemySpawn, transform.position, Quaternion.identity);
            }           
        }
    }
}

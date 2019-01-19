using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPack : MonoBehaviour {

    public List<GameObject> enemySpawns = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < enemySpawns.Count; i++)
            {
                Instantiate(enemySpawns[i], transform.position, Quaternion.identity);
            }           
        }
    }
}

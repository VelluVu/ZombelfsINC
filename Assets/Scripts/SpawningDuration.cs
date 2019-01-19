using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningDuration : MonoBehaviour {

    float lifetime;

    void Start()
    {

        lifetime = 65f;
        gameObject.GetComponent<EnemySpawnPoint>().isSpawningEnemies = false;
        Destroy(gameObject, lifetime);
        
    }
}

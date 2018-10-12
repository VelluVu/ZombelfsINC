using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningDuration : MonoBehaviour {

    
        float lifetime;

        void Start()
        {
            lifetime = 65f;
            Destroy(gameObject, lifetime);
        
        }
}

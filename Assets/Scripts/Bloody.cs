using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloody : MonoBehaviour {

    ParticleSystem ps;
    public List<GameObject> bloodSplatters = new List<GameObject>();
    int counter;
    int gore;

    // Use this for initialization
    void Start () {
        ps = gameObject.GetComponent<ParticleSystem>();
        counter = 0;
        gore = GameStatus.gore;
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (counter < gore)
            {
                counter++;
                Instantiate(bloodSplatters[Random.Range(0, bloodSplatters.Count)], transform.position, Quaternion.identity);
            }
        }

    }


}

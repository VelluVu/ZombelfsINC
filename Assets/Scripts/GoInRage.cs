using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoInRage : MonoBehaviour {

    float timeToEnrage;
    float speedIncrease;

	void Start () {

       
        timeToEnrage = 3f;
        speedIncrease = 10f;

        StartCoroutine(Enrage());

	}

    IEnumerator Enrage()
    {
        yield return new WaitForSeconds(timeToEnrage);
        Debug.Log("I enraged " + gameObject.name);
        gameObject.GetComponentInChildren<Enemy>().EnemySpeedIncrease(speedIncrease);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoInRage : MonoBehaviour {

    float timeToEnrage;
    float speedIncrease;

	void Start () {

       
        timeToEnrage = 10f;
        speedIncrease = 0.2f;

        StartCoroutine(Enrage());

	}

    IEnumerator Enrage()
    {
        for (int i = 0; i < 5; i++)
        {

            if (gameObject != null) {
                yield return new WaitForSeconds(timeToEnrage);
                //Debug.Log("I enraged " + gameObject.name);
                gameObject.GetComponentInChildren<EnemyBase>().EnemySpeedIncrease(speedIncrease);
            }
        }

    }
}
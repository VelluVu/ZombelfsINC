using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanvas : MonoBehaviour {

	
	
	
	void FixedUpdate () {
        gameObject.transform.LookAt(Camera.main.transform.position);
	}
}

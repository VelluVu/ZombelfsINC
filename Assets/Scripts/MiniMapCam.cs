using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCam : MonoBehaviour {

    Transform pPos;
	void Start () {
        pPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        gameObject.transform.position = pPos.position + new Vector3(0,50,0);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopDownCam : MonoBehaviour
{
    Transform pPos;

    private void Start()
    {
        pPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        gameObject.transform.position = pPos.position + new Vector3(0,20,0);
        //gameObject.transform.position = cursor.transform.position;
    }
}
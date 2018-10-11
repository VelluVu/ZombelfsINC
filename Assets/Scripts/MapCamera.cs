using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour {

    float camSpeed;
    float leftXBounds;
    float rightXBounds;

    private void Start()
    {
        camSpeed = 1f;
        leftXBounds = -4.2f;
        rightXBounds = 4.2f;
    }

    private void Update()
    {

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftXBounds, rightXBounds), 0,-10);
        transform.Translate(Input.GetAxis("Horizontal")*camSpeed*Time.deltaTime, 0, 0);
    }
}

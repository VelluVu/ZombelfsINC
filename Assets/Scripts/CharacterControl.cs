using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {

    float moveSpeed;
    float rotationSpeed;
    Rigidbody rb;
    Camera cam;

    private void Start()
    {
        moveSpeed = 1f;
        rotationSpeed = 5f;
        rb = gameObject.GetComponent<Rigidbody>();
        cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        CharacterMovement();
        CharacterDirection();
    }

    private void CharacterMovement()
    {
        float xMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float zMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(xMovement, 0, zMovement);
    }

    private void CharacterDirection()
    {
        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (plane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x,transform.position.y, pointToLook.z));
            
        }

    }
}

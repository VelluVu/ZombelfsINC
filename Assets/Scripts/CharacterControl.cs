using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {

    float moveSpeed;
    float jumpForce;
    float rotationSpeed;

    bool onGround;
    bool recentlyShot;

    public bool useController;
    

    Rigidbody rb;
    Camera cam;
    public Weapon axe;

    private void Start()
    {
        jumpForce = 500f;
        moveSpeed = 5f;
        rotationSpeed = 5f;
        rb = gameObject.GetComponent<Rigidbody>();
        cam = FindObjectOfType<Camera>();         
    }

    private void Update()
    {
        CharacterMovement();
        CharacterDirection();
        BasicAttack();
        Jump();
    }

    //Make character move with default horizontal and vertical buttons
    private void CharacterMovement()
    {
        float xMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float zMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(xMovement, 0, zMovement);
    }

    //Make the Character rotate at the mouse direction or controller
    private void CharacterDirection()
    {
        if (!useController)
        {
            Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (plane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));

            }
        }
        
        if (useController)
        {
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RHorizontal") + 
                Vector3.forward * -Input.GetAxisRaw("RVertical");
            if (playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }
           
        }

    }

   

    //Left Mouse click Attack
    private void BasicAttack()
    {
        if (!useController)
        {

            if (Input.GetMouseButtonDown(0) && !recentlyShot)
            {
                axe.SetAxeIsFiring(true);
                
            }
            if (Input.GetMouseButtonUp(0))
            {

                axe.SetAxeIsFiring(false);
                StartCoroutine(ShootCD());

                

            }
        }

        if (useController)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button5))
            {

                axe.SetAxeIsFiring(true);

              
            }
            if (Input.GetKeyUp(KeyCode.Joystick1Button5))
            {

                axe.SetAxeIsFiring(false);
                StartCoroutine(ShootCD());

                

            }
        }

    }

    // CD will disallow the spamhax
    IEnumerator ShootCD()
    {
        recentlyShot = true;
        
        yield return new WaitForSeconds(axe.GetShotInterval());

        recentlyShot = false;
      
    }

    //Makes Character jump with jump button "space"
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.AddForce(0,jumpForce * Time.deltaTime,0, ForceMode.Impulse);
        }
    }

    //Lots of GroundChecking to disallow spamhax jumping
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            onGround = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            onGround = true;
        }
    }
}

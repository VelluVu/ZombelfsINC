using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlingAxe : MonoBehaviour {

    float projectileSpeedz;
    float projectileSpeedy;
    float projectileRotationSpeed;
    float projectileDamage;
    float projectileLifeTime;

    public GameObject axeHitSound;
    public GameObject axeWhoosh;
    Rigidbody rb;

    bool hasCollided;

    public void SetProjectileSpeedz(float pSpeedz)
    {
        projectileSpeedz = pSpeedz;
    }

    public void SetProjectileSpeedy(float pSpeedy)
    {
        projectileSpeedy = pSpeedy;
    }

    public void SetProjectileDamage(float pDmg)
    {
        projectileDamage = pDmg;
    }

    public void SetProjectileRotationSpeed(float pRSpeed)
    {

        projectileRotationSpeed = pRSpeed;

    }

    public void SetProjectileLifeTime(float pLifeTime)
    {
        projectileLifeTime = pLifeTime;
    }

    private void Start()
    {
        hasCollided = false;
        rb = gameObject.GetComponent<Rigidbody>();  
        Destroy(gameObject, projectileLifeTime);
        rb.AddRelativeTorque(Vector3.right * projectileRotationSpeed, ForceMode.VelocityChange);
        rb.AddRelativeForce(Vector3.up * projectileSpeedy, ForceMode.Impulse);
        rb.AddRelativeForce(Vector3.forward * projectileSpeedz, ForceMode.Impulse);
        Destroy(Instantiate(axeWhoosh, transform.position, transform.rotation), 2f);
    }  

    private void OnCollisionEnter(Collision collision)
    {

        Destroy(Instantiate(axeHitSound),2f);

        if (collision.collider.tag == "Enemy" && collision.collider.tag != "Player")
        {
            if (!hasCollided)
            {
                hasCollided = true;

                collision.collider.gameObject.GetComponent<EnemyBase>().EnemyTakeDamage(projectileDamage, false);

                FixedJoint fj = new FixedJoint();
                fj = gameObject.AddComponent<FixedJoint>();
                fj.connectedBody = collision.collider.attachedRigidbody;

                Destroy(gameObject, 2f);
            }
            StartCoroutine(ResetCollision());
     
        }

       
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
    }

    IEnumerator ResetCollision()
    {
        yield return new WaitForSeconds(1f);
        hasCollided = false;
    }


}


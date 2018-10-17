using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    float projectileSpeedz;
    float projectileSpeedy;
    float projectileRotationSpeed;
    float projectileDamage;
    float projectileLifeTime;

    Rigidbody rb;

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
        rb = gameObject.GetComponent<Rigidbody>();  
        
        Destroy(gameObject, projectileLifeTime);
        rb.AddRelativeTorque(Vector3.right * projectileRotationSpeed, ForceMode.VelocityChange);
        rb.AddRelativeForce(Vector3.up * projectileSpeedy, ForceMode.Impulse);
        rb.AddRelativeForce(Vector3.forward * projectileSpeedz, ForceMode.Impulse);
    }  

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy" && collision.collider.tag != "Player")
        {

            collision.collider.gameObject.GetComponent<Enemy>().EnemyTakeDamage(projectileDamage);

            FixedJoint fj = new FixedJoint();
            fj = gameObject.AddComponent<FixedJoint>();
            fj.connectedBody = collision.collider.attachedRigidbody;
      
            Destroy(gameObject, 2f);
     
        }

        
    }


}


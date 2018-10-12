using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    float projectileSpeed;
    float projectileRotationSpeed;
    float projectileDamage;
    float projectileLifeTime;

    public GameObject stuckProjectile;
    Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        projectileSpeed = 5f;
        projectileDamage = 20f;
        projectileRotationSpeed = 5f;
        projectileLifeTime = 2f;
        Destroy(gameObject, projectileLifeTime);
    }

    public void SetProjectileSpeed(float pSpeed)
    {
        projectileSpeed = pSpeed;
    }

    public void SetProjectileDamage(float pDmg)
    {
        projectileDamage = pDmg;
    }

    public void SetProjectileRotationSpeed(float pRSpeed) {

        projectileRotationSpeed = pRSpeed;

    }

    public void SetProjectileLifeTime(float pLifeTime)
    {
        projectileLifeTime = pLifeTime;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        //transform.Rotate(Vector3.right * projectileRotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            collision.collider.gameObject.GetComponent<Enemy>().EnemyTakeDamage(projectileDamage);
            
            Destroy(gameObject);
        }
    }


}


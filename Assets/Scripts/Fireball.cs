using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    float projectileSpeedz;
    float projectileSpeedy;
    float projectileRotationSpeed;
    float projectileAreaDamage;
    float projectileLifeTime;
    float projectileAreaRadius;
    float statusTickRate = 1f;
    float statusDmgMultiplier = 0.5f;
    float prefabLifeTime = 3f;
    int statusDuration = 5;
    bool hasCollided;
    Rigidbody rb;
    public GameObject explosion;
    public GameObject fireBallSound;
    public GameObject fireBallhitSound;

    public void SetProjectileAreaDamage(float pArea)
    {

        projectileAreaDamage = pArea;

    }

    public void SetProjectileSpeedz(float pSpeedz)
    {

        projectileSpeedz = pSpeedz;

    }

    public void SetProjectileSpeedy(float pSpeedy)
    {

        projectileSpeedy = pSpeedy;

    }

    public void SetProjectileRotationSpeed(float pRSpeed)
    {

        projectileRotationSpeed = pRSpeed;

    }

    public void SetProjectileLifeTime(float pLifeTime)
    {
        projectileLifeTime = pLifeTime;
    }

    public void SetProjectileAreaRadius(float pRadius)
    {
        projectileAreaRadius = pRadius;
    }

    private void Start()
    {
        hasCollided = false;
        rb = gameObject.GetComponent<Rigidbody>();
        Destroy(gameObject, projectileLifeTime);
        rb.AddRelativeTorque(Vector3.right * projectileRotationSpeed, ForceMode.VelocityChange);
        rb.AddRelativeForce(Vector3.up * projectileSpeedy, ForceMode.Impulse);
        rb.AddRelativeForce(Vector3.forward * projectileSpeedz, ForceMode.Impulse);
        Destroy(Instantiate(fireBallSound, transform.position, transform.rotation), prefabLifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy" && collision.collider.tag != "Player")
        {
            if (!hasCollided)
            {
                hasCollided = true;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponentInChildren<ParticleSystem>().Stop();
                StartCoroutine(FireBallHit(collision));
                
            }
            StartCoroutine(ResetCollision());
        }
    }

    IEnumerator ResetCollision()
    {
        yield return new WaitForSeconds(1f);
        hasCollided = false;
    }

    IEnumerator FireBallHit(Collision collision)
    {

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        Destroy(Instantiate(fireBallhitSound, transform.position, transform.rotation), prefabLifeTime);

        Destroy(Instantiate(explosion, transform.position, transform.rotation), prefabLifeTime);
        if (collision.collider.gameObject.GetComponent<EnemyBase>() != null)
        {
            StartCoroutine(collision.collider.gameObject.GetComponent<EnemyBase>().EnemyStatusStart(statusDuration, Mathf.RoundToInt(projectileAreaDamage * statusDmgMultiplier), statusTickRate));
        }
       
        ExplosionDamage(transform.position, projectileAreaRadius);

        yield return new WaitForSeconds(statusDuration);

        Destroy(gameObject);

    }

    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].CompareTag("Enemy"))
            {
                hitColliders[i].gameObject.GetComponent<EnemyBase>().EnemyTakeDamage(projectileAreaDamage, false);               
            }
           
            i++;
        }
    }
}
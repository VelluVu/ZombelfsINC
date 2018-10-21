using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : MonoBehaviour {

    float projectileSpeedz;
    float projectileSpeedy;
    float projectileRotationSpeed;
    float projectileDamage;
    float projectileLifeTime;
    float projectileCriticalChange;
    float maxRoll;

    bool isCritical;
    Rigidbody rb;
    public GameObject swordHitSound;

    public void SetProjectileSpeedz(float pSpeedz)
    {
        projectileSpeedz = pSpeedz;
    }

    public void SetProjectileSpeedy(float pSpeedy)
    {
        projectileSpeedy = pSpeedy;
    }

    public void SetProjectileDamage(float pDmg, float pCritC, float maxR)
    {
        projectileDamage = pDmg;
        projectileCriticalChange = pCritC;
        maxRoll = maxR;
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
        rb.AddRelativeTorque(Vector3.forward * projectileRotationSpeed, ForceMode.VelocityChange);
        rb.AddRelativeForce(Vector3.up * projectileSpeedy, ForceMode.Impulse);
        rb.AddRelativeForce(Vector3.forward * projectileSpeedz, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {

        Destroy(Instantiate(swordHitSound), 2f);

        if (collision.collider.tag == "Enemy" && collision.collider.tag != "Player")
        {

            collision.collider.gameObject.GetComponent<Enemy>().EnemyTakeDamage(ChanceToCrit(projectileDamage), isCritical);

            FixedJoint fj = new FixedJoint();
            fj = gameObject.AddComponent<FixedJoint>();
            fj.connectedBody = collision.collider.attachedRigidbody;

            Destroy(gameObject, 2f);

        }

        gameObject.GetComponent<CapsuleCollider>().enabled = false;


    }

    float ChanceToCrit(float projectileDamage)
    {

        isCritical = false;

        if (Random.Range(0, maxRoll) >= maxRoll * projectileCriticalChange)
        {
            isCritical = true;
        } else
        {
            isCritical = false;
        }

        if (isCritical)
        {

            projectileDamage += Random.Range(maxRoll * projectileCriticalChange, projectileDamage);

        }


        return projectileDamage;
    }

}

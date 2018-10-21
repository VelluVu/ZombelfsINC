using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    bool isFiring;

    public float projectileSpeedz = 3f;
    public float projectileSpeedy = 0.2f;
    public float projectileRotationSpeed = 10f;
    public float projectileDamage = 50f;
    public float projectileLifeTime = 8f;
    public float shotInterval = 5f;
    public float shotCounter = 1f;
    public float meleeDamage = 10f;

    public float criticalChance = 0.25f;
    public float maxRoll = 100f;

    public SwordProjectile swordProjectile;
    public Transform firePoint;  

    public void SetSwordIsFiring(bool firing)
    {
        isFiring = firing;
    }

    public float GetShotInterval()
    {
        return shotInterval;
    }

    private void Update()
    {
        UseWeapon();
    }

    void UseWeapon()
    {
        if (isFiring)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = shotInterval;
                SwordProjectile newSwordProjectile = Instantiate(swordProjectile, firePoint.position, firePoint.rotation) as SwordProjectile;
                Debug.Log(swordProjectile + "Thrown");
                FindObjectOfType<SwordSkillImage>().SetSwordCD(shotInterval);
                newSwordProjectile.SetProjectileSpeedz(projectileSpeedz);
                newSwordProjectile.SetProjectileRotationSpeed(projectileRotationSpeed);
                newSwordProjectile.SetProjectileDamage(projectileDamage, criticalChance, maxRoll);
                newSwordProjectile.SetProjectileLifeTime(projectileLifeTime);
                newSwordProjectile.SetProjectileSpeedy(projectileSpeedy);
            }
        }
        else
        {
            shotCounter = 0;       
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().EnemyTakeDamage(meleeDamage);
        }
    }

    
}

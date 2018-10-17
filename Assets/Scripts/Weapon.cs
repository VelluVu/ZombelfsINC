using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    bool isFiring;

    public float projectileSpeedz = 25f;
    public float projectileSpeedy = 2f;
    public float projectileRotationSpeed = 0.6f;
    public float projectileDamage = 8f;
    public float projectileLifeTime = 4f;
    public float shotInterval = 1f;
    public float shotCounter = 1f;

    public Projectile projectile;
    public Transform firePoint;
 

    public void SetAxeIsFiring(bool firing)
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
                Projectile newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation) as Projectile;
                Debug.Log(projectile + "Thrown");
                newProjectile.SetProjectileSpeedz(projectileSpeedz);
                newProjectile.SetProjectileRotationSpeed(projectileRotationSpeed);
                newProjectile.SetProjectileDamage(projectileDamage);
                newProjectile.SetProjectileLifeTime(projectileLifeTime);
                newProjectile.SetProjectileSpeedy(projectileSpeedy);
            }
        }
        else
        {
            shotCounter = 0;
        }
    }
}

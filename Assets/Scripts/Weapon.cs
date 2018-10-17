using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    bool isFiring;

    float projectileSpeedz;
    float projectileSpeedy;
    float projectileRotationSpeed;
    float projectileDamage;
    float projectileLifeTime;
    float shotInterval;
    float shotCounter;

    public Projectile projectile;
    public Transform firePoint;

    private void Start()
    {
        projectileDamage = 25f;
        projectileSpeedz = 2f;
        projectileSpeedy = 0.6f;
        projectileRotationSpeed = 8f;
        projectileLifeTime = 4f;
        shotInterval = 1f;
    }

    public void SetIsFiring(bool firing)
    {
        isFiring = firing;
    }

    public float GetShotInterval()
    {
        return shotInterval;
    }

    private void Update()
    {
        if(isFiring)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = shotInterval;
                Projectile newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation) as Projectile;
                newProjectile.SetProjectileSpeedz(projectileSpeedz);
                newProjectile.SetProjectileRotationSpeed(projectileRotationSpeed);
                newProjectile.SetProjectileDamage(projectileDamage);
                newProjectile.SetProjectileLifeTime(projectileLifeTime);
                newProjectile.SetProjectileSpeedy(projectileSpeedy);
            }
        } else
        {
            shotCounter = 0;
        }
    }
}

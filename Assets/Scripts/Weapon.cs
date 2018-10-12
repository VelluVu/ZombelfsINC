using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    bool isFiring;

    float projectileSpeed;
    float projectileRotationSpeed;
    float projectileDamage;
    float projectileLifeTime;
    float shotInterval;
    float shotCounter;

    public Projectile projectile;
    public Transform firePoint;

    private void Start()
    {
        projectileDamage = 20f;
        projectileSpeed = 5f;
        projectileRotationSpeed = 1f;
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
                newProjectile.SetProjectileSpeed(projectileSpeed);
                newProjectile.SetProjectileRotationSpeed(projectileRotationSpeed);
                newProjectile.SetProjectileDamage(projectileDamage);
                newProjectile.SetProjectileLifeTime(projectileLifeTime);
            }
        } else
        {
            shotCounter = 0;
        }
    }
}

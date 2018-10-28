using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour {

    bool isFiring;
    bool axeChanged;

    public float projectileSpeedz;
    public float projectileSpeedy;
    public float projectileRotationSpeed;
    public float projectileDamage;
    public float projectileLifeTime;
    public float shotInterval;
    public float shotCounter;

    public WhirlingAxe projectile;
    public Transform firePoint;
    public Equipment axe;

    public void SetAxeIsFiring(bool firing)
    {
        isFiring = firing;
    }

    public void InitializeAxe()
    {
        projectileSpeedz = axe.projectileSpeedz;
        projectileSpeedy = axe.projectileSpeedy;
        projectileRotationSpeed = axe.projectileRotationSpeed;
        projectileDamage = axe.projectileDamage;
        projectileLifeTime = axe.projectileLifeTime;
        shotInterval = axe.shotInterval;
    }

    public void ChangeAxe(Equipment newAxe)
    {
        axe = newAxe;
        axeChanged = true;
    }

    public float GetShotInterval()
    {
        return shotInterval;
    }

    private void Update()
    {
        UseWeapon();
        
        if (axeChanged)
        {
            InitializeAxe();
            axeChanged = false;
        }    
    }

    void UseWeapon()
    {
        if (isFiring)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = shotInterval;
                WhirlingAxe newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation) as WhirlingAxe;
                Debug.Log(projectile + "Thrown");
                FindObjectOfType<AxeSkillImage>().SetAxeCD(shotInterval);
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

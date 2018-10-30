using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour {

    bool isFiring;
    bool axeChanged;

    public float[] axeStats = new float[10];
    public float[] saveAxeStats = new float[10];

    public float projectileLifeTime;
    public float shotCounter = 1;
    public float maxRoll = 100;

    public WhirlingAxe projectile;
    public Transform firePoint;
    public Equipment axe;

    private void Start()
    {
        axeStats = new float[10];
        saveAxeStats = new float[10];
        InitializeAxe();
    }

    public void SetAxeIsFiring(bool firing)
    {
        isFiring = firing;
    }

    public void InitializeAxe()
    {
        axeStats[0] = axe.projectileSpeedz;
        axeStats[1] = axe.projectileSpeedy;
        axeStats[2] = axe.projectileRotationSpeed;
        axeStats[3] = axe.projectileDamage;
        axeStats[4] = axe.projectileAreaDamage;
        axeStats[5] = axe.shotInterval;
        axeStats[6] = axe.spellCost;
        axeStats[7] = axe.projectileAreaRadius;
        axeStats[8] = axe.meleeDamage;
        axeStats[9] = axe.criticalChance;

        for (int i = 0; i < axeStats.Length; i++)
        {
            saveAxeStats[i] = axeStats[i];
        }
    }

    public void LoadAxeStats()
    {
        for (int i = 0; i < saveAxeStats.Length; i++)
        {
            axeStats[i] = saveAxeStats[i];
        }
    }
    
    public void ChangeAxe(Equipment newAxe)
    {
        axe = newAxe;
        axeChanged = true;
    }

    public float GetShotInterval()
    {       
        return axeStats[5];
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
                shotCounter = axeStats[5];
                WhirlingAxe newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation) as WhirlingAxe;
                Debug.Log(projectile + "Thrown");
                FindObjectOfType<AxeSkillImage>().SetAxeCD(axeStats[5]);
                newProjectile.SetProjectileSpeedz(axeStats[0]);
                newProjectile.SetProjectileRotationSpeed(axeStats[2]);
                newProjectile.SetProjectileDamage(axeStats[3]);
                newProjectile.SetProjectileLifeTime(projectileLifeTime);
                newProjectile.SetProjectileSpeedy(axeStats[1]);
            }
        }
        else
        {
            shotCounter = 0;          
        }
    }
}

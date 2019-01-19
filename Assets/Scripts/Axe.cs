using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Axe : MonoBehaviour
{

    bool axeChanged;
    public bool axeMultiShot;
    public int axeShotgunAmount;
    public float axeConeSize;

    public float[] axeStats;
    public static float[] saveAxeStats;

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
            //Debug.Log("SAVED AXESTATS: " + saveAxeStats[i]);
        }

        AxeOnPointAdd(CharacterStats.strength * CharacterStats.bonusMulti);
    }

    public void LoadAxeStats()
    {
        for (int i = 0; i < axeStats.Length; i++)
        {
            if (axeStats[i] != saveAxeStats[i])
            {
                axeStats[i] = saveAxeStats[i];
            }
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

        if (axeChanged)
        {
            InitializeAxe();
            
            axeChanged = false;
        }
    }

    public void UseAxeWeapon()
    {
        WhirlingAxe newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation) as WhirlingAxe;
        FindObjectOfType<AxeSkillImage>().SetAxeCD(axeStats[5]);
        newProjectile.SetProjectileSpeedz(axeStats[0]);
        newProjectile.SetProjectileSpeedy(axeStats[1]);
        newProjectile.SetProjectileRotationSpeed(axeStats[2]);
        newProjectile.SetProjectileDamage(axeStats[3]);
        newProjectile.SetProjectileLifeTime(projectileLifeTime);
        

        if (axeMultiShot)
        {

            for (int i = 0; i < axeShotgunAmount; i++)
            {
                float random = Random.Range(-axeShotgunAmount, axeShotgunAmount);
                Vector3 spread = new Vector3(0, random, 0).normalized * axeConeSize;
                Quaternion projectileDirection = Quaternion.Euler(spread) * firePoint.rotation;
                WhirlingAxe axeShotGunProjectile = Instantiate(projectile, firePoint.position, projectileDirection) as WhirlingAxe;

                axeShotGunProjectile.SetProjectileSpeedz(axeStats[0]);
                axeShotGunProjectile.SetProjectileSpeedy(axeStats[1]);
                axeShotGunProjectile.SetProjectileRotationSpeed(axeStats[2]);
                axeShotGunProjectile.SetProjectileDamage(axeStats[3]);
                axeShotGunProjectile.SetProjectileLifeTime(projectileLifeTime);
                
            }
        }
    }

    public void AxeOnPointAdd(float lvlMult)
    {
        
                saveAxeStats[3] += saveAxeStats[3] * lvlMult;
                axeStats[3] += axeStats[3] * lvlMult;
         
    }

    internal void AxeOnPointRemove(float v)
    {
        
                saveAxeStats[3] -= saveAxeStats[3] * v;
                axeStats[3] -= axeStats[3] * v;
         
    }
}

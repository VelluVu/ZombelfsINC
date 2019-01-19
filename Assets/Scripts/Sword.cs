using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sword : MonoBehaviour {

    bool changeSword;
    public bool swordMultiShot;
    public int swordShotgunAmount;
    public float swordConeSize;

    public float[] swordStats;
    public static float[] saveSwordStats;

    public float _projectileLifeTime;
    public float _shotCounter = 1;
    public float maxRoll = 100;

    public SwordProjectile swordProjectile;
    public Transform firePoint;
    public Equipment sword;
    
    private void Start()
    {
        swordStats = new float[10];
        saveSwordStats = new float[10];
        InitializeSword();
    }

    public void InitializeSword()
    {
        swordStats[0] = sword.projectileSpeedz;
        swordStats[1] = sword.projectileSpeedy;
        swordStats[2] = sword.projectileRotationSpeed;
        swordStats[3] = sword.projectileDamage;
        swordStats[4] = sword.projectileAreaDamage;
        swordStats[5] = sword.shotInterval;
        swordStats[6] = sword.spellCost;
        swordStats[7] = sword.projectileAreaRadius;
        swordStats[8] = sword.meleeDamage;
        swordStats[9] = sword.criticalChance;

        for (int i = 0; i < swordStats.Length; i++)
        {
            saveSwordStats[i] = swordStats[i];
        }

        SwordOnPointAdd(CharacterStats.dexterity * CharacterStats.bonusMulti);

    }

    public void LoadSwordStats()
    {
        for (int i = 0; i < swordStats.Length; i++)
        {
            if (swordStats[i] != saveSwordStats[i])
            {
                swordStats[i] = saveSwordStats[i];
            }
        }
    }

    public void SwordChange(Equipment newWeapon)
    {
        sword = newWeapon;
        changeSword = true;
    }

    public float GetShotInterval()
    {
        return swordStats[5];
    }

    private void Update()
    {
      
        if (changeSword)
        {
            InitializeSword();
            changeSword = false;
        }
        
    }

    public void UseSwordWeapon()
    {
    
        SwordProjectile newSwordProjectile = Instantiate(swordProjectile, firePoint.position, firePoint.rotation) as SwordProjectile;
        FindObjectOfType<SwordSkillImage>().SetSwordCD(swordStats[5]);
        newSwordProjectile.SetProjectileSpeedz(swordStats[0]);
        newSwordProjectile.SetProjectileSpeedy(swordStats[1]);
        newSwordProjectile.SetProjectileRotationSpeed(swordStats[2]);
        newSwordProjectile.SetProjectileDamage(swordStats[3], swordStats[9], maxRoll);
        newSwordProjectile.SetProjectileLifeTime(_projectileLifeTime);
        

        if (swordMultiShot)
        {

            for (int i = 0; i < swordShotgunAmount; i++)
            {
                float random = Random.Range(-swordShotgunAmount, swordShotgunAmount);
                Vector3 spread = new Vector3(0, random, 0).normalized * swordConeSize;
                Quaternion projectileDirection = Quaternion.Euler(spread) * firePoint.rotation;
                SwordProjectile swordShotGunProjectile = Instantiate(swordProjectile, firePoint.position, projectileDirection) as SwordProjectile;

                swordShotGunProjectile.SetProjectileSpeedz(swordStats[0]);
                swordShotGunProjectile.SetProjectileSpeedy(swordStats[1]);
                swordShotGunProjectile.SetProjectileRotationSpeed(swordStats[2]);
                swordShotGunProjectile.SetProjectileDamage(swordStats[3], swordStats[9], maxRoll);
                swordShotGunProjectile.SetProjectileLifeTime(_projectileLifeTime);
                

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (other.GetComponent<CapsuleCollider>())
                other.GetComponent<EnemyBase>().EnemyTakeDamage(swordStats[8], false);
        }
       
    }  

    public void SwordOnPointAdd(float lvlMult)
    {
        
                saveSwordStats[3] += saveSwordStats[3] * lvlMult;
                swordStats[3] += swordStats[3] * lvlMult;
         
    }

    internal void SwordOnPointRemove(float v)
    {
        
                saveSwordStats[3] -= saveSwordStats[3] * v;
                swordStats[3] -= swordStats[3] * v;
        
    }
}
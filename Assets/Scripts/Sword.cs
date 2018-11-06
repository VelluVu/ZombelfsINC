using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    bool isFiring;
    bool changeSword;

    public float[] swordStats;
    public float[] saveSwordStats;

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
    }

    public void LoadSwordStats()
    {
        for (int i = 0; i < swordStats.Length; i++)
        {
            swordStats[i] = saveSwordStats[i];
        }
    }

    public void SetSwordIsFiring(bool firing)
    {
        isFiring = firing;
        
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
        UseWeapon();
        if (changeSword)
        {
            InitializeSword();
            changeSword = false;
        }
        
    }

    void UseWeapon()
    {
        if (isFiring)
        {
            _shotCounter -= Time.deltaTime;
            if (_shotCounter <= 0)
            {

                
                _shotCounter = swordStats[5];
                SwordProjectile newSwordProjectile = Instantiate(swordProjectile, firePoint.position, firePoint.rotation) as SwordProjectile;
                Debug.Log(swordProjectile + "Thrown");
                FindObjectOfType<SwordSkillImage>().SetSwordCD(swordStats[5]);
                newSwordProjectile.SetProjectileSpeedz(swordStats[0]);
                newSwordProjectile.SetProjectileRotationSpeed(swordStats[2]);
                newSwordProjectile.SetProjectileDamage(swordStats[3], swordStats[9], maxRoll);
                newSwordProjectile.SetProjectileLifeTime(_projectileLifeTime);
                newSwordProjectile.SetProjectileSpeedy(swordStats[1]);
            }
        }
        else
        {
            _shotCounter = 0;       
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

    public void SwordOnLevelUp(float lvlMult)
    {
        for (int i = 0; i < swordStats.Length; i++)
        {
            saveSwordStats[i] += saveSwordStats[i] * lvlMult;
            swordStats[i] += swordStats[i] * lvlMult;
        }
    }

    public void SwordStoredLevelUp(List<float> storedLvlMods)
    {

        Debug.Log("ADDED STORED STUFF TO SWORD!");

        for (int i = 0; i < swordStats.Length; i++)
        {

            foreach (float value in storedLvlMods)
            {
                saveSwordStats[i] += saveSwordStats[i] * value;
            }
            foreach (float value in storedLvlMods)
            {
                swordStats[i] += swordStats[i] * value;
            }

        }
    }
}

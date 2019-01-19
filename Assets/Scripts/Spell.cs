using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spell : MonoBehaviour {

    bool spellChanged;

    public bool spellMultiShot;
    public int spellShotgunAmount;
    public float spellConeSize;

    public float[] spellStats;
    public static float[] saveSpellStats;

    public float projectileLifeTime;
    public float shotCounter;

    public Fireball fireballSpell;
    //List of spells...
    public Transform firePoint;
    public Equipment spell;

    private void Start()
    {
        spellStats = new float[10];
        saveSpellStats = new float[10];
        InitializeSpell();
    }

    public void InitializeSpell()
    {
        spellStats[0] = spell.projectileSpeedz;
        spellStats[1] = spell.projectileSpeedy;
        spellStats[2] = spell.projectileRotationSpeed;
        spellStats[3] = spell.projectileDamage;
        spellStats[4] = spell.projectileAreaDamage;
        spellStats[5] = spell.shotInterval;
        spellStats[6] = spell.spellCost;
        spellStats[7] = spell.projectileAreaRadius;
        spellStats[8] = spell.meleeDamage;
        spellStats[9] = spell.criticalChance;

        for (int i = 0; i < spellStats.Length; i++)
        {
            saveSpellStats[i] = spellStats[i];
        }

        SpellOnPointAdd(CharacterStats.bonusMulti * CharacterStats.energy);

    }

    public void LoadSpellStats()
    {
        for (int i = 0; i < spellStats.Length; i++)
        {
            if (spellStats[i] != saveSpellStats[i])
            {
                spellStats[i] = saveSpellStats[i];
            }
        }
    }

    public void ChangeSpell(Equipment newSpell)
    {
        spell = newSpell;
        spellChanged = true;
    }

    public float GetShotInterval()
    {
        return spellStats[5];
    }

    private void Update()
    {
       
        if (spellChanged)
        {
            InitializeSpell();
            spellChanged = false;
        }    
    }

    public void UseSpellWeapon()
    {

        Fireball newSpellProjectile = Instantiate(fireballSpell, firePoint.position, firePoint.rotation) as Fireball;
        FindObjectOfType<CharacterControl>().PlayerLoseMana(spellStats[6]);
        FindObjectOfType<SpellSkillImage>().SetSpellCD(spellStats[5]);

        newSpellProjectile.SetProjectileSpeedz(spellStats[0]);
        newSpellProjectile.SetProjectileSpeedy(spellStats[1]);
        newSpellProjectile.SetProjectileRotationSpeed(spellStats[2]);
        newSpellProjectile.SetProjectileAreaDamage(spellStats[4]);
        newSpellProjectile.SetProjectileAreaRadius(spellStats[7]);
        newSpellProjectile.SetProjectileLifeTime(projectileLifeTime);
    
        if (spellMultiShot) {

            for (int i = 0; i < spellShotgunAmount; i++)
            {
                float random = Random.Range(-spellShotgunAmount, spellShotgunAmount);
                Vector3 spread = new Vector3(0, random, 0).normalized * spellConeSize;
                Quaternion projectileDirection = Quaternion.Euler(spread) * firePoint.rotation;
                Fireball spellShotGunProjectile = Instantiate(fireballSpell, firePoint.position, projectileDirection) as Fireball;
     
                spellShotGunProjectile.SetProjectileSpeedz(spellStats[0]);
                spellShotGunProjectile.SetProjectileSpeedy(spellStats[1]);
                spellShotGunProjectile.SetProjectileRotationSpeed(spellStats[2]);                 
                spellShotGunProjectile.SetProjectileAreaDamage(spellStats[4]);
                spellShotGunProjectile.SetProjectileAreaRadius(spellStats[7]);
                spellShotGunProjectile.SetProjectileLifeTime(projectileLifeTime);

            }
        }
    }

    internal void SpellOnPointAdd(float lvlMult)
    {
        
                saveSpellStats[4] += Mathf.Sqrt(saveSpellStats[4] * lvlMult);
                spellStats[4] += Mathf.Sqrt(spellStats[4] * lvlMult);
                
    }

    internal void SpellOnPointRemove(float v)
    {
       
                saveSpellStats[4] -= Mathf.Sqrt(saveSpellStats[4] * v);
                spellStats[4] -= Mathf.Sqrt(spellStats[4] * v);
        
    }
}
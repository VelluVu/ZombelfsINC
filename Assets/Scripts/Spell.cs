using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {

    bool isFiring;
    bool spellChanged;

    public float[] spellStats;
    public float[] saveSpellStats;

    public float projectileLifeTime;
    public float shotCounter;

    public Fireball fireballSpell;
    public Transform firePoint;
    public Equipment spell;

    private void Start()
    {
        spellStats = new float[10];
        saveSpellStats = new float[10];
        InitializeSpell();
    }

    public void PowerUp(float[] mods)
    {
        
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
    }

    public void LoadSpellStats()
    {
        for (int i = 0; i < spellStats.Length; i++)
        {
            spellStats[i] = saveSpellStats[i];
        }
    }

    public void ChangeSpell(Equipment newSpell)
    {
        spell = newSpell;
        spellChanged = true;
    }

    public void SetSpellIsFiring(bool firing)
    {
        isFiring = firing;
    }

    public float GetShotInterval()
    {
        return spellStats[5];
    }

    private void Update()
    {
        UseWeapon();
        if (spellChanged)
        {
            InitializeSpell();
            spellChanged = false;
        }    
    }

    void UseWeapon()
    {
        if (isFiring)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0 && FindObjectOfType<CharacterControl>().currentMana >= spellStats[6])
            {
                shotCounter = spellStats[5];
                Fireball newSpellProjectile = Instantiate(fireballSpell, firePoint.position, firePoint.rotation) as Fireball;
                Debug.Log(fireballSpell + "Thrown");
                FindObjectOfType<CharacterControl>().PlayerLoseMana(spellStats[6]);
                FindObjectOfType<SpellSkillImage>().SetSpellCD(spellStats[5]);
                newSpellProjectile.SetProjectileSpeedz(spellStats[0]);
                newSpellProjectile.SetProjectileRotationSpeed(spellStats[2]);             
                newSpellProjectile.SetProjectileLifeTime(projectileLifeTime);
                newSpellProjectile.SetProjectileSpeedy(spellStats[1]);
                newSpellProjectile.SetProjectileAreaDamage(spellStats[4]);
                newSpellProjectile.SetProjectileAreaRadius(spellStats[7]);
            }
        }
        else
        {
            shotCounter = 0;          
        }
    }
}

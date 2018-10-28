using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {

    bool isFiring;
    bool spellChanged;

    public float projectileSpeedz;
    public float projectileSpeedy;
    public float projectileRotationSpeed;
    public float projectileAreaDamage;
    public float projectileLifeTime;
    public float shotInterval;
    public float shotCounter;
    public float spellCost;
    public float projectileAreaRadius;

    public Fireball fireballSpell;
    public Transform firePoint;
    public Equipment spell;

    public void InitializeSpell()
    {
        projectileSpeedz = spell.projectileSpeedz;
        projectileSpeedy = spell.projectileSpeedy;
        projectileRotationSpeed = spell.projectileRotationSpeed;
        projectileLifeTime = spell.projectileLifeTime;
        projectileAreaDamage = spell.projectileAreaDamage;
        projectileAreaRadius = spell.projectileAreaRadius;
        shotInterval = spell.shotInterval;
        spellCost = spell.spellCost;
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
        return shotInterval;
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
            if (shotCounter <= 0 && FindObjectOfType<CharacterControl>().GetCurrentMana() >= 25)
            {
                shotCounter = shotInterval;
                Fireball newSpellProjectile = Instantiate(fireballSpell, firePoint.position, firePoint.rotation) as Fireball;
                Debug.Log(fireballSpell + "Thrown");
                FindObjectOfType<CharacterControl>().PlayerLoseMana(spellCost);
                FindObjectOfType<SpellSkillImage>().SetSpellCD(shotInterval);
                newSpellProjectile.SetProjectileSpeedz(projectileSpeedz);
                newSpellProjectile.SetProjectileRotationSpeed(projectileRotationSpeed);             
                newSpellProjectile.SetProjectileLifeTime(projectileLifeTime);
                newSpellProjectile.SetProjectileSpeedy(projectileSpeedy);
                newSpellProjectile.SetProjectileAreaDamage(projectileAreaDamage);
                newSpellProjectile.SetProjectileAreaRadius(projectileAreaRadius);
            }
        }
        else
        {
            shotCounter = 0;          
        }
    }
}

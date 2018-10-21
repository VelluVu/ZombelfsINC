using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {

    bool isFiring;

    public float projectileSpeedz = 2f;
    public float projectileSpeedy = 0f;
    public float projectileRotationSpeed = 0f;
    public float projectileDamage = 75f;
    public float projectileAreaDamage = 25f;
    public float projectileLifeTime = 8f;
    public float shotInterval = 8f;
    public float shotCounter = 1f;
    public float spellCost = 25f;
    public float projectileAreaRadius = 5f;

    public Fireball fireballSpell;
    public Transform firePoint;

    public void ResetSpell()
    {
        projectileSpeedz = 3f;
        projectileSpeedy = 0f;
        projectileRotationSpeed = 0f;
        projectileDamage = 100f;
        projectileLifeTime = 8f;
        shotInterval = 8f;
        shotCounter = 1f;
        spellCost = 25f;
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
                newSpellProjectile.SetProjectileDamage(projectileDamage);
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

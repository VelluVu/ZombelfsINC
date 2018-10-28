using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    bool isFiring;
    bool changeSword;

    public float _projectileSpeedz;
    public float _projectileSpeedy;
    public float _projectileRotationSpeed;
    public float _projectileDamage;
    public float _projectileLifeTime;
    public float _shotInterval; 
    public float _meleeDamage;
    public float _criticalChance;
    public float _shotCounter = 1;
    public float maxRoll = 100;

    public SwordProjectile swordProjectile;
    public Transform firePoint;
    public Equipment sword;

    public void InitializeSword()
    {
        _projectileSpeedz = sword.projectileSpeedz;
        _projectileSpeedy = sword.projectileSpeedy;
        _projectileRotationSpeed = sword.projectileRotationSpeed;
        _projectileDamage = sword.projectileLifeTime;
        _projectileLifeTime = sword.projectileLifeTime;
        _shotInterval = sword.shotInterval;
        _meleeDamage = sword.meleeDamage;
        _criticalChance = sword.criticalChance;
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
        return _shotInterval;
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

                
                _shotCounter = _shotInterval;
                SwordProjectile newSwordProjectile = Instantiate(swordProjectile, firePoint.position, firePoint.rotation) as SwordProjectile;
                Debug.Log(swordProjectile + "Thrown");
                FindObjectOfType<SwordSkillImage>().SetSwordCD(_shotInterval);
                newSwordProjectile.SetProjectileSpeedz(_projectileSpeedz);
                newSwordProjectile.SetProjectileRotationSpeed(_projectileRotationSpeed);
                newSwordProjectile.SetProjectileDamage(_projectileDamage, _criticalChance, maxRoll);
                newSwordProjectile.SetProjectileLifeTime(_projectileLifeTime);
                newSwordProjectile.SetProjectileSpeedy(_projectileSpeedy);
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
                other.GetComponent<EnemyBase>().EnemyTakeDamage(_meleeDamage, false);
        }
       
    }  
}

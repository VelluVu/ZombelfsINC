using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentSlot equipSlot;
    
    public float projectileSpeedz;
    public float projectileSpeedy;
    public float projectileRotationSpeed;
    public float projectileDamage;
    public float projectileLifeTime;
    public float projectileAreaRadius;   
    public float projectileAreaDamage;
    public float spellCost;
    public float shotInterval;
    public float meleeDamage;
    public float criticalChance;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
       
       
    }

}

public enum EquipmentSlot { Weapon1, Weapon2, Weapon3 }



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : GlobalStats {
   
    public float maxMana;
    public float replenishM;
    public float BackwardsMoveSpeed;
    public float diagonalMovementSpeed;
    public float jumpForce;

    float multiplier = 0.1f;

    public int currentCharacterXP;
    public int currentCharacterLevel;
    
    public float replenishH;

    public GameObject lvlUpEff;

    public void UpdateXp(int exp)
    {
        currentCharacterXP += exp;

        int lvl = (int)(0.1f * Mathf.Sqrt(currentCharacterXP));

        if (lvl != currentCharacterLevel)
        {

            currentCharacterLevel = lvl;

            Destroy(Instantiate(lvlUpEff, transform.position, Quaternion.identity),3f);
            LevelUpBonus();
        }

        int xpToNext = 100 * (currentCharacterLevel + 1) * (currentCharacterLevel + 1);
        int difXp = xpToNext - currentCharacterXP;

        int totalDif = xpToNext - (100 * currentCharacterLevel * currentCharacterLevel);

    }

    public void LevelUpBonus()
    {
        maxMana += maxMana * (multiplier*currentCharacterLevel);
        replenishM += replenishM * (multiplier*currentCharacterLevel);
        replenishH += replenishH * (multiplier*currentCharacterLevel);

        if (FindObjectOfType<Axe>() == null)
        {
            FindObjectOfType<WeaponSwitch>().StoreAxeLvlUpMultiplier(multiplier * currentCharacterLevel);
            Debug.Log("AXE NOT EQUIPPED");
            
        }
        else
        {
            Axe axe = FindObjectOfType<Axe>();
            axe.AxeOnLevelUp(multiplier * currentCharacterLevel);
        }
        
        if (FindObjectOfType<Sword>() == null)
        {
            FindObjectOfType<WeaponSwitch>().StoreSwordLvlUpMultiplier(multiplier * currentCharacterLevel);
            Debug.Log("SWORD NOT EQUIPPED");
            
        } else
        {
            Sword sword = FindObjectOfType<Sword>();
            sword.SwordOnLevelUp(multiplier * currentCharacterLevel);
        }

        if (FindObjectOfType<Spell>() == null)
        {
            FindObjectOfType<WeaponSwitch>().StoreSpellLvlUpMultiplier(multiplier * currentCharacterLevel);
            Debug.Log("SPELL NOT EQUIPPED");
            
        }
        else
        {
            Spell spell = FindObjectOfType<Spell>();
            spell.SpellOnLevelUp(multiplier * currentCharacterLevel);
        }
            
    }
}


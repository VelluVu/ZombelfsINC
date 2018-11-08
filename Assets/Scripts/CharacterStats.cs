using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : GlobalStats {

    public float maxMana;
    public float replenishM;
    public float replenishH;
    public float BackwardsMoveSpeed;
    public float jumpForce;
    public static float bonusMulti = 0.01f;

    public static int strength = 1;
    public static int dexterity = 1;
    public static int vitality = 1;
    public static int energy = 1;
   
    public int levelPojo;

    public int currentCharacterXP;
    public static int currentCharacterLevel;
    
    public GameObject lvlUpEff;
    public GameObject levelUpButton;
    public GameObject skillPojots;   
    public List<Button> buttons = new List<Button>();
    public List<Text> statTexts = new List<Text>();
    public WeaponSwitch focusThisWeapon;
    public Spell spellStats;
    public Axe axeStats;
    public Sword swordStats;

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
        levelPojo += 2;
        levelUpButton.SetActive(true);
        levelUpButton.GetComponent<Button>().onClick.AddListener(OpenSkillWindow);

        maxHealth += maxHealth * (bonusMulti * vitality * currentCharacterLevel);
        maxMana += maxMana * (bonusMulti * energy * currentCharacterLevel);
        replenishM += replenishM * (bonusMulti * energy * currentCharacterLevel);
        replenishH += replenishH * (bonusMulti * vitality * currentCharacterLevel);

                                           
    }

    public void OpenSkillWindow()
    {
        
        skillPojots.SetActive(true);
        statTexts[0].text = strength.ToString();
        statTexts[1].text = dexterity.ToString();
        statTexts[2].text = vitality.ToString();
        statTexts[3].text = energy.ToString();
        levelUpButton.GetComponent<Button>().onClick.RemoveListener(OpenSkillWindow);
        levelUpButton.GetComponent<Button>().onClick.AddListener(CloseSkillWindow);
        
        buttons[0].onClick.AddListener(RemoveStrength);
        buttons[1].onClick.AddListener(AddStrength);
        buttons[2].onClick.AddListener(RemoveDexterity);
        buttons[3].onClick.AddListener(AddDexterity);
        buttons[4].onClick.AddListener(RemoveVitality);
        buttons[5].onClick.AddListener(AddVitality);
        buttons[6].onClick.AddListener(RemoveEnergy);
        buttons[7].onClick.AddListener(AddEnergy);
        

    }

    public void CloseSkillWindow()
    {
        Debug.Log("Clicked close!!");
        buttons[0].onClick.RemoveListener(RemoveStrength);
        buttons[1].onClick.RemoveListener(AddStrength);
        buttons[2].onClick.RemoveListener(RemoveDexterity);
        buttons[3].onClick.RemoveListener(AddDexterity);
        buttons[4].onClick.RemoveListener(RemoveVitality);
        buttons[5].onClick.RemoveListener(AddVitality);
        buttons[6].onClick.RemoveListener(RemoveEnergy);
        buttons[7].onClick.RemoveListener(AddEnergy);
        levelUpButton.GetComponent<Button>().onClick.RemoveListener(CloseSkillWindow);
        skillPojots.SetActive(false);       
        levelUpButton.SetActive(false);
    }

    public void RemoveStrength()
    {
        focusThisWeapon.SwitchToThisWep(0);
        
        if (strength > 0)
        {
            Debug.Log("Removed StR Point: ");
            strength -= 1;         
            levelPojo += 1;
            axeStats.AxeOnPointRemove(bonusMulti * strength);
            statTexts[0].text = strength.ToString();
        }
    }

    public void AddStrength()
    {
        focusThisWeapon.SwitchToThisWep(0);
     
        if (levelPojo > 0)
        {                    
            Debug.Log("Added StR Point: ");
            levelPojo -= 1;
            strength += 1;            
            axeStats.AxeOnPointAdd(bonusMulti * strength);
            statTexts[0].text = strength.ToString();
        }
    }

    public void RemoveDexterity()
    {
        focusThisWeapon.SwitchToThisWep(1);
        
        if (dexterity > 0)
        {
            Debug.Log("Removed Dex Point: ");
            dexterity -= 1;           
            levelPojo += 1;
            swordStats.SwordOnPointRemove(bonusMulti * dexterity);
            statTexts[1].text = dexterity.ToString();
        }
    }

    public void AddDexterity()
    {
        focusThisWeapon.SwitchToThisWep(1);
        
        if (levelPojo > 0)
        {          
            Debug.Log("Added Dex Point: ");
            levelPojo -= 1;
            dexterity += 1;           
            swordStats.SwordOnPointAdd(bonusMulti * dexterity);
            statTexts[1].text = dexterity.ToString();
        }
    }

    public void RemoveVitality()
    {
        
        if (vitality > 0)
        {
            Debug.Log("Removed Vit Point: ");
            vitality -= 1;         
            levelPojo += 1;
            maxHealth -= bonusMulti * vitality;
            statTexts[2].text = vitality.ToString();
        }
    }

    public void AddVitality()
    {
        

        if (levelPojo > 0)
        {
            Debug.Log("Added Vit Point: ");
            levelPojo -= 1;
            vitality += 1;         
            maxHealth += bonusMulti * vitality;
            statTexts[2].text = vitality.ToString();
        }
    }

    public void RemoveEnergy()
    {
        focusThisWeapon.SwitchToThisWep(2);
       

        if (energy > 0)
        {
            Debug.Log("Removed Energy Point: ");
            energy -= 1;       
            levelPojo += 1;
            spellStats.SpellOnPointRemove(bonusMulti * energy);
            statTexts[3].text = energy.ToString();
        }
    }

    public void AddEnergy()
    {
        focusThisWeapon.SwitchToThisWep(2);
       

        if (levelPojo > 0)
        {
            Debug.Log("Added Energy Point: ");
            levelPojo -= 1;
            energy += 1;        
            spellStats.SpellOnPointAdd(bonusMulti * energy);
            statTexts[3].text = energy.ToString();
        }
    }

}


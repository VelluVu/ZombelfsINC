using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour {

    #region Singleton

    public static WeaponSwitch instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    

    public int selectedWeapon = 0;
    CharacterControl characterControl;
    List<float> axeLvlMods = new List<float>();
    List<float> swordLvlMods = new List<float>();
    List<float> spellLvlMods = new List<float>();

    private void Start()
    {
        characterControl = gameObject.GetComponentInParent<CharacterControl>();
        SelectWeapon();
    }

    private void Update()
    {
        WeaponSelection();
    }

    public void SwitchToThisWep(int index)
    {
        selectedWeapon = index;
        characterControl.nextAxeShot = 0;
        characterControl.nextSwordShot = 0;
        characterControl.nextSpellShot = 0;
        SelectWeapon();

    }

    public void StoreAxeLvlUpMultiplier(float lvlMod)
    {
        axeLvlMods.Add(lvlMod);

        foreach (float value in axeLvlMods)
        {
            Debug.Log("STORED AXE LVL VALUES: " + value);
        }
        
    }

    public void StoreSwordLvlUpMultiplier(float lvlMod)
    {
        swordLvlMods.Add(lvlMod);

        foreach (float value in swordLvlMods)
        {
            Debug.Log("STORED AXE LVL VALUES: " + value);
        }

    }

    public void StoreSpellLvlUpMultiplier(float lvlMod)
    {
        spellLvlMods.Add(lvlMod);

        foreach (float value in spellLvlMods)
        {
            Debug.Log("STORED AXE LVL VALUES: " + value);
        }

    }

    private void WeaponSelection()
    {
        int previouslySelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            selectedWeapon = 0;

            if (characterControl.axeCdReady)
            characterControl.nextAxeShot = 0;

            Debug.Log("Selected Wep: " + selectedWeapon);
            

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            
            selectedWeapon = 1;

            if (characterControl.swordCdReady)
            characterControl.nextSwordShot = 0;

            Debug.Log("Selected Wep: " + selectedWeapon);
            

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            
            selectedWeapon = 2;

            if (characterControl.spellCdReady)
            characterControl.nextSpellShot = 0;

            Debug.Log("Selected Wep: " + selectedWeapon);
            

        }

        if (previouslySelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    public void SelectWeapon()
    {
        int i = 0;

        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                if(i == 0)
                {
                    FindObjectOfType<Axe>().AxeStoredLevelUp(axeLvlMods);
                    axeLvlMods.Clear();
                }
                if(i == 1)
                {
                    FindObjectOfType<Sword>().SwordStoredLevelUp(swordLvlMods);
                    swordLvlMods.Clear();
                }
                if(i == 2)
                {
                    FindObjectOfType<Spell>().SpellStoredLevelUp(spellLvlMods);
                    spellLvlMods.Clear();
                }
          
            }
            else
            {
                weapon.gameObject.SetActive(false);
               
            }

            i++;
        }
    }
}

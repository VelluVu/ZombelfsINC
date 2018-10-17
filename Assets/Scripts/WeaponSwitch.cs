using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour {

    public int selectedWeapon = 0;

    private void Start()
    {     
        SelectWeapon();
    }

    private void Update()
    {

        int previouslySelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            selectedWeapon = 0;
            Debug.Log("Selected Wep: " + selectedWeapon);
           
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            
            selectedWeapon = 1;
            Debug.Log("Selected Wep: " + selectedWeapon);
            
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            
            selectedWeapon = 2;
            Debug.Log("Selected Wep: " + selectedWeapon);
           
        }

        if (previouslySelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;

        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }
}

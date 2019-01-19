
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerPickUp : Interactable
{

    public StatPower charBoost;
    public AxePower axeBoost;
    public SwordPower swordBoost;
    public SpellPower spellBoost;
    public List<StatPower> charBoosts = new List<StatPower>();
    public List<AxePower> axeBoosts = new List<AxePower>();
    public List<SwordPower> swordBoosts = new List<SwordPower>();
    public List<SpellPower> spellBoosts = new List<SpellPower>();
    public GameObject canvas;
    float powerUpDuration;

    private void Awake()
    {
        foreach (var item in charBoosts)
        {
            if (item != null)
            {
                charBoost = charBoosts[Random.Range(0,charBoosts.Count)];
            }
        }
        foreach (var item in axeBoosts)
        {
            if (item != null)
            {
                axeBoost = axeBoosts[Random.Range(0, axeBoosts.Count)];
            }
        }
        foreach (var item in swordBoosts)
        {
            if (item != null)
            {
                swordBoost = swordBoosts[Random.Range(0, swordBoosts.Count)];
            }
        }
        foreach (var item in spellBoosts)
        {
            if (item != null)
            {
                spellBoost = spellBoosts[Random.Range(0, spellBoosts.Count)];
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && charBoost )
        {
            
            StartCoroutine(PickUp(other));
        }

        if (other.CompareTag("Player") && axeBoost)
        {
            WeaponSwitch switchWep = other.GetComponentInChildren<WeaponSwitch>();
            switchWep.SwitchToThisWep(0);
            StartCoroutine(PickUp(other));
        }

        if (other.CompareTag("Player") && swordBoost)
        {
            WeaponSwitch switchWep = other.GetComponentInChildren<WeaponSwitch>();
            switchWep.SwitchToThisWep(1);
            StartCoroutine(PickUp(other));

        }

        if (other.CompareTag("Player") && spellBoost)
        {
            WeaponSwitch switchWep = other.GetComponentInChildren<WeaponSwitch>();
            switchWep.SwitchToThisWep(2);
            StartCoroutine(PickUp(other));
        }
    }

    IEnumerator PickUp(Collider other)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<ParticleSystem>().Stop();       
        CharacterStats stats = other.GetComponent<CharacterStats>();
        Axe axe = other.GetComponentInChildren<Axe>();
        Sword sword = other.GetComponentInChildren<Sword>();
        Spell spell = other.GetComponentInChildren<Spell>();

        if (charBoost != null)
        {
            Destroy(Instantiate(charBoost.pickUpEffect, gameObject.transform.position, Quaternion.identity), 3f);
            Destroy(Instantiate(charBoost.pickUpSound, gameObject.transform.position, Quaternion.identity), 3f);

            Destroy(Instantiate(charBoost.pickUpText, gameObject.transform.position, Quaternion.identity),5f);

            powerUpDuration = charBoost.boostDuration;
            if(powerUpDuration > 20f)
            {
                powerUpDuration = 20f;
            }
            stats.maxHealth += charBoost.healthBoost;
            stats.maxMana += charBoost.manaBoost;
            stats.moveSpeed += charBoost.speedBoost;
            stats.jumpForce += charBoost.jumpForceBoost;
            stats.replenishH += charBoost.healthRegenBoost;
            stats.replenishM += charBoost.manaRegenBoost;

        }
        else if (axeBoost != null)
        {
            Destroy(Instantiate(axeBoost.pickUpEffect, gameObject.transform.position, Quaternion.identity), 3f);
            Destroy(Instantiate(axeBoost.pickUpSound, gameObject.transform.position, Quaternion.identity), 3f);

            Destroy(Instantiate(axeBoost.pickUpText, gameObject.transform.position, Quaternion.identity),3f);

            powerUpDuration = axeBoost.boostDuration;
            if (powerUpDuration > 20f)
            {
                powerUpDuration = 20f;
            }
            axe.axeMultiShot = axeBoost.multiShooter;
            axe.axeConeSize = axeBoost.shotSpread;
            axe.axeShotgunAmount = axeBoost.shotMultiply;

            for (int i = 0; i < axe.axeStats.Length; i++)
            {
                //Debug.Log("BOOSTED BY   " + axeBoost.GetAxeBoostArray()[i]);
                if (axeBoost.GetAxeBoostArray()[i] > 0)
                    axe.axeStats[i] *= axeBoost.GetAxeBoostArray()[i];

                //Debug.Log("AFTER BOOST VALUE  " + axe.axeStats[i]);
            }   
        }
        else if (swordBoost != null)
        {
            Destroy(Instantiate(swordBoost.pickUpEffect, gameObject.transform.position, Quaternion.identity), 3f);
            Destroy(Instantiate(swordBoost.pickUpSound, gameObject.transform.position, Quaternion.identity), 3f);

            Destroy(Instantiate(swordBoost.pickUpText, gameObject.transform.position, Quaternion.identity), 3f);

            powerUpDuration = swordBoost.boostDuration;
            if (powerUpDuration > 20f)
            {
                powerUpDuration = 20f;
            }
            sword.swordMultiShot = swordBoost.multiShooter;
            sword.swordConeSize = swordBoost.shotSpread;
            sword.swordShotgunAmount = swordBoost.shotMultiply;

            for (int i = 0; i < sword.swordStats.Length; i++)
                {
                    //Debug.Log("BOOSTED BY   " + swordBoost.GetSwordBoostArray()[i]);
                    if(swordBoost.GetSwordBoostArray()[i] > 0)
                        sword.swordStats[i] *= swordBoost.GetSwordBoostArray()[i];

                    //Debug.Log("AFTER BOOST VALUE  " + sword.swordStats[i]);

                }
        }
        else if (spellBoost != null)
        {

            Destroy(Instantiate(spellBoost.pickUpEffect, gameObject.transform.position, Quaternion.identity), 3f);
            Destroy(Instantiate(spellBoost.pickUpSound, gameObject.transform.position, Quaternion.identity), 3f);

            Destroy(Instantiate(spellBoost.pickUpText, gameObject.transform.position, Quaternion.identity), 3f);

            powerUpDuration = spellBoost.boostDuration;
            if (powerUpDuration > 20f)
            {
                powerUpDuration = 20f;
            }
            spell.spellMultiShot = spellBoost.multiShooter;
            spell.spellConeSize = spellBoost.shotSpread;
            spell.spellShotgunAmount = spellBoost.shotMultiply;

            for (int i = 0; i < spell.spellStats.Length; i++)
            {
                //Debug.Log("BOOSTED BY   " + spellBoost.GetSpellBoostArray()[i]);
                if (spellBoost.GetSpellBoostArray()[i] > 0)
                    spell.spellStats[i] *= spellBoost.GetSpellBoostArray()[i];

                //Debug.Log("AFTER BOOST VALUE  " + spell.spellStats[i]);
            }
        }
    
        yield return new WaitForSeconds(powerUpDuration);

        if (axeBoost != null)
        {
            axe.LoadAxeStats();
            axe.axeMultiShot = false;         
        }
        else if (swordBoost != null)
        {
            sword.LoadSwordStats();
            sword.swordMultiShot = false;
        }
        else if (spellBoost != null)
        {
            spell.LoadSpellStats();
            spell.spellMultiShot = false;         
        }
        else if (charBoost != null)
        {
            stats.maxHealth -= charBoost.healthBoost;
            stats.maxMana -= charBoost.manaBoost;
            stats.moveSpeed -= charBoost.speedBoost;
            stats.jumpForce -= charBoost.jumpForceBoost;
            stats.replenishH -= charBoost.healthRegenBoost;
            stats.replenishM -= charBoost.manaRegenBoost;
        }
            
        Destroy(gameObject);
       
    }   
}
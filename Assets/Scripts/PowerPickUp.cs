
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerPickUp : Interactable
{

    public StatPower charBoost;
    public AxePower axeBoost;
    public SwordPower swordBoost;
    public SpellPower spellBoost;
    public GameObject canvas;
    float powerUpDuration;
    
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
            Destroy(Instantiate(charBoost.pickUpEffect, gameObject.transform.position, Quaternion.identity),3f);
            Destroy(Instantiate(charBoost.pickUpSound, gameObject.transform.position, Quaternion.identity), 3f);

            GameObject aPText = Instantiate(charBoost.pickUpText) as GameObject;
            aPText.transform.SetParent(canvas.transform, false);
            aPText.transform.localScale *= 0.1f;
            aPText.GetComponent<Text>().text = charBoost.name;
            Destroy(aPText, 2f);

            powerUpDuration = charBoost.boostDuration;
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

            GameObject aPText = Instantiate(axeBoost.pickUpText) as GameObject;
            aPText.transform.SetParent(canvas.transform, false);
            aPText.transform.localScale *= 0.1f;
            aPText.GetComponent<Text>().text = axeBoost.name;          
            Destroy(aPText,2f);

            powerUpDuration = axeBoost.boostDuration;  
            
            for (int i = 0; i < axe.axeStats.Length; i++)
            {
                //Debug.Log("BOOSTED BY   " + axeBoost.GetAxeBoostArray()[i]);
                
                axe.axeStats[i] *= axeBoost.GetAxeBoostArray()[i];

                //Debug.Log("AFTER BOOST VALUE  " + axe.axeStats[i]);
            }
            

        }
        else if (swordBoost != null)
        {
            Destroy(Instantiate(swordBoost.pickUpEffect, gameObject.transform.position, Quaternion.identity), 3f);
            Destroy(Instantiate(swordBoost.pickUpSound, gameObject.transform.position, Quaternion.identity), 3f);

            GameObject aPText = Instantiate(swordBoost.pickUpText) as GameObject;
            aPText.transform.SetParent(canvas.transform, false);
            aPText.transform.localScale *= 0.1f;
            aPText.GetComponent<Text>().text = swordBoost.name;
            Destroy(aPText, 2f);

            powerUpDuration = swordBoost.boostDuration;

                for (int i = 0; i < sword.swordStats.Length; i++)
                {
                    //Debug.Log("BOOSTED BY   " + swordBoost.GetSwordBoostArray()[i]);

                    sword.swordStats[i] *= swordBoost.GetSwordBoostArray()[i];

                    //Debug.Log("AFTER BOOST VALUE  " + sword.swordStats[i]);

                }
            
        }
        else if (spellBoost != null)
        {

            Destroy(Instantiate(spellBoost.pickUpEffect, gameObject.transform.position, Quaternion.identity), 3f);
            Destroy(Instantiate(spellBoost.pickUpSound, gameObject.transform.position, Quaternion.identity), 3f);

            GameObject aPText = Instantiate(spellBoost.pickUpText) as GameObject;
            aPText.transform.SetParent(canvas.transform, false);
            aPText.transform.localScale *= 0.1f;
            aPText.GetComponent<Text>().text = spellBoost.name;
            Destroy(aPText, 2f);

            powerUpDuration = spellBoost.boostDuration;
          
            for (int i = 0; i < spell.spellStats.Length; i++)
            {
                //Debug.Log("BOOSTED BY   " + spellBoost.GetSpellBoostArray()[i]);

                spell.spellStats[i] *= spellBoost.GetSpellBoostArray()[i];

                //Debug.Log("AFTER BOOST VALUE  " + spell.spellStats[i]);
            }
        }
    

        yield return new WaitForSeconds(powerUpDuration);

        if (axeBoost != null)
            axe.LoadAxeStats();
        else if (swordBoost != null)
            sword.LoadSwordStats();
        else if (spellBoost != null)
            spell.LoadSpellStats();
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

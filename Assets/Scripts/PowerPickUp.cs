
using System.Collections;
using UnityEngine;

public class PowerPickUp : Interactable
{

    public float[] characterStatboosts;
    public float[] weaponBoost;
    public bool statB;
    public bool axeB;
    public bool swordB;
    public bool spellB;
    float powerUpDuration;
    CharacterControl characterBoost;

    //sorry about this it was late night and bad sleep...
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            characterBoost = other.GetComponent<CharacterControl>();
            StartCoroutine(PickUp(other));
        }
    }

    IEnumerator PickUp(Collider other)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<ParticleSystem>().Stop();



        yield return new WaitForSeconds(powerUpDuration);

        Destroy(gameObject);
        /*for (int i = 0; i < power.Length; i++)
        {


            if (power[i].statBooster)
            {
                characterStatboosts[0] = power[i].healthBoost;
                characterStatboosts[1] = power[i].manaBoost;
                characterStatboosts[2] = power[i].speedBoost;
                characterStatboosts[3] = power[i].manaRegenBoost;
                characterStatboosts[4] = power[i].healthRegenBoost;
                characterStatboosts[5] = power[i].jumpForceBoost;
                powerUpDuration = power[i].boostDuration;

                statB = true;
                
                
            }
            else if (power[i].axeBoost)
            {
                axeSkillBoosts[0] = power[i].speedzBoost;
                axeSkillBoosts[1] = power[i].speedyBoost;
                axeSkillBoosts[2] = power[i].rotationSpeedBoost;
                axeSkillBoosts[3] = power[i].damageBoost;
                axeSkillBoosts[4] = power[i].areaDamageBoost;
                axeSkillBoosts[5] = power[i].shotIntervalBoost;
                axeSkillBoosts[6] = power[i].spellCostBoost;
                axeSkillBoosts[7] = power[i].areaRadiusBoost;
                axeSkillBoosts[8] = power[i].meleeDamageBoost;
                axeSkillBoosts[9] = power[i].criticalChanceBoost;
                powerUpDuration = power[i].boostDuration;

                axeB = true;
                
               
            }
            else if (power[i].swordBoost)
            {
                swordSkillBoosts[0] = power[i].speedzBoost;
                swordSkillBoosts[1] = power[i].speedyBoost;
                swordSkillBoosts[2] = power[i].rotationSpeedBoost;
                swordSkillBoosts[3] = power[i].damageBoost;
                swordSkillBoosts[4] = power[i].areaDamageBoost;
                swordSkillBoosts[5] = power[i].shotIntervalBoost;
                swordSkillBoosts[6] = power[i].spellCostBoost;
                swordSkillBoosts[7] = power[i].areaRadiusBoost;
                swordSkillBoosts[8] = power[i].meleeDamageBoost;
                swordSkillBoosts[9] = power[i].criticalChanceBoost;
                powerUpDuration = power[i].boostDuration;

                swordB = true;
               
                
            }
            else if (power[i].spellBoost)
            {
                spellSkillBoosts[0] = power[i].speedzBoost;
                spellSkillBoosts[1] = power[i].speedyBoost;
                spellSkillBoosts[2] = power[i].rotationSpeedBoost;
                spellSkillBoosts[3] = power[i].damageBoost;
                spellSkillBoosts[4] = power[i].areaDamageBoost;
                spellSkillBoosts[5] = power[i].shotIntervalBoost;
                spellSkillBoosts[6] = power[i].spellCostBoost;
                spellSkillBoosts[7] = power[i].areaRadiusBoost;
                spellSkillBoosts[8] = power[i].meleeDamageBoost;
                spellSkillBoosts[9] = power[i].criticalChanceBoost;
                powerUpDuration = power[i].boostDuration;

                spellB = true;
               
                
            }

            if (statB)
            {
                for (int y = 0; y < characterStatboosts.Length; y++)
                {
                    characterBoost.characterStatsArr[y] += characterStatboosts[y];
                }
            }
            else if (axeB)
            {
                for (int z = 0; z < axeSkillBoosts.Length; z++)
                {
                    characterBoost.axe.axeStats[z] *= axeSkillBoosts[z];
                }
            }
            else if (swordB)
            {
                for (int r = 0; r < swordSkillBoosts.Length; r++)
                {
                    characterBoost.sword.swordStats[r] *= swordSkillBoosts[r];
                }
            }
            else if (spellB)
            {
                for (int f = 0; f < spellSkillBoosts.Length; f++)
                {
                    characterBoost.spell.spellStats[f] *= spellSkillBoosts[f];
                }
            }
            
        }
       

        yield return new WaitForSeconds(powerUpDuration);

        if (statB)
        {
            characterBoost.ResetStats();
        }
        if (axeB)
        {
            characterBoost.axe.LoadAxeStats();
        }
        if (swordB)
        {
            characterBoost.sword.LoadSwordStats();
        }
        if (spellB)
        {
            characterBoost.spell.LoadSpellStats();
        }*/

    }
    
}

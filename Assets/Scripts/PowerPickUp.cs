
using System.Collections;
using UnityEngine;

public class PowerPickUp : Interactable
{

    public Power[] power;
    public float[] characterStatboosts = new float[6];
    public float[] axeSkillBoosts = new float[10];
    public float[] swordSkillBoosts = new float[10];
    public float[] spellSkillBoosts = new float[10];
    public bool statB;
    public bool axeB;
    public bool swordB;
    public bool spellB;
    float powerUpDuration;
    CharacterControl characterBoost;

    //sorry about this it was late night and bad sleep...
    private void Start()
    {
        power = new Power[3];
        characterStatboosts = new float[6];
        axeSkillBoosts = new float[10];
        swordSkillBoosts = new float[10];
        spellSkillBoosts = new float[10];
    }

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

        for (int i = 0; i < power.Length; i++)
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
                characterBoost.characterStatsArr[i] += characterStatboosts[i];
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
                characterBoost.axe.axeStats[i] *= axeSkillBoosts[i];
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
                characterBoost.sword.swordStats[i] *= swordSkillBoosts[i];
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
                characterBoost.spell.spellStats[i] *= spellSkillBoosts[i];
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
        }

        Destroy(gameObject);
    }

}

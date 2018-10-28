
using System.Collections;
using UnityEngine;

public class PowerPickUp : Interactable
{

    public Power power;
    float powerUpDuration;
    CharacterControl characterBoost;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            characterBoost = other.GetComponent<CharacterControl>();
            StartCoroutine(PickUp());
        }
    }

    IEnumerator PickUp()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<ParticleSystem>().Stop();

        Instantiate(power.pickUpEffect, gameObject.transform.position, power.pickUpEffect.transform.rotation);
        Instantiate(power.pickUpSound, gameObject.transform.position, power.pickUpSound.transform.rotation);

        Debug.Log("Picked Up : " + power.name);

        powerUpDuration = power.boostDuration;

        if (power.statBooster)
        {

            characterBoost.IncrementMaxHealth(power.healthBoost);
            characterBoost.IncrementMaxMana(power.manaBoost);
            characterBoost.SpeedUp(power.speedBoost);
            characterBoost.ReplenishMana(power.manaRegenBoost);
            characterBoost.ReplenishHealth(power.healthRegenBoost);
            characterBoost.IncreaseJump(power.jumpForceBoost);
        }
        if (power.axeBoost)
        {
            characterBoost.axe.projectileDamage *= power.damageBoost;
            characterBoost.axe.projectileRotationSpeed *= power.rotationSpeedBoost;
            characterBoost.axe.projectileSpeedz *= power.speedzBoost;
            characterBoost.axe.projectileSpeedy *= power.speedyBoost;
            characterBoost.axe.shotInterval *= power.shotIntervalBoost;
        }
        if (power.swordBoost)
        {
            characterBoost.sword._projectileDamage *= power.damageBoost;
            characterBoost.sword._projectileRotationSpeed *= power.rotationSpeedBoost;
            characterBoost.sword._projectileSpeedz *= power.speedzBoost;
            characterBoost.sword._projectileSpeedy *= power.speedyBoost;
            characterBoost.sword._shotInterval *= power.shotIntervalBoost;
            characterBoost.sword._criticalChance *= power.criticalChanceBoost;
            characterBoost.sword._meleeDamage *= power.meleeDamageBoost;
        }
        if (power.spellBoost)
        {
            characterBoost.spell.projectileAreaDamage *= power.areaDamageBoost;
            characterBoost.spell.projectileAreaRadius *= power.areaRadiusBoost;
            characterBoost.spell.projectileSpeedz *= power.speedzBoost;
            characterBoost.spell.projectileSpeedy *= power.speedyBoost;
            characterBoost.spell.shotInterval *= power.shotIntervalBoost;
            characterBoost.spell.projectileRotationSpeed *= power.rotationSpeedBoost;
            characterBoost.spell.spellCost *= power.spellCostBoost;
        }

        yield return new WaitForSeconds(powerUpDuration);

        if (power.statBooster)
        {
            characterBoost.ResetStats();
        }
        if (power.spellBoost)
        {
            characterBoost.spell.InitializeSpell();
        }
        if (power.swordBoost)
        {
            characterBoost.sword.InitializeSword();
        }
        if (power.spellBoost)
        {
            characterBoost.axe.InitializeAxe();
        }

        Destroy(gameObject);
    }

}

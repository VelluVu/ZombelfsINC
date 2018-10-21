using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPowerUp : MonoBehaviour {

    
    AudioSource pickUpSound;
    public GameObject pickUpEffect;
    float beforeDestroy;
    CharacterControl characterPowers;

    public float increase = 100;
    public float replenish = 200;
    public float speedmultiplier = 4;
    public float fireRateMultiplier = 0.25f;
    public float fireRateReset = 8f;
    public float spellCostReduce = 0.5f;
    public float spellCostMultiplier = 2;
    
      
    private void Start()
    {
        beforeDestroy = 20f;
        pickUpSound = gameObject.GetComponent<AudioSource>();
        characterPowers = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            StartCoroutine(PickUp());
            
        }
    } 

    IEnumerator PickUp()
    {
        pickUpSound.Play();
        
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<ParticleSystem>().Stop();



        Instantiate(pickUpEffect, gameObject.transform.position, pickUpEffect.transform.rotation);

        characterPowers.IncrementMaxMana(increase);
        characterPowers.ReplenishMana(replenish);
        characterPowers.spell.projectileSpeedz *= speedmultiplier;
        characterPowers.spell.shotInterval *= fireRateMultiplier;
        characterPowers.spell.spellCost *= spellCostReduce;
        characterPowers.replenishM = speedmultiplier;

        yield return new WaitForSeconds(beforeDestroy);

        characterPowers.ResetStats();
        characterPowers.spell.ResetSpell();

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    float moveSpeed;
    float rotationSpeed;
    float curhealth;
    float maxHealth;
    float damage;
    float maxResist;
    float resistDiminish;
    float instantiateDuration;

    Rigidbody rb;
    NavMeshAgent navMeshAgent;
    Transform eyes;
    Transform chaseTarget;
    public GameObject BloodSpill;
    public Image healthbar;
    public GameObject dmgText;
    public GameObject deathSound;
    DmgText displayDmg;
    

    private void Start()
    {
        
        chaseTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody>();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        eyes = gameObject.transform.GetChild(0);      
        moveSpeed = 0.8f;
        rotationSpeed = 5f;
        maxHealth = 100f;
        curhealth = maxHealth;
        damage = 20f;
        instantiateDuration = 3f;
    }

    private void Update()
    {
        healthbar.fillAmount = curhealth / maxHealth;
        EnemyMove();
        Death();      
    }

    void EnemyMove()
    {
        navMeshAgent.destination = chaseTarget.position;
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
        {
            navMeshAgent.destination = chaseTarget.position;
        }
        //transform.LookAt(chaseTarget.position);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            //DEAL DAMAGE
            collision.collider.GetComponent<CharacterControl>().PlayerTakeDamage(damage);
        }
    }

    public void EnemyTakeDamage(float dmg)
    {
        
            curhealth -= dmg;
            displayDmg = dmgText.gameObject.GetComponentInChildren<Text>().GetComponent<DmgText>();
            displayDmg.SetDmgText(dmg,this);
            Instantiate(dmgText, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), dmgText.transform.rotation);
            Destroy(Instantiate(BloodSpill, transform.position, BloodSpill.transform.rotation), instantiateDuration);
            Debug.Log("I take dmg: " + dmg);
        
        
    }

    public void OverTimeDamage(float dmg)
    {
        curhealth -= dmg;
        displayDmg = dmgText.gameObject.GetComponentInChildren<Text>().GetComponent<DmgText>();     
        displayDmg.SetDmgText(dmg,this);
        if (this != null)
        {
            Instantiate(dmgText, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), dmgText.transform.rotation);
        }
    }

    
    private void Death()
    {
        if (curhealth <= 0)
        {
            Destroy(Instantiate(deathSound, transform.position, transform.rotation),instantiateDuration);
            Destroy(Instantiate(BloodSpill, transform.position, BloodSpill.transform.rotation), instantiateDuration);
            Destroy(gameObject);
        }
    }

    public IEnumerator EnemyStatusStart(int duration, float overTimeDamage, float tickRate)
    {

        Debug.Log("Enemy Starts Coroutine");
        int currentCount = 0;

        for (int i = currentCount; i < duration; i++)
        {
            
            Debug.Log("Tick");
            OverTimeDamage(overTimeDamage);
            yield return new WaitForSeconds(tickRate);

        }

    }
}

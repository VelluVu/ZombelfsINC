using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    float moveSpeed;
    float rotationSpeed;
    float curhealth;
    float maxHealth;
    float damage;

    Rigidbody rb;
    NavMeshAgent navMeshAgent;
    Transform eyes;
    Transform chaseTarget;
    public GameObject BloodSpill;

    private void Start()
    {
        chaseTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody>();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        eyes = gameObject.transform.GetChild(0);      
        moveSpeed = 1.2f;
        rotationSpeed = 5f;
        maxHealth = 100f;
        curhealth = maxHealth;
        damage = 20f;
    }

    private void Update()
    {
        
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
        }
    }

    public void EnemyTakeDamage(float dmg)
    {
        curhealth -= dmg;
        Destroy(Instantiate(BloodSpill, transform.position, BloodSpill.transform.rotation),3f);
        Debug.Log("I take dmg: " + dmg);
    }

    private void Death()
    {
        if (curhealth <= 0)
        {
            Destroy(Instantiate(BloodSpill, transform.position, BloodSpill.transform.rotation), 3f);
            Destroy(gameObject);
        }
    }
}

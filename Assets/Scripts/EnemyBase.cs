using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class EnemyBase : MonoBehaviour {

    public float moveSpeed = 1;
    public float rotationSpeed = 1;
    public float curhealth = 100;
    public float maxHealth = 100;
    public float damage = 1;
    public float maxResist = 1;
    public float resistDiminish = 1;
    public float instantiateDuration = 3;
    public int price = 2;

    public DmgText enemyDisplayDmg;
    public Rigidbody enemyRB;
    public NavMeshAgent enemyNavMeshAgent;
    public Transform enemyChaseTarget;
    public GameObject enemyBloodSpill;
    public Image enemyHealthbar;
    public GameObject enemyDmgText;
    public GameObject enemyDeathSound;
    public Enemy enemy;

    protected virtual void Start()
    {
        enemyDisplayDmg = enemyDmgText.gameObject.GetComponentInChildren<Text>().GetComponent<DmgText>();
        enemyChaseTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyRB = gameObject.GetComponent<Rigidbody>();
        enemyNavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        curhealth = maxHealth;

    }

    protected virtual void Update()
    {
        EnemyMove();
        healthUpdate();
        Death();
    }

    protected virtual void healthUpdate()
    {
        enemyHealthbar.fillAmount = curhealth / maxHealth;
    }

    protected virtual void EnemyMove()
    {
        enemyNavMeshAgent.destination = enemyChaseTarget.position;
        if (enemyNavMeshAgent.remainingDistance <= enemyNavMeshAgent.stoppingDistance && !enemyNavMeshAgent.pathPending)
        {
            enemyNavMeshAgent.destination = enemyChaseTarget.position;
        }

    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterControl>().PlayerTakeDamage(damage);
        }
    }

    protected virtual void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            //DEAL DAMAGE
            collision.collider.GetComponent<CharacterControl>().PlayerTakeDamage(damage);
        }
    }

    public virtual void EnemyTakeDamage(float dmg, bool crit)
    {

        curhealth -= dmg;
        enemyDisplayDmg.SetDmgText(dmg, enemy, crit);
        Destroy(Instantiate(enemyDmgText, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), enemyDmgText.transform.rotation), instantiateDuration);
        Destroy(Instantiate(enemyBloodSpill, transform.position, enemyBloodSpill.transform.rotation), instantiateDuration);
        Debug.Log("I take dmg: " + dmg);

    }

    public virtual void OverTimeDamage(float dmg)
    {
        curhealth -= dmg;
        enemyDisplayDmg.SetDmgText(dmg, enemy, false);
        if (this != null)
        {
            Destroy(Instantiate(enemyDmgText, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), enemyDmgText.transform.rotation), instantiateDuration);
        }
    }


    protected virtual void Death()
    {
        if (curhealth <= 0)
        {
            ScoreTable.Addpoint(price);
            Destroy(Instantiate(enemyDeathSound, transform.position, transform.rotation), instantiateDuration);
            Destroy(Instantiate(enemyBloodSpill, transform.position, enemyBloodSpill.transform.rotation), instantiateDuration);
            Destroy(gameObject);
        }
    }

    public virtual IEnumerator EnemyStatusStart(int duration, float overTimeDamage, float tickRate)
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

    public virtual void EnemySpeedIncrease(float multiplier)
    {
        moveSpeed += multiplier;
    }
}

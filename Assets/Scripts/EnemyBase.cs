using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class EnemyBase : MonoBehaviour {

    float moveSpeed;
    float curhealth;
    float maxHealth;
    float damage;
    float instantiateDuration = 3;
    float laughCd;
        float range;
    int price;

    public DmgText enemyDisplayDmg;
    public Rigidbody enemyRB;
    public NavMeshAgent enemyNavMeshAgent;
    public Transform enemyChaseTarget;
    public GameObject enemyBloodSpill;
    public Image enemyHealthbar;
    public GameObject enemyDmgText;
    public GameObject enemyDeathSound;
    public GameObject laughPrefab;
    public GameObject specialTalk;
    EnemyStats enemyStats;

    protected virtual void Start()
    {
        enemyStats = gameObject.GetComponent<EnemyStats>();
        enemyDisplayDmg = enemyDmgText.gameObject.GetComponentInChildren<Text>().GetComponent<DmgText>();
        enemyChaseTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyRB = gameObject.GetComponent<Rigidbody>();
        enemyNavMeshAgent = gameObject.GetComponent<NavMeshAgent>();

        maxHealth = enemyStats.maxHealth;
        damage = enemyStats.damage;
        price = enemyStats.price;
        moveSpeed = enemyStats.moveSpeed;
  
        curhealth = maxHealth;

    }

    protected virtual void Update()
    {
        EnemyMove();
        healthUpdate();
        Death();
        laughing();
    }

    protected virtual void laughing()
    {
        range = Vector3.Distance(transform.position, enemyChaseTarget.transform.position);
        laughCd -= Time.deltaTime;
        if (range < 7f)
        {
            if (laughCd <= 0)
            {             
                Destroy(Instantiate(laughPrefab), 4);
                laughCd = 4;
            }
            
        }
    }

protected virtual void healthUpdate()
    {
        enemyHealthbar.fillAmount = curhealth / maxHealth;
    }

    protected virtual void EnemyMove()
    {
        enemyNavMeshAgent.speed = moveSpeed;
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
        if (collision.collider.CompareTag("Player"))
        {
            //DEAL DAMAGE
            collision.collider.GetComponent<CharacterControl>().PlayerTakeDamage(damage);
        }
    }

    
        private void OnCollisionEnter(Collision collision)
        {
        if (collision.collider.CompareTag("Player"))
            Destroy(Instantiate(specialTalk),11f);
        }
    

    public virtual void EnemyTakeDamage(float dmg, bool crit)
    {

        curhealth -= dmg;
        enemyDisplayDmg.SetDmgText(dmg, this, crit);
        Destroy(Instantiate(enemyDmgText, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), enemyDmgText.transform.rotation), instantiateDuration);
        Destroy(Instantiate(enemyBloodSpill, transform.position, enemyBloodSpill.transform.rotation), instantiateDuration);
        Debug.Log("I take dmg: " + dmg);

    }

    public virtual void OverTimeDamage(float dmg)
    {
        curhealth -= dmg;
        enemyDisplayDmg.SetDmgText(dmg, this, false);
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
            FindObjectOfType<CharacterStats>().UpdateXp(100);
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
        moveSpeed *= multiplier;
    }
}

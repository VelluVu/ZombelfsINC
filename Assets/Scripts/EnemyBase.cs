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
    float xpReward;
    bool speaking;
    bool doDmg;
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
    public GameObject ragDoll;
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
        xpReward = enemyStats.xpReward;
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
            if (doDmg == false)
            {
                other.GetComponent<CharacterControl>().PlayerTakeDamage(damage);
                doDmg = true;
                StartCoroutine(EatMore());
            }

        }
    }

    IEnumerator EatMore()
    {
        yield return new WaitForSeconds(0.1f);
        doDmg = false;
    }

    protected virtual void OnCollisionStay(Collision collision)
    {
        //StartCoroutine(HitPlayer());
        if (collision.collider.CompareTag("Player"))
        {
            //DEAL DAMAGE
            
            //collision.collider.GetComponent<CharacterControl>().PlayerTakeDamage(damage);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Player"))
        {
            if (!speaking)
            {
                
                Destroy(Instantiate(specialTalk), 11f);
                speaking = true;

            }
            StartCoroutine(SpeakCD());
        }  
    }

    IEnumerator SpeakCD()
    {
        yield return new WaitForSeconds(10f);
        speaking = false;
    }
    
    public virtual void EnemyTakeDamage(float dmg, bool crit)
    {
        int dmgs = Mathf.RoundToInt(dmg);
        curhealth -= dmgs;
        enemyDisplayDmg.SetDmgText(dmgs, this, crit);
        Destroy(Instantiate(enemyDmgText, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), enemyDmgText.transform.rotation), instantiateDuration);
        Destroy(Instantiate(enemyBloodSpill, transform.position, enemyBloodSpill.transform.rotation), instantiateDuration);
        //Debug.Log("I take dmg: " + dmg);

    }

    public virtual void OverTimeDamage(float dmg)
    {
        int dmgs = Mathf.RoundToInt(dmg);
        curhealth -= dmg;
        enemyDisplayDmg.SetDmgText(dmgs, this, false);
        if (this != null)
        {
            Destroy(Instantiate(enemyDmgText, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), enemyDmgText.transform.rotation), instantiateDuration);
        }
    }

    protected virtual void Death()
    {
        if (curhealth <= 0)
        {

            ScoreTable.ChangeScore(ScoreTable.currentPlayer, "score", price);
            Destroy(Instantiate(ragDoll, transform.position, transform.rotation), 20f);
            Destroy(Instantiate(enemyDeathSound, transform.position, transform.rotation), instantiateDuration);
            Destroy(Instantiate(enemyBloodSpill, transform.position, enemyBloodSpill.transform.rotation), instantiateDuration);
            Destroy(gameObject);
            FindObjectOfType<CharacterStats>().UpdateXp(Mathf.RoundToInt(xpReward));
            
        }
    }

    public virtual IEnumerator EnemyStatusStart(int duration, int overTimeDamage, float tickRate)
    {

        //Debug.Log("Enemy Starts Coroutine");
        int currentCount = 0;

        for (int i = currentCount; i < duration; i++)
        {
            //Debug.Log("Tick");
            OverTimeDamage(overTimeDamage);
            yield return new WaitForSeconds(tickRate);
        }
    }

    public virtual void EnemySpeedIncrease(float multiplier)
    {
        moveSpeed += moveSpeed * multiplier;
    }
}

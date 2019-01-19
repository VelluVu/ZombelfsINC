using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour {

    int curSkill;

    bool onGround;
    bool recentlyShot;
    bool isDead;
    bool isWalking;
   
    public float currentHealth;
    public float currentMana;

    public float nextSpellShot;
    public float nextSwordShot;
    public float nextAxeShot;
    public bool swordCdReady;
    public bool axeCdReady;
    public bool spellCdReady;

    public bool useController;
    public Image healthPool;
    public Image manaPool;

    Rigidbody rb;
    Camera cam;
    public WeaponSwitch wep;
    public Axe axe;
    public Sword sword;
    public Spell spell;    
    public Animator charAnim;
    public GameObject bloodSplit;
    public GameObject loseSound;
    CharacterStats stats;

    private void Start()
    {
        ScoreTable.LoadScores();
        GameStatus.Load(ScoreTable.currentPlayer);
        rb = gameObject.GetComponent<Rigidbody>();
        cam = GameObject.FindGameObjectWithTag("TopDown").GetComponent<Camera>();
        stats = gameObject.GetComponent<CharacterStats>();
        GameStatus.theGameIsOn = true;
        currentHealth = stats.maxHealth;
        currentMana = stats.maxMana;  
        Time.timeScale = 1;        
        swordCdReady = true;
        axeCdReady = true;
        spellCdReady = true;
        isDead = false;
        GameStatus.inLevel = true;
    }

    private void Update()
    {
          
        healthPool.fillAmount = currentHealth / stats.maxHealth;
        manaPool.fillAmount = currentMana / stats.maxMana;
        PassiveManaRegen();
        PassiveHealthRegen();
        CharacterMovement();
        CharacterDirection();
        BasicAttack();
        Jump();
     
    }
   
  
    public void PassiveManaRegen()
    {
        if (currentMana <= stats.maxMana)
        {
            currentMana += stats.replenishM * Time.deltaTime;
            
        }
    }

    public void PassiveHealthRegen()
    {
        if (currentHealth <= stats.maxHealth)
        {
            currentHealth += stats.replenishH * Time.deltaTime;

        }
    }

    //Make character move with default horizontal and vertical buttons
    private void CharacterMovement()
    {

        if (Input.GetAxisRaw("Horizontal") > 0  && !isDead || Input.GetAxisRaw("Vertical") > 0  && !isDead || Input.GetAxisRaw("Horizontal") < 0  && !isDead || Input.GetAxisRaw("Vertical") < 0  && !isDead)
        {
            charAnim.SetBool("Walk", true);
            isWalking = true;
        } else
        {
            charAnim.SetBool("Walk", false);
            isWalking = false;
        }

        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
              
        if (isWalking && !isDead)
        {

            transform.Translate(movement.normalized * stats.moveSpeed * Time.deltaTime);

        }
    }

    //Make the Character rotate at the mouse direction or controller
    private void CharacterDirection()
    {
        
        if (!useController)
        {
            Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, transform.position);
            float rayLength;

            if (plane.Raycast(cameraRay, out rayLength))
            {
         
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
                Quaternion targetRotation = Quaternion.LookRotation(pointToLook - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * stats.rotationSpeed);
                //transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z)

            }
        }
        
        if (useController)
        {
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RHorizontal") + 
                Vector3.forward * -Input.GetAxisRaw("RVertical");
            if (playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }
           
        }

    }

    //Left Mouse click Attack
    void BasicAttack()
    {

        if (!useController)
        {
            if (Input.GetButton("Fire1"))
            {
                int index = wep.GetWeapon();

                if (index == 0 && axeCdReady)
                {
                    StartCoroutine(AxeCooldown());
                }

                if (index == 1 && swordCdReady)
                {       
                    StartCoroutine(SwordCooldown());
                }

                if (index == 2 && spellCdReady && currentMana > spell.spellStats[6])
                {
                    StartCoroutine(SpellCooldown());
                }

            }

        }


        if (useController)
        {
            if (Input.GetButton("Fire1"))
            {
                int index = wep.GetWeapon();

                if (index == 0 && axeCdReady)
                {
                    StartCoroutine(AxeCooldown());
                }

                if (index == 1 && swordCdReady)
                {
                    StartCoroutine(SwordCooldown());
                }

                if (index == 2 && spellCdReady)
                {
                    StartCoroutine(SpellCooldown());
                }
            }
        }
    }

    // CD will disallow the spamhax
    IEnumerator AxeCooldown()
    {

        axeCdReady = false;      
        axe.UseAxeWeapon();
        charAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(axe.GetShotInterval());
        axeCdReady = true;

    }

    IEnumerator SwordCooldown()
    {

        swordCdReady = false;
        sword.UseSwordWeapon();
        charAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(sword.GetShotInterval());
        swordCdReady = true;

    }

    IEnumerator SpellCooldown()
    {

        spellCdReady = false;
        spell.UseSpellWeapon();
        charAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(spell.GetShotInterval());
        spellCdReady = true;

    }

    //Makes Character jump with jump button "space"
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && onGround)
        {
            charAnim.SetTrigger("Jump");
            rb.AddForce(0, stats.jumpForce,0, ForceMode.Impulse);
        } 
    }

    //Lots of GroundChecking to disallow spamhax jumping
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            onGround = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
     
        if (collision.collider.tag == "Ground")
        {
            onGround = true;
        }
    }

    public void PlayerLoseMana(float cost)
    {
        currentMana -= cost;
    }

    public void PlayerTakeDamage(float dmg)
    {
        if (!isDead)
        {
            currentHealth -= dmg;
            PlayerDie();
            Destroy(Instantiate(bloodSplit, transform.position, bloodSplit.transform.rotation), 1f);
        }
    }

    public void PlayerDie()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            GameStatus.theGameIsOn = false;
            charAnim.SetBool("Dies", true);    
            FindObjectOfType<CanvasControl>().LoseWindow();
            Destroy(Instantiate(loseSound), 3f);
           
        }
        
    }  

}

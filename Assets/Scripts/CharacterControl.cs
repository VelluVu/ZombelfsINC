using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour {

    float moveSpeed;
    float backwardsMoveSpeed;
    float diagonalMovementSpeed;
    float jumpForce;
    float rotationSpeed;

    float maxHealth;
    float currentHealth;
    public float nextAxeShot;
    public float nextSwordShot;
    public float nextSpellShot;
    float maxMana;
    float currentMana;
    float replenishM;
    float replenishH;

    int curSkill;

    bool onGround;
    bool recentlyShot;
    
    public bool swordCdReady;
    public bool axeCdReady;
    public bool spellCdReady;

    public bool useController;
    public Image healthPool;
    public Image manaPool;

    Rigidbody rb;
    Camera cam;
    public Axe axe;
    public Sword sword;
    public Spell spell;

    public GameObject bloodSplit;
    public GameObject loseSound;
    CharacterStats stats;

    //WeaponSwitch weaponSwitch;
    public void SpeedUp(float speedP)
    {
        moveSpeed *= speedP;
        backwardsMoveSpeed *= speedP;
        diagonalMovementSpeed *= speedP; 
        rotationSpeed *= speedP;
    }

    public void IncreaseJump(float jumpP)
    {
        jumpForce += jumpP;
    }

    public void IncrementMaxHealth(float upHp)
    {
        maxHealth += upHp;
    }

    public void HealHealth(float heal)
    {
        if (currentHealth <= maxHealth)
        {
            currentHealth += heal;
        }
    }

    public float GetCurrentMana()
    {
        return currentMana;
    }

    public void IncrementMaxMana(float upMana)
    {
        maxMana += upMana;
    }

    public void ReplenishMana(float replenishMana)
    {
        if (currentMana <= maxMana)
        {
            currentMana += replenishMana;
        }
    }

    public void ReplenishHealth(float replenishHealth)
    {
        if (currentHealth <= maxHealth)
        {
            currentHealth += replenishHealth;
        }
    }

    public bool GetSwordCd()
    {
        return swordCdReady;
    }

    public bool GetAxeCd()
    {
        return axeCdReady;
    }

    private void Start()
    {
        stats = gameObject.GetComponent<CharacterStats>();
        Time.timeScale = 1;
        //weaponSwitch = gameObject.GetComponentInChildren<WeaponSwitch>();
        backwardsMoveSpeed = stats.BackwardsMoveSpeed;
        diagonalMovementSpeed = stats.diagonalMovementSpeed;
        swordCdReady = true;
        axeCdReady = true;
        spellCdReady = true;
        maxMana = stats.maxMana;
        maxHealth = stats.maxHealth;           
        currentMana = maxMana;
        currentHealth = maxHealth;
        jumpForce = stats.jumpForce;
        moveSpeed = stats.moveSpeed;
        rotationSpeed = stats.rotationSpeed;
        replenishM = stats.replenishM;
        replenishH = stats.replenishH;
        rb = gameObject.GetComponent<Rigidbody>();
        cam = GameObject.FindGameObjectWithTag("TopDown").GetComponent<Camera>();      
    }

    public void ResetStats()
    {
        maxMana = stats.maxMana;
        jumpForce = stats.jumpForce;
        moveSpeed = stats.moveSpeed;
        rotationSpeed = stats.rotationSpeed;
        replenishM = stats.replenishM;
        maxHealth = stats.maxHealth;
        replenishH = stats.replenishH;
        backwardsMoveSpeed = stats.BackwardsMoveSpeed;
        diagonalMovementSpeed = stats.diagonalMovementSpeed;
    }

    private void Update()
    {
        /*if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }*/

        healthPool.fillAmount = currentHealth / maxHealth;
        manaPool.fillAmount = currentMana / maxMana;
        PassiveManaRegen();
        CharacterMovement();
        CharacterDirection();
        BasicAttack();
        Jump();       
    }
  
    public void PassiveManaRegen()
    {
        if (currentMana <= maxMana)
        {
            currentMana += replenishM * Time.deltaTime;
            //Debug.Log("regen mana: " + replenishM * Time.deltaTime);
        }
    }

    //Make character move with default horizontal and vertical buttons
    private void CharacterMovement()
    {
        float xMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        
        float zMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        if (xMovement > 0 && zMovement > 0 || xMovement < 0 && zMovement > 0)
        {
            xMovement *= diagonalMovementSpeed;
        }

        if (xMovement < 0 && zMovement < 0 || xMovement > 0 && zMovement < 0)
        {
            xMovement *= backwardsMoveSpeed;
        }
         
        if (zMovement <= 0)
        {
            zMovement *= backwardsMoveSpeed;
        }
        transform.Translate(xMovement, 0, zMovement);
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
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                //transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
                
                


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
            if (Input.GetButton("Fire1") && !recentlyShot && Time.time > nextAxeShot)
            {

                nextAxeShot = Time.time + axe.GetShotInterval();
                axe.SetAxeIsFiring(true);
                recentlyShot = true;

            }
            else
            {
                axe.SetAxeIsFiring(false);
                recentlyShot = false;
            }

            if (Input.GetButton("Fire1") && !recentlyShot && Time.time > nextSwordShot)
            {

                nextSwordShot = Time.time + sword.GetShotInterval();
                sword.SetSwordIsFiring(true);
                recentlyShot = true;

            }
            else
            {
                sword.SetSwordIsFiring(false);
                recentlyShot = false;
            }

            if (Input.GetButton("Fire1") && !recentlyShot && Time.time > nextSpellShot)
            {

                recentlyShot = true;
                nextSpellShot = Time.time + spell.GetShotInterval();
                spell.SetSpellIsFiring(true);

            }
            else
            {
                spell.SetSpellIsFiring(false);
                recentlyShot = false;
            }               

        }

        if (useController)
        {
            if (Input.GetButton("Fire1") && !recentlyShot && Time.time > nextAxeShot)
            {

                nextAxeShot = Time.time + axe.GetShotInterval();
                axe.SetAxeIsFiring(true);
                recentlyShot = true;

            }
            else
            {
                axe.SetAxeIsFiring(false);
                recentlyShot = false;
            }

            if (Input.GetButton("Fire1") && !recentlyShot && Time.time > nextSwordShot)
            {

                nextSwordShot = Time.time + sword.GetShotInterval();
                sword.SetSwordIsFiring(true);
                recentlyShot = true;

            }
            else
            {
                sword.SetSwordIsFiring(false);
                recentlyShot = false;
            }

            if (Input.GetButton("Fire1") && !recentlyShot && Time.time > nextSpellShot)
            {

                recentlyShot = true;
                nextSpellShot = Time.time + spell.GetShotInterval();
                spell.SetSpellIsFiring(true);

            }
            else
            {
                spell.SetSpellIsFiring(false);
                recentlyShot = false;
            }
                    
        }
    }

    // CD will disallow the spamhax
    IEnumerator AxeCooldown()
    {
        recentlyShot = true;
        yield return new WaitForSeconds(axe.GetShotInterval());
        axe.SetAxeIsFiring(true);
        axeCdReady = false;
        
    }

    IEnumerator SwordCooldown()
    {
        recentlyShot = true;
        yield return new WaitForSeconds(sword.GetShotInterval());
        sword.SetSwordIsFiring(true);
        swordCdReady = false;
        
    }

    IEnumerator SpellCooldown()
    {
        recentlyShot = true;
        yield return new WaitForSeconds(spell.GetShotInterval());
        spell.SetSpellIsFiring(true);
        spellCdReady = false;
        
    }

    //Makes Character jump with jump button "space"
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.AddForce(0,jumpForce,0, ForceMode.Impulse);
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
        currentHealth -= dmg;
        PlayerDie();
        Destroy(Instantiate(bloodSplit, transform.position, bloodSplit.transform.rotation),1f);
    }

    public void PlayerDie()
    {
        if (currentHealth <= 0)
        {
            Time.timeScale = 0;
            FindObjectOfType<CanvasControl>().LoseWindow();
            Destroy(Instantiate(loseSound), 3f);              
        }
    }
}

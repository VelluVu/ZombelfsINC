using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureAnimate : MonoBehaviour {

    public Animator chestAnim;
    public GameObject shiny;
    bool shinyLoot;
    float shinyEnough = 0;

    private void Update()
    {
        shinyEnough -= Time.deltaTime;

        if (shinyLoot)
        {
            if (shinyEnough <= 0)
            {
                Destroy(Instantiate(shiny, new Vector3 (transform.position.x,transform.position.y+0.5f,transform.position.z), Quaternion.identity), 3);
                shinyEnough = 3;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
            if (other.CompareTag("Player"))
            {

                chestAnim.SetBool("Open", true);
                shinyLoot = true;
            
            }
                      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chestAnim.SetBool("Open", false);
        }
        shinyLoot = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureAnimate : MonoBehaviour {

    public Animator chestAnim;

    private void OnTriggerStay(Collider other)
    {
        
            if (other.CompareTag("Player"))
            {
                chestAnim.SetBool("Open", true);
            }
           
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chestAnim.SetBool("Open", false);
        }
    }
}

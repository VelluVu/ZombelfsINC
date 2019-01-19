using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : Interactable {

    public List<Item> items = new List<Item>();   
    public Text pickUpText;
    int rng;
    
    private void Start()
    {
        
        rng = Random.Range(0, items.Count);
        pickUpText = GameObject.FindGameObjectWithTag("GlobalText").GetComponent<Text>();

    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {

            if (pickUpText.enabled == false)
            {

                pickUpText.enabled = true;
                pickUpText.GetComponent<Text>().text = "Pick up (E) " + items[rng].name;

            }                
            
            if (Input.GetKeyDown(KeyCode.E))
            {

                PickUp();

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        pickUpText.enabled = false;

    }

    void PickUp()
    {

        pickUpText.enabled = false;
        Debug.Log("Picked Up : " + items[rng].name);

        bool wasPickedUp = Inventory.instance.Add(items[rng]);

        if (wasPickedUp)
        {          
            Destroy(gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmgText : MonoBehaviour {

    float duration;
    float speed;
    public Text displayAmount;

    private void Start()
    {      
        duration = 3f;
        speed = 1f;
        Destroy(gameObject, duration);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        

    }

    public void SetDmgText(float amount, Enemy curEnemy, bool crit)
    {
        if (curEnemy != null)
        {
            displayAmount.text = amount.ToString();
            if (crit)
            {
                displayAmount.color = Color.yellow;
                displayAmount.fontSize = 26;
            }
            else
            {
                displayAmount.color = Color.magenta;
                displayAmount.fontSize = 22;
            }
        }
    }

    public float GetDuration()
    {
        return duration; 
    }


}

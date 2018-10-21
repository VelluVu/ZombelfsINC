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
        speed = 0.5f;
        Destroy(gameObject, duration);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void SetDmgText(float amount, Enemy curEnemy)
    {
        if (curEnemy != null)
        {
            displayAmount.text = amount.ToString();
        }
    }

    public float GetDuration()
    {
        return duration; 
    }


}

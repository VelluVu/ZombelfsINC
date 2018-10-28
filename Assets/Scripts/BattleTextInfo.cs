using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTextInfo : MonoBehaviour {

    Text curText;
    Color green;

    //colors depend on difficulty of the level
    //Color red;
    //Color yellow;

    private void Start()
    {
        curText = gameObject.GetComponent<Text>();
        green = Color.green;
        
        //red = Color.red;
        //yellow = Color.yellow;

        curText.color = green;
    }
}

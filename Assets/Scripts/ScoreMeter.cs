using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMeter : MonoBehaviour {

	
	void Update () {
        gameObject.GetComponent<Text>().text = ScoreTable.Points.ToString();
	}
}

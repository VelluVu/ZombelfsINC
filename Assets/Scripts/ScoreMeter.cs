using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMeter : MonoBehaviour {

    Text scoreText;

    private void Start()
    {
        scoreText = gameObject.GetComponent<Text>();
        scoreText.text = ScoreTable.GetScore(ScoreTable.currentPlayer, "score").ToString();
    }

    void Update () {
        scoreText.text = ScoreTable.GetScore(ScoreTable.currentPlayer,"score").ToString();
	}
}

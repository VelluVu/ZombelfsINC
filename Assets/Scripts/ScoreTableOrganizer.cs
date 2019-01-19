using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTableOrganizer : MonoBehaviour {

    public GameObject playerScorePrefab;

    public void OpenHighScore()
    {
        
        if (ScoreTable.playerScores == null)
        {
            Debug.Log("No scoreTable");
            return;
        }

        ScoreTable.LoadAllScores();

        string[] players = ScoreTable.GetPlayers();

        foreach (string name in players)
        {

            GameObject obj = Instantiate(playerScorePrefab);
            obj.transform.SetParent(this.transform);
            obj.transform.Find("Name").GetComponent<Text>().text = name;
            obj.transform.Find("Value1").GetComponent<Text>().text = ScoreTable.GetScore(name, "level").ToString();
            obj.transform.Find("Value2").GetComponent<Text>().text = ScoreTable.GetScore(name, "score").ToString();

        }
    }
}

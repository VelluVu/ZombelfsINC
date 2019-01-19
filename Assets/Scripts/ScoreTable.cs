using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ScoreTable : MonoBehaviour {

	public static int Points { get; set; }
    public static Dictionary<string, Dictionary<string, int>> playerScores = new Dictionary<string, Dictionary<string, int>>();
    public static string currentPlayer;
    //playerScores["playerName"]["scoreType"] = value;

   
    public static void PrintScore()
    {
        Debug.Log("Points: " + Points);
    }

    public static void Addpoint(int point)
    {
        //playerScores["playerName"]["score"] = point;
        Points += point;
    }
    
    public static int GetScore(string playerName, string scoreType)
    {
        
        if (playerScores.ContainsKey(playerName) == false)
        {
            return 0;
        }

        if(playerScores[playerName].ContainsKey(scoreType) == false)
        {
            return 0;
        }

        return playerScores[playerName][scoreType];

    }

    public static void SetScore(string playerName, string scoreType, int value)
    {
       
        if (playerScores.ContainsKey(playerName) == false)
        {
            playerScores[playerName] = new Dictionary<string, int>();
        }

        playerScores[playerName][scoreType] = value;

    }

    public static void ChangeScore(string playerName, string scoreType, int amount)
    {
        
        int curScore = GetScore(playerName, scoreType);
        SetScore(playerName, scoreType, curScore + amount);

    }

    public static string[] GetPlayers()
    {
        
        return playerScores.Keys.ToArray();
    }

    public static void SaveScores()
    {

        if(PlayerPrefs.HasKey(currentPlayer + "scores"))
        {       
            PlayerPrefs.SetInt(currentPlayer + "scores", GetScore(currentPlayer,"score"));
        }
        if(PlayerPrefs.HasKey(currentPlayer + "levels"))
        {
            PlayerPrefs.SetInt(currentPlayer + "levels", GetScore(currentPlayer, "level"));
        }
        
    }

    public static void LoadScores()
    {
        if(PlayerPrefs.HasKey(currentPlayer))
        {
            SetScore(currentPlayer, "score", PlayerPrefs.GetInt(currentPlayer + "scores"));
            SetScore(currentPlayer, "level", PlayerPrefs.GetInt(currentPlayer + "levels"));
        }

    }

    public static void LoadAllScores()
    {
        
    }
}

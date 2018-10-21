using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ScoreTable : MonoBehaviour {

	public static int Points { get; set; }
    
    public static void PrintScore()
    {
        Debug.Log("Points: " + Points);
    }

    public static void Addpoint(int point)
    {
        Points += point;
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour {

    public static string currentLevel;

	void Awake () {

        DontDestroyOnLoad(gameObject);
        
    }
    private void Update()
    {
        Debug.Log("CurrentLevel: " + currentLevel);
    }
}

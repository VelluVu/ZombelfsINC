﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour {

    public string levelToLoad;
    public bool cleared;
    public Material completeMat;

    private void Start()
    {
        if (GameStatus.status.GetType().GetField(levelToLoad).GetValue(GameStatus.status).ToString() == "True") {
            Cleared(true);
        }
    }

    public void Cleared(bool isClear)
    {
        Debug.Log("Cleared " + isClear + " " + gameObject.name);

        if (isClear == true)
        {
            cleared = true;
            GameStatus.status.GetType().GetField(levelToLoad).SetValue(GameStatus.status, true);
            transform.GetComponent<MeshRenderer>().material = completeMat;
        }
    }
}
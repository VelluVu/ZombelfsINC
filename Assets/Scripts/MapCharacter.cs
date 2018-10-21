﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCharacter : MonoBehaviour {

    float speed;
    
    private void Start()
    {
        Time.timeScale = 1;
        if (GameStatus.currentLevel != null)
        {          
            transform.position = GameObject.Find(GameStatus.currentLevel).transform.position;
        }
        speed = 1f;

    }

    private void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 
            Input.GetAxis("Vertical") * speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LevelTrigger")
        {
            if (collision.gameObject.name != GameStatus.currentLevel) {
                GameStatus.currentLevel = collision.gameObject.name;
                SceneManager.LoadScene(collision.gameObject.GetComponent<LoadLevel>().levelToLoad);
            }
        }
    }
}

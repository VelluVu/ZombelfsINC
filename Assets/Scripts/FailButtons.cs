using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailButtons : MonoBehaviour {

    Button button;
  
    private void Start()
    {

        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnPress);
        
    }

    void OnPress()
    {
        switch (button.tag)
        {
            case "Replay":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);              
                break;
            case "Leave":
                GameStatus.winStatus = false;
                SceneManager.LoadScene("Map");
                break;

            default:
                break;
        }
    }
}

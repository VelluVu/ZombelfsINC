using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour {

    public GameObject loseWindow;
    public GameObject winWindow;

    public void LoseWindow()
    {      
        loseWindow.SetActive(true);
    }

    public void WinWindow()
    {
        winWindow.SetActive(true);
    }

}

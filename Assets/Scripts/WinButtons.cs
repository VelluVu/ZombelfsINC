using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinButtons : MonoBehaviour {

    Button button;

    private void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Press);
    }

    void Press()
    {
        SceneManager.LoadScene("Map");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePrefabDestroy : MonoBehaviour {

    Button button;

    private void Start()
    {
        button = GameObject.Find("Back").GetComponent<Button>();
        button.onClick.AddListener(ImOut);
    }

    void ImOut()
    {
        Destroy(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LastAreaWinCondition : MonoBehaviour {

    public GameObject boss;
    public GameObject winText;
    public GameObject winWindow;

    private void Start()
    {
        StartCoroutine(BossCheck());
    }

    IEnumerator BossCheck()
    {
        while (GameStatus.theGameIsOn == true)
        {
            yield return new WaitForSeconds(5f);

            if (boss.GetComponent<EnemyWithAi>() == null)
            {
  
                FindObjectOfType<CanvasControl>().WinWindow();

            }
        }
    }

}

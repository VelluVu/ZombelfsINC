using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SurvivalWinCondition : MonoBehaviour
{

    public List<GameObject> spawnPoints = new List<GameObject>();
    int count;

    // Use this for initialization
    void Start()
    {
        count = 0;
        StartCoroutine(TimeToVictory());
        StartCoroutine(SpawnCheck());

    }

    IEnumerator TimeToVictory()
    {
        yield return new WaitForSeconds(300);
        FindObjectOfType<CanvasControl>().WinWindow();
    }

    IEnumerator SpawnCheck()
    {

        while (GameStatus.theGameIsOn == true)
        {
            count = 0;
            yield return new WaitForSeconds(5);
            if(FindObjectOfType<EnemyBase>() == null)
            {
                FindObjectOfType<CanvasControl>().WinWindow();
            }
        }
    }
}

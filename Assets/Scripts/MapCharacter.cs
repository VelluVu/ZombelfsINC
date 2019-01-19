using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapCharacter : MonoBehaviour {

    public Text battleText;
    public NavMeshAgent navMesh;
    public Camera cam;
    public Animator anim;
    public GameObject gameComplete;
    public GameObject introduction;
    
    private void Start()
    {
        GameStatus.theGameIsOn = true;
        GameStatus.inLevel = false;
        if(GameStatus.gameBegin == true)
        {
            introduction.SetActive(true);
        }

        GameStatus.gameBegin = false;

        if (GameStatus.currentLevel != null)
        {
            if (GameStatus.winStatus == true)
            {
                GameObject.Find(GameStatus.currentLevel).GetComponent<LoadLevel>().Cleared(true);

                if(GameStatus.gameCompleted == true)
                {
                    gameComplete.SetActive(true);
                }
            }
            transform.position = GameObject.Find(GameStatus.currentLevel).transform.position;
        }
       
    }

    private void Update()
    {
        MoveToTarget();
        if(navMesh.hasPath)
        {
            anim.SetBool("Walk", true);
        } else
        {
            anim.SetBool("Walk", false);
        }
    }

    private void MoveToTarget()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                navMesh.destination = hit.point;
                
            }
        } 
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "LevelTrigger")
        {
            battleText.enabled = true;
            
            if (Input.GetAxis("Submit") > 0)
            {
                /*if (collision.gameObject.name != GameStatus.currentLevel)
                {*/
                    GameStatus.currentLevel = collision.gameObject.name;
                    SceneManager.LoadScene(collision.gameObject.GetComponent<LoadLevel>().levelToLoad);
               // }
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        battleText.enabled = false;
    }
}